using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CCMERP.Domain.Auth
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
    public class Useauthrtokens
    {
        public int UserId { get; set; }
        public int Status { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string IpAddress { get; set; }
        public string expires { get; set; }
    }
    public class Users 
    {
        public int id { get; set; }
        public int orgId { get; set; }
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }
     
    }

    public class GetUser
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

    }

    public class GetUsersresponse
    {
        
            public int totalNoRecords { get; set; }
        public List<Users> users { get; set; }
    }

}