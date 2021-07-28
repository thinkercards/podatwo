using CCMERP.Domain.Auth;
using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Domain.Enum;
using CCMERP.Domain.Settings;
using CCMERP.Persistence;
using CCMERP.Service.Contract;
using CCMERP.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace CCMERP.Service.Implementation
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole<int>> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailService _emailService;
		private readonly JWTSettings _jwtSettings;
		private readonly IDateTimeService _dateTimeService;
		private readonly IFeatureManager _featureManager;
		public MailSettings _mailSettings { get; }
		public ApplicationDetail _applicationDetail { get; }
		private static Random random = new Random();
		private readonly IdentityContext _context;
		private readonly ITransactionDbContext _tcontext;
		public AccountService(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole<int>> roleManager,
			IOptions<JWTSettings> jwtSettings,
			IDateTimeService dateTimeService,
			SignInManager<ApplicationUser> signInManager,
			IEmailService emailService,
			IFeatureManager featureManager,
			IOptions<MailSettings> mailSettings,
			IOptions<ApplicationDetail> applicationDetail,
			IdentityContext context,
			ITransactionDbContext tcontext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtSettings = jwtSettings.Value;
			_dateTimeService = dateTimeService;
			_signInManager = signInManager;
			_emailService = emailService;
			_featureManager = featureManager;
			_mailSettings = mailSettings.Value;
			_applicationDetail = applicationDetail.Value;
			_context = context;
			_tcontext = tcontext;
		}
		public async Task<Response<int>> Resendotp(ForgotPasswordRequest model, string ipAddress)
		{
			try
			{
			
			   var account = await _userManager.FindByEmailAsync(model.Email);

				// always return ok response to prevent email enumeration
				if (account == null)
				{

					return await Task.FromResult(new Response<int>(0, message: $"No Accounts Registered with {model.Email}.", false));
				}
				else
				{
					var providers = await _userManager.GetValidTwoFactorProvidersAsync(account);
					var token = await _userManager.GenerateTwoFactorTokenAsync(account, providers[0]);
					await _emailService.SendEmailAsync(new MailRequest() { From = _mailSettings.SmtpUser, ToEmail = account.Email, Body = $"Dear '{account.FirstName},' Use this OTP {token} for complete your Sign in procedures. OTP is valid for 5 minutes", Subject = "OTP Verification" });

					var user1 = _context.Useauthrtokens.Where(a => a.UserId == account.Id && a.LoginProvider == providers[0] && a.Name == account.Email).FirstOrDefault();
					if (user1 != null)
					{

						_context.Useauthrtokens.Attach(user1);
						_context.Useauthrtokens.Remove(user1);
						_context.SaveChanges();
					}

					Useauthrtokens usertokens = new Useauthrtokens()
					{
						UserId = account.Id,
						LoginProvider = providers[0],
						Name = account.Email,
						Value = token,
						IpAddress = ipAddress,
						Status = 1
						//expires = DateTime.UtcNow.AddMinutes(5).ToString("yyyy-MM-dd HH:MM:ss")
					};
					_context.Useauthrtokens.Add(usertokens);
					await _context.SaveChangesAsync();
					return await Task.FromResult(new Response<int>(1, message: $"Success", true));
				}
			}
			catch (Exception)
			{

				return await Task.FromResult(new Response<int>(0, message: $"Exception", false));
			}
		}
		public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
		{
			ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

			AuthenticationResponse response = new AuthenticationResponse();

			try
			{
				if (user == null)
				{
					return new Response<AuthenticationResponse>(response, message: $"No Accounts Registered with {request.Email}.", false);
				}
				else
				{

					user.TwoFactorEnabled = true;
					await _userManager.UpdateAsync(user);
					response.Email = request.Email;
				}
				if (!user.EmailConfirmed)
				{
					return new Response<AuthenticationResponse>(response, message: $"Account Not Confirmed for '{request.Email}'.", false);
				}

				var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
				if (!result.Succeeded && !result.RequiresTwoFactor)
				{
					return new Response<AuthenticationResponse>(response, message: $"Invalid Credentials for '{request.Email}'.", false);
				}
				var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
				var token = await _userManager.GenerateTwoFactorTokenAsync(user, providers[0]);
				await _emailService.SendEmailAsync(new MailRequest() { From = _mailSettings.SmtpUser, ToEmail = user.Email, Body = $"Dear '{user.FirstName},' Use this OTP {token} for complete your Sign in procedures. OTP is valid for 5 minutes", Subject = "OTP Verification" });
				response.Email = user.Email;
				var user1 = _context.Useauthrtokens.Where(a => a.UserId == user.Id && a.LoginProvider == providers[0] && a.Name == user.Email).FirstOrDefault();
				if (user1 != null)
				{
					 
					_context.Useauthrtokens.Attach(user1);
					_context.Useauthrtokens.Remove(user1);
					_context.SaveChanges();
				}


				

				Useauthrtokens usertokens = new Useauthrtokens()
				{
					UserId = user.Id,
					LoginProvider = providers[0],
					Name = user.Email,
					Value = token,
					IpAddress = ipAddress,
					Status = 1
					//expires = DateTime.UtcNow.AddMinutes(5).ToString("yyyy-MM-dd HH:MM:ss")
				};
				_context.Useauthrtokens.Add(usertokens);
				await _context.SaveChangesAsync();

				return new Response<AuthenticationResponse>(response, $"Your successfully authenticate  and verification otp  sent  your email address {response.Email}", true);
			}
			catch (Exception ex)
			{
				return new Response<AuthenticationResponse>(response, $"Exception", false);
			}
		}


		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}


		public async Task<Response<AuthenticationResponse>> TwoFactorAuthenticateAsync(TwoFactorAuthenticationRequest request, string ipAddress)
		{
			AuthenticationResponse response = new AuthenticationResponse();

			try
			{
				var user = await _userManager.FindByEmailAsync(request.Email);
				if (user == null)
				{
					return new Response<AuthenticationResponse>(response, message: $"No Accounts Registered with {request.Email}.", false);
				}
				var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
				//var user1 = await _signInManager.GetTwoFactorAuthenticationUserAsync();
				//var result = await _signInManager.TwoFactorSignInAsync(providers[0], request.otp.Trim(), false, rememberClient: false);
				//CultureInfo culture = new CultureInfo("en-US");
				//DateTime tempDate = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd HH:MM:ss"), CultureInfo.InvariantCulture);


				Useauthrtokens user1 = _context.Useauthrtokens.Where(a => a.UserId == user.Id && a.LoginProvider == providers[0] && a.Name == user.Email && a.Value == request.otp.Trim() && a.Status==1).FirstOrDefault();
    //            if (user1 == null)
    //            {
				//	return new Response<AuthenticationResponse>(response, message: $"Please enter a valid otp'.", false);
				//}
				//bool dt = tempDate <= Convert.ToDateTime(user1.expires, CultureInfo.InvariantCulture);
				//response.Email = user.Email;
				if (user1 == null)
				{
					return new Response<AuthenticationResponse>(response, message: $"Please enter a valid otp'.", false);
				}
				else
				{
					user1.Status = 0;
					_context.Useauthrtokens.Update(user1);
					await _context.SaveChangesAsync();

					JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
					response.Id = user.Id;
					response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
					response.Email = user.Email;
					response.UserName = user.UserName;
					response.IsVerified = user.EmailConfirmed;
					response.firstName = user.FirstName;
					response.lastName = user.LastName;
					var refreshToken = GenerateRefreshToken(ipAddress);
					response.RefreshToken = refreshToken.Token;
					try
                    {
					var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
					if (rolesList.Count > 0)
					{
						response.Roles = rolesList.ToList();
						
						if (rolesList.Contains("ClientAdmin"))
						{

							var org = _context.OrganizationUserMapping.Where(a => a.User_ID == user.Id && a.Role_ID == 2).FirstOrDefault();
							if (org != null)
							{
								response.orgId = org.Org_ID;

							}
						}
						if (rolesList.Contains("SalesRep"))
						{

							var org = _context.OrganizationUserMapping.Where(a => a.User_ID == user.Id && a.Role_ID ==3).FirstOrDefault();
							if (org != null)
							{
								response.orgId = org.Org_ID;

							}
						}
						else if (rolesList.Contains("Customer"))
						{

							var orgm = _tcontext.organizationCustomerMappings.Where(a => a.User_ID == user.Id).ToList();
								if (orgm.Count > 0)
								{
									var org = _context.Organization.Select(a => new Organization { Org_ID = a.Org_ID, Name = a.Name }).ToList();


									var corg = (from o in orgm
												join i in org
												on o.Org_ID equals i.Org_ID
												where o.User_ID == response.Id
												select new CustomerOrganizations
												{
													Name = i.Name,
													Org_ID = i.Org_ID
												}).ToList();

									if (corg.Count > 0)
									{
										response.customerOrganizations = corg;
									}
								}
						}

					}
					}
					catch (Exception)
					{

					
					}


					return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}", true);
				}




			}
			catch (Exception ex)
			{

				return new Response<AuthenticationResponse>(response, $"Exception:{ex.Message}", false);
			}
		}

		public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
		{
			try
			{

		   
			var userWithSameUserName = await _userManager.FindByEmailAsync(request.email);
				if (userWithSameUserName != null)
				{
					if (request.role == "Customer")
					{
						var cust = _tcontext.organizationCustomerMappings.Where(a => a.Org_ID == request.Org_ID && a.CustomerID == request.mapId).FirstOrDefault();
						if (cust == null)
						{
							var cust1 = _tcontext.Customers.Where(a =>  a.CustomerID == request.mapId).FirstOrDefault();
							var org1 = _context.Organization.Where(a => a.Org_ID == request.Org_ID ).FirstOrDefault();


							return new Response<string>(request.email, message: $"Customer '{ cust1.Name }' not linked with this organisation '{org1.Name}'.", false);
						}
						else
						{
							var Olduser = await _userManager.FindByEmailAsync(request.email);
							cust.User_ID = Olduser.Id;
							_tcontext.organizationCustomerMappings.Update(cust);
							await _tcontext.SaveChangesAsync();
							var cust1 = _tcontext.Customers.Where(a => a.CustomerID == request.mapId).FirstOrDefault();
							var org1 = _context.Organization.Where(a => a.Org_ID == request.Org_ID).FirstOrDefault();
							await _emailService.SendEmailAsync(new MailRequest() { From = _mailSettings.SmtpUser, ToEmail = Olduser.Email, Body = $"Customer '{cust1.Name},' successfully linked with this organisation '{org1}'", Subject = "Confirm Registration" });

							return new Response<string>(request.email, message: $"Customer '{cust1.Name},' successfully linked with this organisation '{org1.Name}'", true);
						}
					}
					else
					{
						return new Response<string>(request.email, message: $"Username '{request.email}' is already taken.", false);
					}
			}
			var user = new ApplicationUser
			{
				Email = request.email,
				FirstName = request.firstName,
				LastName = request.lastName,
				UserName = request.email,
				PhoneNumber=request.phoneNumber
			};
			var userWithSameEmail = await _userManager.FindByEmailAsync(request.email);
			if (userWithSameEmail == null)
			{
				string pwd = "Pass@123";
				var result = await _userManager.CreateAsync(user, pwd);
				if (result.Succeeded)
				{
						
						await _userManager.AddToRoleAsync(user, request.role);

                        if (request.role == "ClientAdmin")
                        {
							OrganizationUserMapping organizationUserMapping = new OrganizationUserMapping()
							{
								User_ID = user.Id,
								Org_ID  = request.mapId,
								Role_ID= 2,
								IsActive=1
							};
							_context.OrganizationUserMapping.Add(organizationUserMapping);
							await _context.SaveChangesAsync();
						}else if(request.role == "Customer")
						{
							var cust = _tcontext.organizationCustomerMappings.Find(request.Org_ID , request.mapId);
							if (cust == null)
							{
								var cust1 = _tcontext.Customers.Where(a => a.CustomerID == request.mapId).FirstOrDefault();
								var org1 = _context.Organization.Where(a => a.Org_ID == request.Org_ID).FirstOrDefault();
								return new Response<string>(request.email, message: $"Customer '{ cust1.Name }' not linked with this organisation '{org1.Name}'.", false);
							}
							else
							{



								cust.User_ID = user.Id;
								cust.IsActive = 1;
								_tcontext.organizationCustomerMappings.Update(cust);
							var ret=	await _tcontext.SaveChangesAsync();

								
							}
						}else if (request.role == "SalesRep")
						{
							OrganizationUserMapping organizationUserMapping = new OrganizationUserMapping()
							{
								User_ID = user.Id,
								Org_ID = request.Org_ID,
								Role_ID = 3,
								IsActive = 1
							};
							_context.OrganizationUserMapping.Add(organizationUserMapping);
							await _context.SaveChangesAsync();
						}
                        try
                        {
                            var verificationUri = await SendVerificationEmail(user, _applicationDetail.ContactWebsite);
							if (!string.IsNullOrEmpty(verificationUri))
							{
								await _emailService.SendEmailAsync(new MailRequest() { From = _mailSettings.SmtpUser, ToEmail = user.Email, Body = $"<!DOCTYPE html><html><body><p>Your account successfully created,</p>Please confirm your account by <a href='{verificationUri}'> click this link </a></body></html>", Subject = "Confirm Registration" });
							}
                        }
						catch (Exception)
                        {

                           
                        }
					
						
						return new Response<string>(user.Id.ToString(), message: $"Account successfully created", true);
				}
				else
				{
					return new Response<string>(request.email, message: $"{result.Errors.ToList()[0].Description}", false);
				}
			}
			else
			{
			   
				return new Response<string>(request.email, message: $"Email '{request.email}' is already taken.", false);
			}
			}
			catch (Exception ex)
			{

				return new Response<string>(request.email, $"Exception:{ex.Message}", false);
			}
		}

		private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);

			var roleClaims = new List<Claim>();

			for (int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim("roles", roles[i]));
			}

			string ipAddress = IpHelper.GetIpAddress();

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id.ToString()),
				new Claim("ip", ipAddress),
				new Claim("startTime", DateTime.Now.ToString()),
				new Claim("expires", DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes).ToString()),
				new Claim("update", DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes-2).ToString()),
				new Claim("Duration", _jwtSettings.DurationInMinutes.ToString()),
				new Claim("firstname", user.FirstName),
				new Claim("lastname", user.LastName),
				new Claim("email", user.Email),
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: signingCredentials
				);
			return jwtSecurityToken;
		}

		private string RandomTokenString()
		{
			using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
			var randomBytes = new byte[40];
			rngCryptoServiceProvider.GetBytes(randomBytes);
			// convert random bytes to hex string
			return BitConverter.ToString(randomBytes).Replace("-", "");
		}

		private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
		{
            try
            {

			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			var route = "confirm/email";
			var _enpointUri = new Uri(string.Concat($"{origin}", route));
			var verificationUri = $"{_enpointUri.ToString()}?email={user.Email}&code={HttpUtility.UrlEncode(code)}";
			return verificationUri;
			}
			catch (Exception ex)
			{

				return "";
			}
		}

		public async Task<Response<ConfirmEmailResponse>> ConfirmEmailAsync(string email, string code)
		{
			ConfirmEmailResponse confirmEmailResponse = new ConfirmEmailResponse();

            try
            {

            
			var user = await _userManager.FindByEmailAsync(email);
			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var result = await _userManager.ConfirmEmailAsync(user, code);
			if (result.Succeeded)
			{
				confirmEmailResponse.Email = user.Email;
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				confirmEmailResponse.Token = token;
				return new Response<ConfirmEmailResponse>(confirmEmailResponse, message: $"Account Confirmed for {user.Email}", true);
			}
			else
			{
				return new Response<ConfirmEmailResponse>(confirmEmailResponse,message: $"An error occured while confirming {user.Email}.", false);
			}
			}
			catch (Exception)
			{

				return await Task.FromResult(new Response<ConfirmEmailResponse>(confirmEmailResponse, message: $"Exception", false));
			}
		}

			

		private RefreshToken GenerateRefreshToken(string ipAddress)
		{
			return new RefreshToken
			{
				Token = RandomTokenString(),
				Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				Created = DateTime.UtcNow,
				CreatedByIp = ipAddress
			};
		}

		public async Task<Response<int>>  ForgotPassword(ForgotPasswordRequest model, string origin)
		{
            try
            {

            
			var account = await _userManager.FindByEmailAsync(model.Email);

			// always return ok response to prevent email enumeration
			if (account == null)
			{

				return await Task.FromResult( new Response<int>(0, message: $"User account not found", false));
			}
			else
			{
				var code = await _userManager.GeneratePasswordResetTokenAsync(account);
				var route = $"{ _applicationDetail.ContactWebsite}reset/password";
				var _enpointUri = new Uri(route);
				var verificationUri = $"{_enpointUri.ToString()}?email={account.Email}&code={HttpUtility.UrlEncode(code)}";

				var emailRequest = new MailRequest()
				{
					Body = $"<!DOCTYPE html><html><body><a href='{verificationUri}'> Click this link </a> to reset your password</body></html>",
					ToEmail = model.Email,
					Subject = "Reset Password",
				};
				await _emailService.SendEmailAsync(emailRequest);
				return await Task.FromResult(new Response<int>(1, message: $"Success", true));
			}
			}
			catch (Exception)
			{

				return await Task.FromResult(new Response<int>(0, message: $"Exception", false));
			}
		}

		public async Task<Response<int>> ResetPassword(ResetPasswordRequest model)
		{
            try
            {

           
			var account = await _userManager.FindByEmailAsync(model.Email);
			if (account == null)
            {

				return await Task.FromResult(new Response<int>(0, message: $"No Accounts Registered with {model.Email}.", false));

            }
            else
            {
				var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
				if (result.Succeeded)
				{
					return await Task.FromResult(new Response<int>(1, message: $"Password Resetted.", true));
				}
				else
				{
					return await Task.FromResult(new Response<int>(0, message: $"Error occured while reseting the password.", false));
				}

			}
			}
			catch (Exception ex)
			{

				return await Task.FromResult(new Response<int>(0, message: $"Exception", false));
			}
		}


		public async Task<Response<GetUsersresponse>> GetUsers(int orgId = 0, int customerId = 0)
		{

			GetUsersresponse usersresponse = new GetUsersresponse();
			try
			{

				if (customerId == 0)
				{
					int[] ro = { 2, 3 };
					var ou = _context.OrganizationUserMapping.Where(a=>a.Org_ID==orgId && a.IsActive==1).ToList();
					var oc = _tcontext.organizationCustomerMappings.Where(a => a.Org_ID == orgId && a.IsActive == 1).ToList();
					var _user = (from t1 in _context.Users
								 join t2 in _context.UserRoles on t1.Id equals t2.UserId
								 join t3 in _context.Roles on t2.RoleId equals t3.Id
								 select new Users
								 {
									 id = t1.Id,
									 firstName = t1.FirstName,
									 lastName = t1.LastName,
									 userName = t1.UserName,
									 email = t1.Email,
									 phoneNumber = t1.PhoneNumber,
									 role = t3.Name

								 }).ToList();

					usersresponse.users = (from t1 in _user
										   join t2 in ou on t1.id equals t2.User_ID
										   select new Users
										   {
											   id = t1.id,
											   firstName = t1.firstName,
											   lastName = t1.lastName,
											   userName = t1.userName,
											   email = t1.email,
											   phoneNumber = t1.phoneNumber,
											   role = t1.role,
											   orgId =t2.Org_ID
										   }).Union<Users>((from t1 in _user
															join t2 in oc on t1.id equals t2.User_ID
															select new Users
															{
																id = t1.id,
																firstName = t1.firstName,
																lastName = t1.lastName,
																userName = t1.userName,
																email = t1.email,
																phoneNumber = t1.phoneNumber,
																role = t1.role,
																orgId = t2.Org_ID,
																customerId = t2.CustomerID
															})).ToList();


				}
				else
				{

					var oc = _tcontext.organizationCustomerMappings.Where(a => a.CustomerID == customerId && a.IsActive == 1).ToList();
					var _user = (from t1 in _context.Users
								 join t2 in _context.UserRoles on t1.Id equals t2.UserId
								 join t3 in _context.Roles on t2.RoleId equals t3.Id
								 select new Users
								 {
									 id = t1.Id,
									 firstName = t1.FirstName,
									 lastName = t1.LastName,
									 userName = t1.UserName,
									 email = t1.Email,
									 phoneNumber = t1.PhoneNumber,
									 role = t3.Name

								 }).ToList();
					usersresponse.users = (from t1 in _user
										   join t2 in oc on t1.id equals t2.User_ID
										   select new Users
										   {
											   id = t1.id,
											   firstName = t1.firstName,
											   lastName = t1.lastName,
											   userName = t1.userName,
											   email = t1.email,
											   phoneNumber = t1.phoneNumber,
											   role = t1.role,
											   orgId = t2.Org_ID,
											   customerId = t2.CustomerID
										   }).ToList();

				}

				if (usersresponse.users.Count == 0)
				{
					
					return await Task.FromResult(new Response<GetUsersresponse>(usersresponse, "No record found ", false));
				}
				else
				{
					usersresponse.totalNoRecords = usersresponse.users.Count;
					
					return await Task.FromResult(new Response<GetUsersresponse>(usersresponse, "Success", true));
				}
			}
			catch (Exception ex)
			{

				return await Task.FromResult(new Response<GetUsersresponse>(usersresponse, "Exception", false));
			}


		}


		public async Task<Response<GetUsersresponse>> GetSalesReps(int orgId)
		{

			GetUsersresponse usersresponse = new GetUsersresponse();
			try
			{
				int[] ro = { 2, 3 };
				var ou = _context.OrganizationUserMapping.Where(a => a.Org_ID == orgId && a.IsActive == 1).ToList();
				var oc = _tcontext.organizationCustomerMappings.Where(a => a.Org_ID == orgId && a.IsActive == 1).ToList();
				var _user = (from t1 in _context.Users
							 join t2 in _context.UserRoles on t1.Id equals t2.UserId
							 join t3 in _context.Roles on t2.RoleId equals t3.Id
							 where t3.Id == 3
							 select new Users
							 {
								 id = t1.Id,
								 firstName = t1.FirstName,
								 lastName = t1.LastName,
								 userName = t1.UserName,
								 email = t1.Email,
								 phoneNumber = t1.PhoneNumber,
								 role = t3.Name

							 }).ToList();

				usersresponse.users = (from t1 in _user
									   join t2 in ou on t1.id equals t2.User_ID
									   select new Users
									   {
										   id = t1.id,
										   firstName = t1.firstName,
										   lastName = t1.lastName,
										   userName = t1.userName,
										   email = t1.email,
										   phoneNumber = t1.phoneNumber,
										   role = t1.role,
										   orgId = t2.Org_ID
									   }).Union<Users>((from t1 in _user
														join t2 in oc on t1.id equals t2.User_ID
														select new Users
														{
															id = t1.id,
															firstName = t1.firstName,
															lastName = t1.lastName,
															userName = t1.userName,
															email = t1.email,
															phoneNumber = t1.phoneNumber,
															role = t1.role,
															orgId = t2.Org_ID,
															customerId = t2.CustomerID
														})).ToList();
				

				if (usersresponse.users.Count == 0)
				{

					return await Task.FromResult(new Response<GetUsersresponse>(usersresponse, "No record found ", false));
				}
				else
				{
					usersresponse.totalNoRecords = usersresponse.users.Count;

					return await Task.FromResult(new Response<GetUsersresponse>(usersresponse, "Success", true));
				}
			}
			catch (Exception ex)
			{

				return await Task.FromResult(new Response<GetUsersresponse>(usersresponse, "Exception", false));
			}


		}


		public async Task<Response<GetUser>> GetUser(string id)
		{

			GetUser usersresponse = new GetUser();
			try
			{
				var user = await _userManager.FindByIdAsync(id);

				if (user == null)
				{

					return await Task.FromResult(new Response<GetUser>(usersresponse, "No record found ", false));
				}
				else
				{
					usersresponse = new GetUser()
					{
						id= user.Id,
						email = user.Email,
						firstName= user.FirstName,
						lastName= user.LastName,
						phoneNumber= user.PhoneNumber
					};

					return await Task.FromResult(new Response<GetUser>(usersresponse, "Success", true));
				}
			}
			catch (Exception ex)
			{

				return await Task.FromResult(new Response<GetUser>(usersresponse, "Exception", false));
			}


		}


		public async Task<Response<int>> UpdateUser(GetUser user)
		{

			GetUser usersresponse = new GetUser();
			try
			{
				ApplicationUser _user = await _userManager.FindByIdAsync(user.id.ToString());

				if (_user == null)
				{

					return await Task.FromResult(new Response<int>(0, "User not found ", false));
				}
				else
				{
					_user.FirstName = user.firstName;
					_user.Email = user.email;
					_user.NormalizedEmail = user.email.ToUpper();
					_user.NormalizedUserName = user.email.ToUpper();
					_user.UserName = user.email;
					_user.LastName = user.lastName;
					_user.PhoneNumber = user.phoneNumber;
					_context.Users.Update(_user);
					await _context.SaveChangesAsync();
					return await Task.FromResult(new Response<int>(1, "Success", true));
				}
			}
			catch (Exception ex)
			{

				return await Task.FromResult(new Response<int>(0, "Exception", false));
			}


		}
	}

}

