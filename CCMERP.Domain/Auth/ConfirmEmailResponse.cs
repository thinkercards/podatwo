using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.Auth
{
    public class ConfirmEmailResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
