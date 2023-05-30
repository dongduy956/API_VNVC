using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class PayDetail : BaseEntity
    {
        public double Price { get; set; }
        public int Number { get; set; }
        public double Discount { get; set; }
        public double? DiscountPackage { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }
        public int ShipmentId { get; set; }
        public int PayId { get; set; }
        [ForeignKey("VaccinePackageId")]
        public VaccinePackage? VaccinePackage { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("PayId")]
        public Pay? Pay { get; set; }
        [NotMapped]
        public double Total => (1 - (this.Discount )) * this.Price * this.Number;
    }
}
