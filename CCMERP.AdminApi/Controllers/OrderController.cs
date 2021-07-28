using CCMERP.Service.Features.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


namespace CCMERP.AdminApi.Controllers
{
    [Route("api/v1/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> AddItem([FromBody] CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
