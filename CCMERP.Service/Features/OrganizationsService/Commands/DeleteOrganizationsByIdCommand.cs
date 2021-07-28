using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.OrganizationsService.Commands
{
    public class DeleteOrganizationsByIdCommand : IRequest<Response<int>>
    {
        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Org_ID { get; set; }
        public class DeleteOrganizationsByIdCommandHandler : IRequestHandler<DeleteOrganizationsByIdCommand, Response<int>>
        {
            private readonly IdentityContext _context;
            public DeleteOrganizationsByIdCommandHandler(IdentityContext context)
            {
                _context = context;
            }
            public async Task<Response<int>> Handle(DeleteOrganizationsByIdCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = await _context.Organization.Where(a => a.Org_ID == request.Org_ID).FirstOrDefaultAsync();
                    if (customer == null)
                    {
                        
                        return new Response<int>(0, "No organizations found ", false);
                    }
                    else
                    {
                       

                        return new Response<int>(customer.Org_ID, "Organizations successfully deleted", true);
                    }
                }
                catch (Exception)
                {
                    return new Response<int>(0, "Exception", false);
                }


            }
        }
    }
}
