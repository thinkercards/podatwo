using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class UOMMaster
    {
        public int OrgId { get; set; }
        [Key]
        public int UOMId { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}
