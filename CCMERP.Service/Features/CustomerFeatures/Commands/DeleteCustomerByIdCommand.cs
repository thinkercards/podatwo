using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.CustomerFeatures.Commands
{
    public class DeleteCustomerByIdCommand : IRequest<Response<int>>
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int CustomerID { get; set; }

        public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommand, Response<int>>
        {
            private readonly ITransactionDbContext _context;
            public DeleteCustomerByIdCommandHandler(ITransactionDbContext context)
            {
                _context = context;
            }
            public async Task<Response<int>> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = await _context.Customers.Where(a => a.CustomerID == request.CustomerID).FirstOrDefaultAsync();
                    if (customer == null)
                    {
                        
                        return new Response<int>(0, "No customer found ", false);
                    }
                    else
                    {
                       

                        return new Response<int>(customer.CustomerID, "Customer successfully deleted", true);
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
