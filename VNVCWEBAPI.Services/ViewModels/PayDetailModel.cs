using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class PayDetailModel : BaseModel
    {
        public double Price { get; set; }
        public int Number { get; set; }        
        public double Discount { get; set; }
        public double? DiscountPackage { get; set; }
        public int? VaccineId { get; set; }

        public string? VaccineName { get; set; }
        public int ShipmentId { get; set; }
        public string? ShipmentCode { get; set; }
        public int? VaccinePackageId { get; set; }
        public string? VaccinePackageName { get; set; }
        public int PayId { get; set; }
        public double Total => (1 - this.Discount) * this.Price * this.Number;
    }
}
