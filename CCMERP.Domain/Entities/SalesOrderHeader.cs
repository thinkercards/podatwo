using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Entities
{
    public class SalesOrderHeader
	{
		[Key]
		public int SOHdrId { get; set; }
		public int OrgId { get; set; }
		public int CustomerId { get; set; }
		public string SONo { get; set; }
		public DateTime SODate { get; set; }
		public DateTime ExpectedDate { get; set; }
		public string Remarks { get; set; }
		public string ShippingAddress1 { get; set; }
		public string ShippingAddress2 { get; set; }
		public string ShippingCity { get; set; }
		public string ShippingState { get; set; }
		public int ShippingCountry { get; set; }
		[NotMapped]
		public string shippingCountryName { get; set; }
		public string ShippingZipCode { get; set; }
		public string BillingAddress1 { get; set; }
		public string BillingAddress2 { get; set; }
		public string BillingCity { get; set; }
		public string BillingState { get; set; }
		public int BillingCountry { get; set; }
		[NotMapped]
		public string billingCountryName { get; set; }
		public string BillingZipCode { get; set; }
		[NotMapped]
		public string currency { get; set; }
		public int CurrencyId { get; set; }
		public int StatusId { get; set; }
	}
}
