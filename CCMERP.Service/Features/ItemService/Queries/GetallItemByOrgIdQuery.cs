using CCMERP.Domain.AddItem.Request;
using CCMERP.Domain.AddItem.Response;
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

namespace CCMERP.Service.Features.ItemService.Queries
{
    public class GetallItemByOrgIdQuery : IRequest<Response<GetallItemByOrgIdResponse>>
    {
        public int orgId { get; set; }
        public class GetallItemByOrgIdQueryHandler : IRequestHandler<GetallItemByOrgIdQuery, Response<GetallItemByOrgIdResponse>>
        {
            private readonly TransactionDbContext _context;
            public GetallItemByOrgIdQueryHandler(TransactionDbContext context)
            {
                _context = context;
            }
            public async Task<Response<GetallItemByOrgIdResponse>> Handle(GetallItemByOrgIdQuery request, CancellationToken cancellationToken)
            {

                GetallItemByOrgIdResponse GetallItemByOrgIdResponse = new GetallItemByOrgIdResponse();
                try
                {
                    GetallItemByOrgIdResponse.items = _context.itemmaster.Where(a => a.OrgId == request.orgId).Select(a=> new AddItemsRequest() { 
                    
                    itemid=a.ItemId,
                    itemCode=a.ItemCode,
                    itemName=a.ItemName,
                    uomid=a.UOMID,
                    uomName = (_context.uOMMaster.Where(b=>b.UOMId==a.UOMID).FirstOrDefault().Name),
                    basePrice=a.BasePrice,
                    minOrderQty=a.MinOrderQty,
                    orgid=a.OrgId

                    }).ToList();


                    if (GetallItemByOrgIdResponse.items.Count == 0)
                    {
                        //getAllOrganizationsResponse.totalNoRecords = OrganizationList.Count;
                        return await Task.FromResult(new Response<GetallItemByOrgIdResponse>(GetallItemByOrgIdResponse, "No record found ", false));
                    }
                    else
                    {

                        return await Task.FromResult(new Response<GetallItemByOrgIdResponse>(GetallItemByOrgIdResponse, "Success", true));
                    }
                }
                catch (Exception)
                {

                    return await Task.FromResult(new Response<GetallItemByOrgIdResponse>(GetallItemByOrgIdResponse, "Exception", false));
                }


            }
        }

    }
}
