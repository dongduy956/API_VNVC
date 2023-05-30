using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class ShipmentModel : BaseModel
    {
        public string? ShipmentCode { get; set; }
        public string? SupplierName { get; set; }
        public int SupplierId { get; set; }
        public int VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Country { get; set; }
        public int? QuantityRemain { get; set; }
    }
}
