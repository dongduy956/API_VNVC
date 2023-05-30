using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class OrderEntrySlipQuarterOfYear : OrderEntrySlip
    {
        public int Quarter { get; set; }
    }
}
