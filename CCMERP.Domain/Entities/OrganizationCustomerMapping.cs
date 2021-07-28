using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class OrganizationCustomerMapping
    {
        [Key]
        public int Org_ID { get; set; }
        public int User_ID { get; set; }
        [Key]
        public int CustomerID { get; set; }
        public int SalesRepId { get; set; }
        public short IsActive { get; set; }
    }
}
