using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CCMERP.Domain.Auth
{
    public class AuthenticationResponse
    { 
        public int Id { get; set; }
        public int orgId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public List<CustomerOrganizations> customerOrganizations { get; set; }
    }

   
}
