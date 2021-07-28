using CCMERP.Domain.AddItem.Request;
using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.ItemService.Commands
{
	public class UploadItemCommand : IRequest<Response<int>>
	{
        public IFormFile file { get; set; }
        public int orgId { get; set; }
        public int createdBy { get; set; }
        public class UploadItemCommandHandler : IRequestHandler<UploadItemCommand, Response<int>>
		{
			private readonly ITransactionDbContext _context;
			public UploadItemCommandHandler(ITransactionDbContext context)
			{
				_context = context;
			}
			public async Task<Response<int>> Handle(UploadItemCommand request, CancellationToken cancellationToken)
			{
				List<ItemMaster> addItemsRequests = new List<ItemMaster>();
				int count = 0;
				try
				{
					//var extension = "." + request.file.FileName.Split('.')[request.file.FileName.Split('.').Length - 1];

					if (request.file.FileName.EndsWith(".csv"))
					{

						using (var sreader = new StreamReader(request.file.OpenReadStream()))
						{
							string[] headers = sreader.ReadLine().Split(',');     //Title
							string[] headers1 = { "ItemCode", "ItemName", "BasePrice", "UOMID", "MinOrderQty" };     //Title

							if (headers.SequenceEqual(headers1))
							{

								while (!sreader.EndOfStream)                          //get all the content in rows 
								{
									string[] rows = sreader.ReadLine().Split(',');
									ItemMaster item = new ItemMaster()
									{
										OrgId = request.orgId,
										ItemCode = rows[0].ToString().Trim(),
										ItemName = rows[1].ToString().Trim(),
										BasePrice = double.Parse(rows[2].ToString().Trim()),
										UOMID = int.Parse(rows[3].ToString().Trim()),
										MinOrderQty = int.Parse(rows[4].ToString().Trim()),
										CreatedDate = DateTime.Now,
										CreatedBy = request.createdBy
									};
									addItemsRequests.Add(item);
								}


								var anyDuplicate = addItemsRequests.GroupBy(x => x.ItemCode).Any(g => g.Count() > 1);

								if (anyDuplicate)
								{
									return new Response<int>(0, "Csv file contains duplicate item entries", false);
								}
								else
								{
									if (addItemsRequests.Count > 0)
									{
										foreach (var item in addItemsRequests)
										{
											var item1 = _context.itemmaster.Where(a => a.OrgId == request.orgId && a.ItemCode == item.ItemCode).FirstOrDefault();
											if (item1 != null)
											{
												item1.ItemCode = item.ItemCode;
												item1.ItemName = item.ItemName;
												item1.UOMID = item.UOMID;
												item1.BasePrice = item.BasePrice;
												item1.MinOrderQty = item.MinOrderQty;
												item1.ModifiedBy = request.createdBy;
												item1.ModifiedDate = DateTime.Now;
												_context.itemmaster.Update(item1);
												await _context.SaveChangesAsync();
												
											}
											else
											{
												ItemMaster itemMaster = new ItemMaster()
												{
													OrgId = request.orgId,
													ItemCode = item.ItemCode,
													ItemName = item.ItemName,
													BasePrice = item.BasePrice,
													UOMID = item.UOMID,
													MinOrderQty = item.MinOrderQty,
													CreatedDate = DateTime.Now,
													CreatedBy = request.createdBy
												};
												_context.itemmaster.Add(itemMaster);
												await _context.SaveChangesAsync();
											}
										}
										count = count +await _context.SaveChangesAsync();

									}
								}

								return new Response<int>(1, "Success", true);
							}
							else
							{
								return new Response<int>(0, "Please upload a valid csv file", true);
							}
							
                        }
                       
					}
                    else
                    {
						return new Response<int>(0, "Please upload a valid csv file", false);
					}
				}
				catch (Exception ex)
				{
					return new Response<int>(0, "Exception", false);
				}
			}
		}
	
	}
	
}
