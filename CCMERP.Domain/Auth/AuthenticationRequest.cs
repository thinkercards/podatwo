
using Newtonsoft.Json;

namespace CCMERP.Domain.Auth
{
    public class AuthenticationRequest
    {
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
    }

    public class TwoFactorAuthenticationRequest
    {
        [JsonProperty(PropertyName = "otp")]
        public string otp { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    }
}
