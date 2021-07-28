using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class KeyMaster
    {
        [Key]
        public int KeyId { get; set; }
        [Key]
        public int OrgId { get; set; }
        public long Value { get; set; }
        public string Description { get; set; }
    }
}
