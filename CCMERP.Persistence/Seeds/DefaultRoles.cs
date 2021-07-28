using CCMERP.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CCMERP.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static List<IdentityRole<int>> IdentityRoleList()
        {
            return new List<IdentityRole<int>>()
            {
                new IdentityRole<int>
                {
                    Id = Constants.SuperAdmin,
                    Name = Roles.SuperAdmin.ToString(),
                    NormalizedName = Roles.SuperAdmin.ToString()
                },
                new IdentityRole<int>
                {
                    Id = Constants.ClientAdmin,
                    Name = Roles.ClientAdmin.ToString(),
                    NormalizedName = Roles.ClientAdmin.ToString()
                },
                new IdentityRole<int>
                {
                    Id = Constants.SalesRep,
                    Name = Roles.SalesRep.ToString(),
                    NormalizedName = Roles.SalesRep.ToString()
                },
                new IdentityRole<int>
                {
                    Id = Constants.Customer,
                    Name = Roles.Customer.ToString(),
                    NormalizedName = Roles.Customer.ToString()
                }
            };
        }
    }
}
