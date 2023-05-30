using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class VaccinePackageDetails : BaseEntity
    {
        public int OrderInjection { get; set; }
        public int NumberOfInjections { get; set; }
        public int? VaccineId { get; set; }
        public int ShipmentId { get; set; }
        public int VaccinePackageId { get; set; }
        public bool isGeneral { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("VaccinePackageId")]
        public VaccinePackage? VaccinePackage { get; set; }

    }
}
