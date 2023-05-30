using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class PayModel : BaseModel
    {
        public int? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int InjectionScheduleId { get; set; }
        public string Payer { get; set; }
        public int PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }
        public double GuestMoney { get; set; }
        public double ExcessMoney { get; set; }
        public double Discount { get; set; }
        public double Total { get; set;}
        public string? CustomerName { get; set; }
    }
}
