using CCMERP.Domain.Common;
using CCMERP.Domain.General.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Service.Contract
{
    public interface IGeneralService
    {
        Task<Response<GetCurrencyResponse>> GetCurrency();
        Task<Response<GetCountryResponse>> GetCountry();
    }
}
