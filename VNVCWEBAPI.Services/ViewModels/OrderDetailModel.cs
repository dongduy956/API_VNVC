using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class OrderDetailModel : BaseModel
    {
        public int OrderId { get; set; }
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public int? ShipmentId { get; set; }
        public string? ShipmentCode { get; set; }
        public double? Total => this.Number * this.Price;
    }
}
