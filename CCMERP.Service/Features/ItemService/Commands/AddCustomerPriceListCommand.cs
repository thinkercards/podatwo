using CCMERP.Domain.AddItem.Request;
using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.ItemService.Commands
{
    public class AddCustomerPriceListCommand : IRequest<Response<int>>
	{
		public List<AddCustomerPriceListRequest> addCustomerPriceListRequests { get; set; }
		public class AddCustomerPriceListCommandHandler : IRequestHandler<AddCustomerPriceListCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public AddCustomerPriceListCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(AddCustomerPriceListCommand request, CancellationToken cancellationToken)
			{
				try
				{



					//_context.Customers.Add(customer);
					//await _context.SaveChangesAsync();

					//OrganizationCustomerMapping organizationCustomerMapping = new OrganizationCustomerMapping()
					//{
					//	CustomerID = customer.CustomerID,
					//	Org_ID = request.Org_ID,
					//	IsActive = 0,
					//	User_ID = 0
					//};
					//_context.organizationCustomerMappings.Add(organizationCustomerMapping);
					await _context.SaveChangesAsync();
					return new Response<int>(1, "Success", true);
				}
				catch (Exception)
				{
					return new Response<int>(0, "Exception", false);
				}
			}
		}
	}
}
