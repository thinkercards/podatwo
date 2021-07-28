using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Domain.Order.Request;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.Order.Commands
{
    public class CreateOrderCommand : IRequest<Response<int>>
    {
        public CreateOrderCommand()
        {
			sohdrId = 0;

		}

		public int sohdrId { get; set; }
        [Required]
		public int orgId { get; set; }
		[Required]
		public int customerId { get; set; }
        [Required]
        public DateTime sODate { get; set; }
        [Required]
        public DateTime expectedDate { get; set; }
        [Required]
        public string shippingAddress1 { get; set; }
        public string shippingAddress2 { get; set; }
        [Required]
        public string shippingCity { get; set; }
        [Required]
        public string shippingState { get; set; }
        [Required]
        public int shippingCountry { get; set; }
        [Required]
        public string shippingZipCode { get; set; }
        [Required]
        public string billingAddress1 { get; set; }
        public string billingAddress2 { get; set; }
        [Required]
        public string billingCity { get; set; }
        [Required]
        public string billingState { get; set; }
        [Required]
        public int billingCountry { get; set; }
        [Required]
        public string billingZipCode { get; set; }
        [Required]
        public int currencyId { get; set; }
        public List<SalesOrderDtlRequest> salesOrderDtls { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public CreateOrderCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
			{
				int ret = 0;
				try
				{
					SalesOrderHeader salesOrderHeader = new SalesOrderHeader();

					if (request.sohdrId == 0)
					{
						string SNO = await KEYVALUE(request.orgId, 1);

						salesOrderHeader = new SalesOrderHeader()
						{
							SONo = SNO,
							OrgId = request.orgId,
							CustomerId = request.customerId,
							ShippingAddress1 = request.shippingAddress1,
							ShippingAddress2 = request.shippingAddress2,
							ShippingCity = request.shippingCity,
							ShippingState = request.shippingState,
							ShippingCountry = request.shippingCountry,
							ShippingZipCode = request.shippingZipCode,
							BillingAddress1 = request.billingAddress1,
							BillingAddress2 = request.billingAddress2,
							BillingCity = request.billingCity,
							BillingState = request.billingState,
							BillingCountry = request.billingCountry,
							BillingZipCode = request.billingZipCode,
							CurrencyId = request.currencyId,
							ExpectedDate = request.expectedDate,
							SODate = request.sODate,
							StatusId = 1,
						};
						_context.salesorderheader.Add(salesOrderHeader);
						await _context.SaveChangesAsync();
					}
					else
					{
						salesOrderHeader = await _context.salesorderheader.FindAsync(request.sohdrId, request.orgId, request.customerId);
						if (salesOrderHeader != null)
						{
							salesOrderHeader.ShippingAddress1 = request.shippingAddress1;
							salesOrderHeader.ShippingAddress2 = request.shippingAddress2;
							salesOrderHeader.ShippingCity = request.shippingCity;
							salesOrderHeader.ShippingState = request.shippingState;
							salesOrderHeader.ShippingCountry = request.shippingCountry;
							salesOrderHeader.ShippingZipCode = request.shippingZipCode;
							salesOrderHeader.BillingAddress1 = request.billingAddress1;
							salesOrderHeader.BillingAddress2 = request.billingAddress2;
							salesOrderHeader.BillingCity = request.billingCity;
							salesOrderHeader.BillingState = request.billingState;
							salesOrderHeader.BillingCountry = request.billingCountry;
							salesOrderHeader.BillingZipCode = request.billingZipCode;
							salesOrderHeader.CurrencyId = request.currencyId;
							salesOrderHeader.ExpectedDate = request.expectedDate;
							salesOrderHeader.SODate = request.sODate;
						}
						_context.salesorderheader.Update(salesOrderHeader);
						await _context.SaveChangesAsync();

						List<SalesOrderDtl> salesOrderDtls = _context.salesorderdtl.Where(a => a.OrgId == request.orgId && a.SOHdrId == request.sohdrId).ToList();
						_context.salesorderdtl.RemoveRange(salesOrderDtls);
						await _context.SaveChangesAsync();
					}
					foreach(var item in request.salesOrderDtls)
                    {
						SalesOrderDtl orderDtl = new SalesOrderDtl()
						{
							ItemId = item.itemId,
							OrgId = item.orgId,
							ExpectedDate = item.expectedDate,
							Quantity = item.quantity,
							SOHdrId = salesOrderHeader.SOHdrId
						};
						_context.salesorderdtl.Add(orderDtl);
						ret =+ await _context.SaveChangesAsync();
					}
					
					return new Response<int>(ret, "Success", true);
				}
				catch (Exception)
				{
					return new Response<int>(0, "Exception", false);
				}
			}

			public async Task<string> KEYVALUE(int orgId,int keys)
            {
				string SNO = string.Empty;
                try
                {
					KeyMaster keyMaster = _context.keymaster.Find(keys,orgId );
					long keyval = (keyMaster.Value + 1);
					keyMaster.Value = keyval;
					_context.keymaster.Update(keyMaster);
					await _context.SaveChangesAsync();
					SNO = $"{orgId.ToString().PadRight(3, '0')}{keyval.ToString().PadLeft(8, '0')}";

				}
                catch (Exception ex)
                {

                 
                }
				return SNO;

			}

		}
	}
}
