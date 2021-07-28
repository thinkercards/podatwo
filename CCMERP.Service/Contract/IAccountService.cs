using CCMERP.Domain.Auth;
using CCMERP.Domain.Common;
using System.Threading.Tasks;

namespace CCMERP.Service.Contract
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<ConfirmEmailResponse>> ConfirmEmailAsync(string userId, string code);
        Task<Response<int>> ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<int>> ResetPassword(ResetPasswordRequest model);
        Task<Response<GetUsersresponse>> GetUsers(int orgId = 0, int customerId=0);

        Task<Response<GetUsersresponse>> GetSalesReps(int orgId = 0);
        Task<Response<GetUser>> GetUser(string id);
        Task<Response<AuthenticationResponse>> TwoFactorAuthenticateAsync(TwoFactorAuthenticationRequest request, string ipAddress);
        Task<Response<int>> UpdateUser(GetUser user);
        Task<Response<int>> Resendotp(ForgotPasswordRequest model, string origin);
    }
}
