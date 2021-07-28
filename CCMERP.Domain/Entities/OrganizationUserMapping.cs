using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class OrganizationUserMapping
    {
        [Key]
        public int Org_ID { get; set; }
        [Key]
        public int User_ID { get; set; }
        [Key]
        public int Role_ID { get; set; }
        public short IsActive { get; set; }
    }
}

