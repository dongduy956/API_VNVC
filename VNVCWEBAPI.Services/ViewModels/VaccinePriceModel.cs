using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class VaccinePriceModel : BaseModel
    {
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public int ShipmentId { get; set; }
        public string? ShipmentCode { get; set; }
        public double EntryPrice { get; set; }
        public double RetailPrice { get; set; }
        public double PreOderPrice { get; set; }
        public bool? IsHide { get; set; }
    }
}
