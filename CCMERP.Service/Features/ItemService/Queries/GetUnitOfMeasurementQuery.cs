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
    public class GetUnitOfMeasurementQuery : IRequest<Response<GetUOMMasterResponse>>
    {
        public int orgId { get; set; }
        public class GetUnitOfMeasurementQueryHandler : IRequestHandler<GetUnitOfMeasurementQuery, Response<GetUOMMasterResponse>>
        {
            private readonly TransactionDbContext _context;
            public GetUnitOfMeasurementQueryHandler(TransactionDbContext context)
            {
                _context = context;
            }
            public async Task<Response<GetUOMMasterResponse>> Handle(GetUnitOfMeasurementQuery request, CancellationToken cancellationToken)
            {

                GetUOMMasterResponse getUOMMasterResponse = new GetUOMMasterResponse();
                try
                {
                    getUOMMasterResponse.uOMs =  _context.uOMMaster.Where(a => a.IsActive == 1 && a.OrgId== request.orgId).ToList();


                    if (getUOMMasterResponse.uOMs.Count == 0)
                    {
                        //getAllOrganizationsResponse.totalNoRecords = OrganizationList.Count;
                        return await Task.FromResult(new Response<GetUOMMasterResponse>(getUOMMasterResponse, "No record found ", false));
                    }
                    else
                    {
                       
                        return await Task.FromResult(new Response<GetUOMMasterResponse>(getUOMMasterResponse, "Success", true));
                    }
                }
                catch (Exception)
                {

                    return await Task.FromResult(new Response<GetUOMMasterResponse>(getUOMMasterResponse, "Exception", false));
                }


            }
        }

    }
}
