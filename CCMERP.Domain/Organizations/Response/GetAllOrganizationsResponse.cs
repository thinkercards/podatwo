using CCMERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Organizations.Response
{
    public class GetAllOrganizationsResponse
    {
        public List<Organization> organizations { get; set; }
        public int totalNoRecords { get; set; }
    }
}
