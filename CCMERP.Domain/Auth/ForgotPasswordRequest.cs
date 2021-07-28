using System.ComponentModel.DataAnnotations;

namespace CCMERP.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
