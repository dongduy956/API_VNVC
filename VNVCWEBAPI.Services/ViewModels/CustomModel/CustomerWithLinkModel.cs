using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class CustomerWithLinkModel : CustomerModel
    {
        public CustomerWithLinkModel()
        {
            CustomerModels = new List<CustomerModel>();
        }
        public List<CustomerModel> CustomerModels { get; set; }
    }
}
