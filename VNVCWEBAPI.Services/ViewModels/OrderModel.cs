using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class OrderModel : BaseModel
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public int Status { get; set; }
        public int StaffId { get; set; }
        public double? Total { get; set; }
        public string? StaffName { get; set; }
    }
}
