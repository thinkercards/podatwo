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
    public class GetallItemByIdQuery : IRequest<Response<GetallItemByIdResponse>>
    {

        public int itemId { get; set; }
        public int orgId { get; set; }
        public string itemCode { get; set; }
        public class GetallItemByIdQueryHandler : IRequestHandler<GetallItemByIdQuery, Response<GetallItemByIdResponse>>
        {
            private readonly TransactionDbContext _context;
            public GetallItemByIdQueryHandler(TransactionDbContext context)
            {
                _context = context;
            }
            public async Task<Response<GetallItemByIdResponse>> Handle(GetallItemByIdQuery request, CancellationToken cancellationToken)
            {

                GetallItemByIdResponse GetallItemByIdResponse = new GetallItemByIdResponse();
                try
                {

                  
                    if (request.itemId == 0) {

                        var item1 = _context.itemmaster.Where(a => a.OrgId == request.orgId && a.ItemCode == request.itemCode).FirstOrDefault();
                        if(item1!=null)
                            GetallItemByIdResponse.item =  new AddItemsRequest()
                        {
                              
                            itemid = item1.ItemId,
                            itemCode = item1.ItemCode,
                            itemName = item1.ItemName,
                            uomid = item1.UOMID,
                                uomName = (_context.uOMMaster.Where(b => b.UOMId == item1.UOMID).FirstOrDefault().Name),
                                basePrice = item1.BasePrice,
                            minOrderQty = item1.MinOrderQty,
                            orgid = item1.OrgId
                        };
                    }
                    else
                    {
                        var item1 = _context.itemmaster.Where(a => a.OrgId == request.orgId && a.ItemId == request.itemId).FirstOrDefault();
                        if (item1 != null)
                            GetallItemByIdResponse.item = new AddItemsRequest()
                            {

                                itemid = item1.ItemId,
                                itemCode = item1.ItemCode,
                                itemName = item1.ItemName,
                                uomid = item1.UOMID,
                                uomName = (_context.uOMMaster.Where(b => b.UOMId == item1.UOMID).FirstOrDefault().Name),
                                basePrice = item1.BasePrice,
                                minOrderQty = item1.MinOrderQty,
                                orgid = item1.OrgId
                            };
                    }

                    if (GetallItemByIdResponse.item == null)
                    {
                        //getAllOrganizationsResponse.totalNoRecords = OrganizationList.Count;
                        return await Task.FromResult(new Response<GetallItemByIdResponse>(GetallItemByIdResponse, "No record found ", false));
                    }
                    else
                    {

                        return await Task.FromResult(new Response<GetallItemByIdResponse>(GetallItemByIdResponse, "Success", true));
                    }
                }
                catch (Exception)
                {

                    return await Task.FromResult(new Response<GetallItemByIdResponse>(GetallItemByIdResponse, "Exception", false));
                }


            }
        }

    }
}