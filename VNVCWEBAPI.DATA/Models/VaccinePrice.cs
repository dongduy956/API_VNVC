using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class VaccinePrice : BaseEntity
    {
        public int? VaccineId { get; set; }
        public int ShipmentId { get; set; }
        public double EntryPrice { get; set; }
        public double RetailPrice { get; set; }
        public double PreOderPrice { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }
    }
}
