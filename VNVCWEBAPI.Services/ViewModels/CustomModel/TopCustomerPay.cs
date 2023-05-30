using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class TopCustomerPay
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double Total { get; set; }
    }
}
