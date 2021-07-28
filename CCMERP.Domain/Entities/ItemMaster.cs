using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class ItemMaster
    {
        [Key]
        public int ItemId { get; set; }
        public int OrgId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double BasePrice { get; set; }
        public int CatgeoryId { get; set; }
        public int UOMID { get; set; }
        public int MinOrderQty { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
