using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class CurrencyMaster
    {
        [Key]
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyABR { get; set; }
        public int CountryID { get; set; }
    }
}
