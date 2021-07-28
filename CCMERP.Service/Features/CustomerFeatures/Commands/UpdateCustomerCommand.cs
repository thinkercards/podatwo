using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.CustomerFeatures.Commands
{
    public class UpdateCustomerCommand : IRequest<Response<int>>
	{
		[Required]
		public int CustomerID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string VatID { get; set; }
		[Required]
		public string ShippingAddress1 { get; set; }
		public string ShippingAddress2 { get; set; }
		[Required]
		public string ShippingCity { get; set; }
		[Required]
		public string ShippingState { get; set; }
		[Required]
		public int ShippingCountry { get; set; }
		[Required]
		public string ShippingZipCode { get; set; }
		[Required]
		public string BillingAddress1 { get; set; }

		public string BillingAddress2 { get; set; }
		[Required]
		public string BillingCity { get; set; }
		[Required]
		public string BillingState { get; set; }
		[Required]
		public int BillingCountry { get; set; }
		[Required]
		public string BillingZipCode { get; set; }
		[Required]
		public int CurrencyId { get; set; }
		[Required]
		public string ContactPerson { get; set; }
		[Required]
		public string ContactPosition { get; set; }
		[Required]
		public string ContactNumber { get; set; }
		[Required]
		public string ContactEmail { get; set; }
		[Required]
		public int LastModifiedBy { get; set; }
		public string LastModifiedByProgram { get; set; }

		public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<int>>
        {
            private readonly ITransactionDbContext _context;
            public UpdateCustomerCommandHandler(ITransactionDbContext context)
            {
                _context = context;
            }
            public async Task<Response<int>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                try
                {

                
                var cust = _context.Customers.Where(a => a.CustomerID == request.CustomerID).FirstOrDefault();

                if (cust == null)
                {
                        return new Response<int>(0, "No customer found", true);
                    }
                else
                {
						cust.Name = request.Name;
						cust.VatID = request.VatID;
						cust.ShippingAddress1 = request.ShippingAddress1;
						cust.ShippingAddress2 = request.ShippingAddress2;
						cust.ShippingCity = request.ShippingCity;
						cust.ShippingState = request.ShippingState;
						cust.ShippingCountry = request.ShippingCountry;
						cust.ShippingZipCode = request.ShippingZipCode;
						cust.BillingAddress1 = request.BillingAddress1;
						cust.BillingAddress2 = request.BillingAddress2;
						cust.BillingCity = request.BillingCity;
						cust.BillingState = request.BillingState;
						cust.BillingCountry = request.BillingCountry;
						cust.BillingZipCode = request.BillingZipCode;
						cust.CurrencyId = request.CurrencyId;
						cust.ContactPerson = request.ContactPerson;
						cust.ContactPosition = request.ContactPosition;
						cust.ContactNumber = request.ContactNumber;
						cust.ContactEmail = request.ContactEmail;
						cust.LastModifiedBy = request.LastModifiedBy;
						cust.LastModifiedByProgram = request.LastModifiedByProgram;
						cust.LastModifiedDate =DateTime.Now;

					_context.Customers.Update(cust);
                    await _context.SaveChangesAsync();


                        return new Response<int>(cust.CustomerID, "Success", true);
                    }
                }
                catch (Exception)
                {

					return new Response<int>(0, "Exception", false);
				}
            }
        }
    }
}
