using CCMERP.Domain.Common;
using CCMERP.Domain.Entities;
using CCMERP.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMERP.Service.Features.OrganizationsService.Commands
{
    public class CreateOrganizationsCommand : IRequest<Response<int>>
    {
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
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[RegularExpression("([1-9]+)", ErrorMessage = "Please enter valid Number")]
        public int CreatedByUser { get; set; }
       
        [MaxLength(45)]
        public string CreatedByProgram { get; set; }
        
        [MaxLength(50)]
        public string ExternalReference { get; set; }

        
        [MaxLength(150)]
        public string Comments { get; set; }

        [Required]
        [MaxLength(15)]
        public string Zipcode { get; set; }

        public class CreateOrganizationsCommandHandler : IRequestHandler<CreateOrganizationsCommand, Response<int>>
        {
            private readonly IdentityContext _context;
            private readonly ITransactionDbContext _tcontext;
            public CreateOrganizationsCommandHandler(IdentityContext context, ITransactionDbContext tcontext)
            {
                _context = context;
                _tcontext = tcontext;
            }
            public async Task<Response<int>> Handle(CreateOrganizationsCommand request, CancellationToken cancellationToken)
            {

                try
                {

                    var org = _context.Organization.Where(a => a.Name.ToLower() == request.Name.ToLower()).ToList();
                    if (org.Count >0)
                    {
                        return new Response<int>(0, "An organization with the same name already exists", false);
                    }
                    else
                    {

                        Organization organization = new Organization()
                        {
                            Name = request.Name,
                            Address1 = request.Address1,
                            Address2 = request.Address2,
                            City = request.City,
                            State = request.State,
                            CountryID = request.CountryID,
                            Currency_ID = request.Currency_ID,
                            ContactPerson = request.ContactPerson,
                            ContactNumber = request.ContactNumber,
                            VATID = request.VATID,
                            TaxWording = request.TaxWording,
                            CreatedByUser = request.CreatedByUser,
                            CreatedDate = DateTime.Now,
                            ExternalReference = request.ExternalReference,
                            CreatedByProgram = request.CreatedByProgram,
                            Comments = request.Comments,
                            Zipcode = request.Zipcode,
                            IsActive=1
                        };


                        _context.Organization.Add(organization);
                        await _context.SaveChangesAsync();
                        KeyMaster key = new KeyMaster()
                        {
                            KeyId = 1,
                            OrgId = organization.Org_ID,
                            Value = 0,
                            Description = "Order No",
                        };
                        _tcontext.keymaster.Add(key);
                        await _context.SaveChangesAsync();
                        return new Response<int>(organization.Org_ID, "Success", true);
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
