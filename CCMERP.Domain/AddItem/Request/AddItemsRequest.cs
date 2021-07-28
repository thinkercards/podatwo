using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.AddItem.Request
{
    public class AddItemsRequest
    {
        public int itemid { get; set; }
        public int orgid { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public double basePrice { get; set; }
        public int uomid { get; set; }
        [NotMapped]
        public string uomName { get; set; }
        public int minOrderQty { get; set; }
        public int createdBy { get; set; }
    }
}
