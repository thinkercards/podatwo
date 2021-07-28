using CCMERP.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CCMERP.Persistence.Seeds
{
    public static class MappingUserRole
    {
        public static List<IdentityUserRole<int>> IdentityUserRoleList()
        {
            return new List<IdentityUserRole<int>>()
            {
                
                new IdentityUserRole<int>
                {
                    RoleId = Constants.SuperAdmin,
                    UserId = Constants.SuperAdminUser
                }
            };
        }
    }
}
