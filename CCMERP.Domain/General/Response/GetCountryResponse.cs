using CCMERP.Domain.Entities;
using CCMERP.Domain.General.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.General.Response
{
    public class GetCountryResponse
    {
        public List<CountryMaster> getCountry { get; set; }
    }
}
