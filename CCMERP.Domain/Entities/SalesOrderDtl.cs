using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class SalesOrderDtl
	{
		[Key]
		public int SODtlId { get; set; }
		public int OrgId { get; set; }
		public int SOHdrId { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public DateTime ExpectedDate { get; set; }
		public string Remarks { get; set; }
		public int StatusId { get; set; }
	}
}
