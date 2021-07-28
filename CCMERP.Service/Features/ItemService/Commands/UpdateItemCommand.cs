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
    public class UpdateItemCommand : IRequest<Response<int>>
	{
		public AddItemsRequest item { get; set; }
		public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public UpdateItemCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
			{
				try
				{



					var item1 = _context.itemmaster.Where(a => a.OrgId == request.item.orgid && a.ItemId == request.item.itemid && a.ItemCode == request.item.itemCode).FirstOrDefault();
					if (item1 != null)
					{

						item1.ItemCode = request.item.itemCode;
						item1.ItemName = request.item.itemName;
						item1.UOMID = request.item.uomid;
						item1.BasePrice = request.item.basePrice;
						item1.MinOrderQty = request.item.minOrderQty;
						item1.ModifiedBy = request.item.createdBy;
						item1.ModifiedDate = DateTime.Now;
						_context.itemmaster.Update(item1);
						await _context.SaveChangesAsync();
						return new Response<int>(1, "Success", true);
                    }
                    else
                    {
						return new Response<int>(0, "No item found", false);

					}
				}
				catch (Exception ex)
				{
					return new Response<int>(0, "Exception", false);
				}
			}
		}
	}
}
