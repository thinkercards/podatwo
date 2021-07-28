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
    public class DeleteItemCommand : IRequest<Response<int>>
	{
		public int itemId { get; set; }
		public int orgId { get; set; }
		public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public DeleteItemCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
			{
				try
				{
					var item1 = _context.itemmaster.Where(a => a.OrgId == request.orgId && a.ItemId == request.itemId).FirstOrDefault();
					if (item1 != null)
					{
						_context.itemmaster.Remove(item1);
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

