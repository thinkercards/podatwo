using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Order.Request
{
    public class SalesOrderDtlRequest
    {
		public int orgId { get; set; }
		public int itemId { get; set; }
		public int quantity { get; set; }
		public DateTime expectedDate { get; set; }
	}
}
