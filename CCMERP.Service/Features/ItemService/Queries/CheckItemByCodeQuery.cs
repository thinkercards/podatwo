using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.ItemService.Queries
{
	 public class CheckItemByCodeQuery : IRequest<Response<int>>
	{

		public int orgId { get; set; }
		public string itemCode { get; set; }
		public class CheckItemByCodeQueryHandler : IRequestHandler<CheckItemByCodeQuery, Response<int>>
		{
			private readonly TransactionDbContext _context;
			public CheckItemByCodeQueryHandler(TransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(CheckItemByCodeQuery request, CancellationToken cancellationToken)
			{

			  
					try
					{
						var item1 = _context.itemmaster.Where(a => a.OrgId == request.orgId && a.ItemCode == request.itemCode).FirstOrDefault();
						if (item1 != null) {
						return await Task.FromResult(new Response<int>(1, "Success", true));
						
					}
					else
					{
						return await Task.FromResult(new Response<int>(0, "No record found ", false));

					}

				   
				}
				catch (Exception)
				{

					return await Task.FromResult(new Response<int>(0, "Exception", false));
				}


			}
		}

	}
}