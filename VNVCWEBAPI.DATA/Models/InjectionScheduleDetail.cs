using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class InjectionScheduleDetail : BaseEntity
    {
        public int Injections { get; set; }
        public DateTime? InjectionTime { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public bool Injection { get; set; }
        public bool Pay { get; set; }
        public int? InjectionStaffId { get; set; }
        public int ShipmentId { get; set; }
        public int? VaccineId { get; set; }
        public int InjectionScheduleId { get; set; }
        public int? VaccinePackageid { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }
        [ForeignKey("InjectionStaffId")]
        public Staff? InjectionStaff { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("InjectionScheduleId")]
        public InjectionSchedule? InjectionSchedule { get; set; }
        [ForeignKey("VaccinePackageid")]
        public VaccinePackage? VaccinePackage { get; set; }

    }
}
