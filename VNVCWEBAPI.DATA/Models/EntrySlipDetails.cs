using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class EntrySlipDetails : BaseEntity
    {
        public int EntrySlipId { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public int? VaccineId { get; set; }
        public int? ShipmentID { get; set; }

        [ForeignKey("ShipmentID")]
        public Shipment? Shipment { get; set; }
        [ForeignKey("EntrySlipId")]
        public EntrySlip? EntrySlip { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }

        [NotMapped]
        public double Total => this.Number * this.Price;
    }
}
