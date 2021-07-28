using CCMERP.Domain.Common;
using CCMERP.Domain.Customers.Response;
using CCMERP.Domain.Entities;
using CCMERP.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace CCMERP.Service.Features.CustomerFeatures.Queries
{
    public class GetAllCustomerQuery : IRequest<Response<GetAllCustomersResponse>>
    {
        public GetAllCustomerQuery()
        {
            salesRepId = 0;
        }
        public int orgId { get; set; }
        public int salesRepId { get; set; }

        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, Response<GetAllCustomersResponse>>
        {
            private readonly ITransactionDbContext _context;
            private readonly IdentityContext _icontext;
            public GetAllCustomerQueryHandler(ITransactionDbContext context, IdentityContext icontext)
            {
                _context = context;
                _icontext = icontext;
            }
            public async Task<Response<GetAllCustomersResponse>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {

                GetAllCustomersResponse customersResponse = new GetAllCustomersResponse();
                List<Customer> customers = new List<Customer>();
                try
                {
                    if (request.salesRepId == 0)
                    {
                        customers = await (from c in _context.Customers
                                           join co in _context.organizationCustomerMappings on c.CustomerID equals co.CustomerID
                                           where co.Org_ID == request.orgId
                                           select new Customer()
                                           {
                                               CustomerID = c.CustomerID,
                                               Name = c.Name,
                                               VatID = c.VatID,
                                               ShippingAddress1 = c.ShippingAddress1,
                                               ShippingAddress2 = c.ShippingAddress2,
                                               ShippingCity = c.ShippingCity,
                                               ShippingState = c.ShippingState,
                                               ShippingCountry = c.ShippingCountry,
                                               ShippingZipCode = c.ShippingZipCode,
                                               BillingAddress1 = c.BillingAddress1,
                                               BillingAddress2 = c.BillingAddress2,
                                               BillingCity = c.BillingCity,
                                               BillingState = c.BillingState,
                                               BillingCountry = c.BillingCountry,
                                               BillingZipCode = c.BillingZipCode,
                                               CurrencyId = c.CurrencyId,
                                               ContactPerson = c.ContactPerson,
                                               ContactPosition = c.ContactPosition,
                                               ContactNumber = c.ContactNumber,
                                               ContactEmail = c.ContactEmail,
                                               orgId = co.Org_ID,
                                               userId = co.User_ID
                                           }).ToListAsync();
                    }
                    else
                    {
                        customers = await (from c in _context.Customers
                                           join co in _context.organizationCustomerMappings on c.CustomerID equals co.CustomerID
                                           where co.Org_ID == request.orgId && co.SalesRepId==request.salesRepId
                                           select new Customer()
                                           {
                                               CustomerID = c.CustomerID,
                                               Name = c.Name,
                                               VatID = c.VatID,
                                               ShippingAddress1 = c.ShippingAddress1,
                                               ShippingAddress2 = c.ShippingAddress2,
                                               ShippingCity = c.ShippingCity,
                                               ShippingState = c.ShippingState,
                                               ShippingCountry = c.ShippingCountry,
                                               ShippingZipCode = c.ShippingZipCode,
                                               BillingAddress1 = c.BillingAddress1,
                                               BillingAddress2 = c.BillingAddress2,
                                               BillingCity = c.BillingCity,
                                               BillingState = c.BillingState,
                                               BillingCountry = c.BillingCountry,
                                               BillingZipCode = c.BillingZipCode,
                                               CurrencyId = c.CurrencyId,
                                               ContactPerson = c.ContactPerson,
                                               ContactPosition = c.ContactPosition,
                                               ContactNumber = c.ContactNumber,
                                               ContactEmail = c.ContactEmail,
                                               orgId = co.Org_ID,
                                               userId = co.User_ID
                                           }).ToListAsync();
                    }
                    if (customers.Count == 0)
                    {
                        customersResponse.totalNoRecords = customers.Count;
                        return new Response<GetAllCustomersResponse>(customersResponse, "No record found ", false);
                    }
                    else
                    {
                        customersResponse.customers = customers;
                        return new Response<GetAllCustomersResponse>(customersResponse, "Success", true);
                    }
                }
                catch (Exception)
                {

                    return new Response<GetAllCustomersResponse>(customersResponse, "Exception", false);
                }


            }
        }
    }
}
