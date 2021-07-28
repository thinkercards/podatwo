using CCMERP.Domain.AddItem.Request;
using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
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
    public class AddItemCommand : IRequest<Response<int>>
    {
        public List<AddItemsRequest> addItemsRequests { get; set; }
		public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public AddItemCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(AddItemCommand request, CancellationToken cancellationToken)
			{
				try
				{
					if (request.addItemsRequests.Count > 0)
					{
						foreach (var item in request.addItemsRequests)
						{
							ItemMaster itemMaster = new ItemMaster()
							{
								 OrgId = item.orgid,
								 ItemCode = item.itemCode,
								 ItemName = item.itemName,
								BasePrice = item.basePrice,

								UOMID = item.uomid,
								MinOrderQty = item.minOrderQty,
								CreatedDate = DateTime.Now,
								CreatedBy = item.createdBy

							};
							_context.itemmaster.Add(itemMaster);
							await _context.SaveChangesAsync();

						}
					}
					

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
				catch (Exception ex)
				{
					return new Response<int>(0, "Exception", false);
				}
			}
		}
	}
}
