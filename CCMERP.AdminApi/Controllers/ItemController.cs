using CCMERP.Service.Features.ItemService.Commands;
using CCMERP.Service.Features.ItemService.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.AdminApi.Controllers
{
    [Route("api/v1/Item")]
    public class ItemController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] AddItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteItem( int orgId,int itemId)
        {
            return Ok(await Mediator.Send(new DeleteItemCommand { orgId = orgId,itemId = itemId }));
        }

        [HttpPost("AddCustomerPriceList")]
        public async Task<IActionResult> AddCustomerPriceList([FromBody] AddCustomerPriceListCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet("GetUnitOfMeasurement")]
        public async Task<IActionResult> GetUnitOfMeasurement(int orgID)
        {
            return Ok(await Mediator.Send(new GetUnitOfMeasurementQuery { orgId = 1 }));
        }
        [HttpGet("getallItemByOrgId")]
        public async Task<IActionResult> etallItemByOrgId(int orgId)
        {
            return Ok(await Mediator.Send(new GetallItemByOrgIdQuery { orgId = orgId }));
        }

        [HttpGet("GetallItemById")]
        public async Task<IActionResult> GetallItemById(int orgId,string itemCode=null,int itemId=0)
        {
            return Ok(await Mediator.Send(new GetallItemByIdQuery { orgId = orgId,itemId = itemId,itemCode= itemCode }));
        }

        [HttpGet("CheckItemByCode")]
        public async Task<IActionResult> CheckItemByCode(int orgId, string itemCode)
        {
            return Ok(await Mediator.Send(new CheckItemByCodeQuery { orgId = orgId,  itemCode = itemCode }));
        }

        [HttpPost("uploaditem", Name = "upload")]
        public async Task<IActionResult> UploadFile(int orgId,int createdBy, IFormFile file, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new UploadItemCommand { orgId = orgId,createdBy= createdBy, file = file }));
        }
     
       
    }
}
