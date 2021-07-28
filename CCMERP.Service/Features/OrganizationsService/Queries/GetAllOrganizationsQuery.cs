using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Domain.Organizations.Response;
using CCMERP.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.OrganizationsService.Queries
{
    public class GetAllOrganizationsQuery : IRequest<Response<GetAllOrganizationsResponse>>
    {

        public class GetAllCurrencyQueryHandler : IRequestHandler<GetAllOrganizationsQuery, Response<GetAllOrganizationsResponse>>
        {
            private readonly IdentityContext _context;
            public GetAllCurrencyQueryHandler(IdentityContext context)
            {
                _context = context;
            }
            public async Task<Response<GetAllOrganizationsResponse>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
            {

                GetAllOrganizationsResponse getAllOrganizationsResponse = new GetAllOrganizationsResponse();
                try
                {
                    var OrganizationList = await  
                        (
                        from o in _context.Organization
                       
                        select new Organization()
                        {
                            Org_ID = o.Org_ID,
                    Name = o.Name,
                    Address1 = o.Address1,
                    Address2 = o.Address2,
                    City = o.City,
                    State = o.State,
                    CountryID = o.CountryID,
                    Currency_ID = o.Currency_ID,
                    ContactPerson = o.ContactPerson,
                    ContactNumber = o.ContactNumber,
                    VATID = o.VATID,
                    TaxWording = o.TaxWording,
                    LastModifiedBy = o.LastModifiedBy,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedByProgram = o.LastModifiedByProgram,
                    Zipcode = o.Zipcode

                }).Distinct().ToListAsync();
                    if (OrganizationList.Count == 0)
                    {
                        getAllOrganizationsResponse.totalNoRecords = OrganizationList.Count;
                        return new Response<GetAllOrganizationsResponse>(getAllOrganizationsResponse, "No record found ", false);
                    }
                    else
                    {
                        getAllOrganizationsResponse.organizations = OrganizationList;
                        return new Response<GetAllOrganizationsResponse>(getAllOrganizationsResponse, "Success", true);
                    }
                }
                catch (Exception)
                {

                    return new Response<GetAllOrganizationsResponse>(getAllOrganizationsResponse, "Exception", false);
                }


            }
        }



    }
}
