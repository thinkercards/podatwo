using CCMERP.Domain.Auth;
using CCMERP.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMERP.AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string email, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(email, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            return Ok(await _accountService.ForgotPassword(model, Request.Headers["origin"]));
        }
        [HttpPost("Resendotp")]
        public async Task<IActionResult> Resendotp(ForgotPasswordRequest model)
        {
            return Ok(await _accountService.Resendotp(model, GenerateIPAddress()));
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> ResetPassword(int orgId = 0, int customerId = 0)
        {

            return Ok(await _accountService.GetUsers(orgId, customerId));
        }
        [HttpGet("GetSalesReps")]
        public async Task<IActionResult> GetSalesReps(int orgId)
        {
            return Ok(await _accountService.GetSalesReps(orgId)); 
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {

            return Ok(await _accountService.GetUser(id));
        }
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(GetUser model)
        {

            return Ok(await _accountService.UpdateUser(model));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {

            return Ok(await _accountService.ResetPassword(model));
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        [HttpPost("twofactorauthenticateasync")]
        public async Task<IActionResult> TwoFactorAuthenticateAsync(TwoFactorAuthenticationRequest request)
        {
            return Ok(await _accountService.TwoFactorAuthenticateAsync(request, GenerateIPAddress()));
        }
    }
}
