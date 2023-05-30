using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class EntrySlipModel : BaseModel
    {
        public int? OrderId { get; set; }
        public int? SupplierId { get; set; }
        public int? StaffId { get; set; }
        public string? SupplierName { get; set; }
        public string? StaffName { get; set; }
        public double Total { get; set; }
    }
}
