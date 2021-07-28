using System;

namespace CCMERP.Domain.Enum
{
    public enum Roles
    {
        SuperAdmin,
        ClientAdmin,
        SalesRep,
        Customer
    }

    public static class Constants
    {
        public static readonly int SuperAdmin = 500;
        public static readonly int ClientAdmin = 300;
        public static readonly int SalesRep = 200;
        public static readonly int Customer = 100;

        public static readonly int SuperAdminUser = 1;
        public static readonly int CustomerUser = 100;
    }


}
