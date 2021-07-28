using CCMERP.Service.Features.GeneralService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


namespace CCMERP.AdminApi.Controllers
{
    [Route("api/V1/General")]
    [ApiController]
    
    public class GeneralController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        [HttpGet("GetAllCurrency")]
        public async Task<IActionResult> GetAllCurrency()
        {
            return Ok(await Mediator.Send(new GetAllCurrencyQuery()));
        }

        [HttpGet("GetAllCountry")]
        public async Task<IActionResult> GetAllCountry()
        {
            return Ok(await Mediator.Send(new GetAllCountryQuery()));
        }
    }
}
