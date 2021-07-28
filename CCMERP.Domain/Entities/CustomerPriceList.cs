using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class CustomerPriceList
    {
        [Key]
        public int ItemId { get; set; }
        [Key]
        public int CustomerId { get; set; }
        public int Orgid { get; set; }
        public double BasePrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int IsActive { get; set; }
    }
}
