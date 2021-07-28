using CCMERP.Service.Features.OrganizationsService.Commands;
using CCMERP.Service.Features.OrganizationsService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


namespace CCMERP.AdminApi.Controllers
{
    [Route("api/V1/Organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        [HttpPost]
        public async Task<IActionResult> Create(CreateOrganizationsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllOrganizationsQuery()));
        }

        [HttpGet("{orgID}")]
        public async Task<IActionResult> GetById(int orgID)
        {
            return Ok(await Mediator.Send(new GetorganizationByIdQuery { orgID = orgID }));
        }

        [HttpDelete("{orgID}")]
        public async Task<IActionResult> Delete(int orgID)
        {
            return Ok(await Mediator.Send(new DeleteOrganizationsByIdCommand { Org_ID = orgID }));
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrganizationsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
