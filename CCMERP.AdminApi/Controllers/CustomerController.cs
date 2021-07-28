using CCMERP.Service.Features.CustomerFeatures.Commands;
using CCMERP.Service.Features.CustomerFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using static CCMERP.Service.Features.CustomerFeatures.Commands.SetCustomerSalesRepCommand;

namespace CCMERP.AdminApi.Controllers
{
    [ApiController]
    [Route("api/v1/Customer")]
    public class CustomerController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int orgId, int salesRepId=0)
        {
            return Ok(await Mediator.Send(new GetAllCustomerQuery { orgId = orgId , salesRepId = salesRepId }));
        }

        [HttpGet("{customerID}")]
        public async Task<IActionResult> GetById(int customerID)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdQuery { customerID = customerID }));
        }

        [HttpDelete("{customerID}")]
        public async Task<IActionResult> Delete(int customerID)
        {
            return Ok(await Mediator.Send(new DeleteCustomerByIdCommand { CustomerID = customerID }));
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("SetCustomerSalesRep")]
        public async Task<IActionResult> SetCustomerSalesRep(SetCustomerSalesRepCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
