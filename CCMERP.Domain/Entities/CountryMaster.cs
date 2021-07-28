using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class CountryMaster
    {
        [Key]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string ABR { get; set; }
        public int IsActive { get; set; }
        public int CountryCode { get; set; }
    }
}
