using CCMERP.Domain.AddItem.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Domain.AddItem.Response
{
    public class GetallItemByOrgIdResponse
    {
        public List<AddItemsRequest> items { get; set; }
    }

    public class GetallItemByIdResponse
    {
        public AddItemsRequest item { get; set; }
    }
}
