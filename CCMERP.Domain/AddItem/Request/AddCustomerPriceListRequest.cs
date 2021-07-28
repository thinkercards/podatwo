using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.AddItem.Request
{
    public class AddCustomerPriceListRequest
    {
        public string itemCode { get; set; }
        public int customerId { get; set; }
        public int orgid { get; set; }
        public double basePrice { get; set; }
        public int createdBy { get; set; }
    }
}
