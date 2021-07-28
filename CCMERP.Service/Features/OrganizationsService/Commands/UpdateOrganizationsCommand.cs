using CCMERP.Domain.Common;
using CCMERP.Persistence;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.OrganizationsService.Commands
{
    public class UpdateOrganizationsCommand : IRequest<Response<int>>
    {
        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Org_ID { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(49)]
        public string Name { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Address1 { get; set; }

        [MaxLength(100)]
        public string Address2 { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string State { get; set; }

        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int CountryID { get; set; }

        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int Currency_ID { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string ContactPerson { get; set; }


        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string ContactNumber { get; set; }


        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string VATID { get; set; }


        [MaxLength(45)]
        public string TaxWording { get; set; }

        [Required]
        [MaxLength(15)]
        public string Zipcode { get; set; }

        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int LastModifiedBy { get; set; }
        [MaxLength(45)]
        public string LastModifiedByProgram { get; set; }
        public class UpdateOrganizationsCommandHandler : IRequestHandler<UpdateOrganizationsCommand, Response<int>>
        {
            private readonly IdentityContext _context;
            public UpdateOrganizationsCommandHandler(IdentityContext context)
            {
                _context = context;
            }
            public async Task<Response<int>> Handle(UpdateOrganizationsCommand request, CancellationToken cancellationToken)
            {
                try
                {

                
                var org = _context.Organization.Where(a => a.Org_ID == request.Org_ID).FirstOrDefault();

                if (org == null)
                {
                        return new Response<int>(0, "No Organization found", true);
                }
                else
                {
                          org.Name = request.Name;
                          org.Address1 = request.Address1;
                          org.Address2 = request.Address2;
                          org.City = request.City;
                          org.State = request.State;
                          org.CountryID = request.CountryID;
                          org.Currency_ID = request.Currency_ID;
                          org.ContactPerson = request.ContactPerson;
                          org.ContactNumber = request.ContactNumber;
                          org.VATID = request.VATID;
                          org.TaxWording = request.TaxWording;
                          org.LastModifiedBy = request.LastModifiedBy;
                          org.LastModifiedDate = DateTime.Now;
                          org.LastModifiedByProgram = request.LastModifiedByProgram;
                          org.Zipcode = request.Zipcode;
                    _context.Organization.Update(org);
                    await _context.SaveChangesAsync();

                        return new Response<int>(org.Org_ID, "Success", true);
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
