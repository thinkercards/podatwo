using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.CustomerFeatures.Queries
{
    public class GetCustomerByIdQuery : IRequest<Response<Customer>>
    {
        public int customerID { get; set; }
        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response<Customer>>
        {
            private readonly ITransactionDbContext _context;
            private readonly IdentityContext _icontext;
            public GetCustomerByIdQueryHandler(ITransactionDbContext context, IdentityContext icontext)
            {
                _context = context;
                _icontext = icontext;
            }
            public async Task<Response<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {

                Customer customer = new Customer();
                try
                {
                    //customer = _context.Customers.Where(a => a.CustomerID == request.customerID).FirstOrDefault();

                    var country = _icontext.country_master.ToList();
                    var curency = _icontext.currency_master.ToList();
                    var cust = _context.Customers.ToList();
                    var org = _context.organizationCustomerMappings.ToList();


                    customer = (from t1 in cust
                                join t2 in country on t1.BillingCountry equals t2.CountryID
                                join t3 in country on t1.ShippingCountry equals t3.CountryID
                                join t5 in org on t1.CustomerID equals t5.CustomerID
                                    join t4 in curency on t1.CurrencyId equals t4.CurrencyID where t1.CustomerID== request.customerID
                                select new Customer
                                    {
                                        Name = t1.Name,
                                        VatID = t1.VatID,
                                        ShippingAddress1 = t1.ShippingAddress1,
                                        ShippingAddress2 = t1.ShippingAddress2,
                                        ShippingCity = t1.ShippingCity,
                                        ShippingState = t1.ShippingState,
                                        ShippingCountry = t1.ShippingCountry,
                                        shippingCountryName = t3.CountryName,
                                        ShippingZipCode = t1.ShippingZipCode,
                                        BillingAddress1 = t1.BillingAddress1,
                                        BillingAddress2 = t1.BillingAddress2,
                                        BillingCity = t1.BillingCity,
                                        BillingState = t1.BillingState,
                                        BillingCountry = t1.BillingCountry,
                                        billingCountryName = t2.CountryName,
                                        BillingZipCode = t1.BillingZipCode,
                                        CurrencyId = t1.CurrencyId,
                                        ContactPerson = t1.ContactPerson,
                                        ContactPosition = t1.ContactPosition,
                                        ContactNumber = t1.ContactNumber,
                                        ContactEmail = t1.ContactEmail,
                                        CreatedByUser = t1.CreatedByUser,
                                        CreatedDate = DateTime.Now,
                                        CreatedByProgram = t1.CreatedByProgram,
                                        currency=t4.CurrencyName,
                                        CustomerID=t1.CustomerID,
                                        salesExecutivesEmail = (t5.SalesRepId!=0? _icontext.Users.Where(a=>a.Id== t5.SalesRepId).FirstOrDefault().Email:""),
                                        salesExecutivesName = (t5.SalesRepId!=0? _icontext.Users.Where(a=>a.Id== t5.SalesRepId).FirstOrDefault().FirstName:"")

                                }).FirstOrDefault();


                    if (customer != null)
                    {
                        return await Task.FromResult(new Response<Customer>(customer, "Success", true));
                       
                    }
                    else
                    {
                        return await Task.FromResult(new Response<Customer>(customer, "No record found ", false));

                    }
                }
                catch (Exception)
                {

                    return await Task.FromResult(new Response<Customer>(customer, "Exception", false));
                }


            }
        }
    }
}
