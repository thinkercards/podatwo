using CCMERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Customers.Response
{
    public class GetAllCustomersResponse
    {
        public List<Customer> customers { get; set; }
        public int totalNoRecords { get; set; }
    }
}
