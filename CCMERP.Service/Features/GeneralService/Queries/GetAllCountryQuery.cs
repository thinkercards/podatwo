using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Domain.General.Response;
using CCMERP.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.GeneralService.Queries
{
    public class GetAllCountryQuery : IRequest<Response<GetCountryResponse>>
    {
        public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, Response<GetCountryResponse>>
        {
            private readonly IdentityContext _context;
            public GetAllCountryQueryHandler(IdentityContext context)
            {
                _context = context;
            }
            public async Task<Response<GetCountryResponse>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
            {
 
                 GetCountryResponse getCountryResponse = new GetCountryResponse();
                try
                {
                    var CountryList = await _context.country_master.ToListAsync();
                    if (CountryList.Count == 0)
                    {
                        return new Response<GetCountryResponse>(getCountryResponse, "No record found ", false);
                    }
                    else
                    {
                        getCountryResponse.getCountry = CountryList;
                        return new Response<GetCountryResponse>(getCountryResponse, "Success", true);
                    }
                }
                catch (Exception ex)
                {

                    return new Response<GetCountryResponse>(getCountryResponse, "Exception", false);
                }
                
               
            }
        }

    }
}
