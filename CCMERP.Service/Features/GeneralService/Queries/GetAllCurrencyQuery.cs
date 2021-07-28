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
    public class GetAllCurrencyQuery : IRequest<Response<GetCurrencyResponse>>
    {

        public class GetAllCurrencyQueryHandler : IRequestHandler<GetAllCurrencyQuery, Response<GetCurrencyResponse>>
        {
            private readonly IdentityContext _context;
            public GetAllCurrencyQueryHandler(IdentityContext context)
            {
                _context = context;
            }
            public async Task<Response<GetCurrencyResponse>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
            {

                GetCurrencyResponse getCurrencyResponse = new GetCurrencyResponse();
                try
                {
                    var currencyList = await _context.currency_master.ToListAsync();
                    if (currencyList.Count == 0)
                    {
                        return new Response<GetCurrencyResponse>(getCurrencyResponse, "No record found ", false);
                    }
                    else
                    {
                        getCurrencyResponse.getCurrencyData = currencyList;
                        return new Response<GetCurrencyResponse>(getCurrencyResponse, "Success", true);
                    }
                }
                catch (Exception ex)
                {

                    return new Response<GetCurrencyResponse>(getCurrencyResponse, "Exception", false);
                }


            }
        }


    }
}
