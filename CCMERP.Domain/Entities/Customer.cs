using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCMERP.Domain.Entities
{
    public class Customer 
    {
		[Key]
		public int CustomerID { get; set; }
		public string Name { get; set; }
		public string VatID { get; set; }
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
		public string ContactPerson { get; set; }
		public string ContactPosition { get; set; }
		public string ContactNumber { get; set; }
		public string ContactEmail { get; set; }
		public int IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByUser { get; set; }
		public string CreatedByProgram { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public int LastModifiedBy { get; set; }
		public string LastModifiedByProgram { get; set; }
		public string ExternalReference { get; set; }
		[NotMapped]
		public int orgId { get; set; }

		[NotMapped]
		public int userId { get; set; }

		[NotMapped]
		public string salesExecutivesName { get; set; }
		[NotMapped]
		public string salesExecutivesEmail { get; set; }
	}
}
