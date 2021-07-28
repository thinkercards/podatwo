using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
namespace CCMERP.Service.Features.OrganizationsService.Queries
{
    public class GetorganizationByIdQuery : IRequest<Response<Organization>>
    {
        public int orgID { get; set; }
        public  class GetorganizationByIdQueryHandler : IRequestHandler<GetorganizationByIdQuery, Response<Organization>>
        {
            private readonly IdentityContext _context;
            public GetorganizationByIdQueryHandler(IdentityContext context)
            {
                _context = context;
            }
            public async Task<Response<Organization>> Handle(GetorganizationByIdQuery request, CancellationToken cancellationToken)
            {

                Organization organization = new Organization();
                try
                {
                    organization = 

                    organization = (from t1 in _context.Organization
                                 join t2 in _context.country_master on t1.CountryID equals t2.CountryID
                                 join t3 in _context.currency_master on t1.Currency_ID equals t3.CurrencyID where t1.Org_ID == request.orgID
                                    select new Organization {
                                     Org_ID=t1.Org_ID,
                                     Name = t1.Name,
                                     Address1 = t1.Address1,
                                     Address2 = t1.Address2,
                                     City = t1.City,
                                     State = t1.State,
                                     CountryID = t1.CountryID,
                                     Currency_ID = t1.Currency_ID,
                                     ContactPerson = t1.ContactPerson,
                                     ContactNumber = t1.ContactNumber,
                                     VATID = t1.VATID,
                                     TaxWording = t1.TaxWording,
                                     CreatedByUser = t1.CreatedByUser,
                                     ExternalReference = t1.ExternalReference,
                                     CreatedByProgram = t1.CreatedByProgram,
                                     Comments = t1.Comments,
                                     Zipcode = t1.Zipcode,
                                     country = t2.CountryName,
                                     currency = t3.CurrencyName

                                 }).FirstOrDefault();

                    if (organization != null)
                    {
                        return await Task.FromResult(new Response<Organization>(organization, "Success", true));
                    }
                    else
                    {


                        return await Task.FromResult(new Response<Organization>(organization, "No record found ", false));
                    }
                }
                catch (Exception)
                {

                    return await Task.FromResult(new Response<Organization>(organization, "Exception", false));
                }

            }
        }
    }
}
