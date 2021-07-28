using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.CustomerFeatures.Commands
{
    public class SetCustomerSalesRepCommand : IRequest<Response<int>>
	{
		[Required]
		public int customerId { get; set; }

		[Required]
		public int orgId { get; set; }
		[Required]
		public int salesRepId { get; set; }

		public class SetCustomerSalesRepCommandHandler : IRequestHandler<SetCustomerSalesRepCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public SetCustomerSalesRepCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(SetCustomerSalesRepCommand request, CancellationToken cancellationToken)
			{
				try
				{

					var cust = _context.organizationCustomerMappings.Find(request.orgId, request.customerId);

					if (cust == null)
					{
						return new Response<int>(0, "No customer found", true);
					}
					else
					{
						cust.SalesRepId = request.salesRepId;
						
						_context.organizationCustomerMappings.Update(cust);
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

        
