using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Persistence;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.CustomerFeatures.Commands
{
    public class CreateCustomerCommand : IRequest<Response<int>>
	{
		[Required]
		public int Org_ID { get; set; }
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
		public int CreatedByUser { get; set; }
		public string CreatedByProgram { get; set; }
		public string ExternalReference { get; set; }
		public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<int>>
        {
            private readonly ITransactionDbContext _context;
            public CreateCustomerCommandHandler(ITransactionDbContext context)
            {
                _context = context;
            }
            public async Task<Response<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                try
                {

                
				var customer = new Customer()
				{
				    Name = request.Name,
					VatID = request.VatID,
					ShippingAddress1 = request.ShippingAddress1,
					ShippingAddress2 = request.ShippingAddress2,
					ShippingCity = request.ShippingCity,
					ShippingState = request.ShippingState,
					ShippingCountry = request.ShippingCountry,
					ShippingZipCode = request.ShippingZipCode,
					BillingAddress1 = request.BillingAddress1,
					BillingAddress2 = request.BillingAddress2,
					BillingCity = request.BillingCity,
					BillingState = request.BillingState,
					BillingCountry = request.BillingCountry,
					BillingZipCode = request.BillingZipCode,
					CurrencyId = request.CurrencyId,
					ContactPerson = request.ContactPerson,
					ContactPosition = request.ContactPosition,
					ContactNumber = request.ContactNumber,
					ContactEmail = request.ContactEmail,
					CreatedByUser = request.CreatedByUser,
					CreatedDate = DateTime.Now,
					CreatedByProgram = request.CreatedByProgram,
					IsActive=1
				};
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

					OrganizationCustomerMapping organizationCustomerMapping = new OrganizationCustomerMapping()
					{
						CustomerID = customer.CustomerID,
						Org_ID = request.Org_ID,
						SalesRepId = 0,
						IsActive = 0,
						User_ID=0
				    };

				_context.organizationCustomerMappings.Add(organizationCustomerMapping);
				await _context.SaveChangesAsync();

					

					return new Response<int>(customer.CustomerID, "Success", true);
				}
				catch (Exception ex)
				{
					return new Response<int>(0, "Exception", false);
				}
			}
        }
    }
}
