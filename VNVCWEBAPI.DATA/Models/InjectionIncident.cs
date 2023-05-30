using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class InjectionIncident : BaseEntity
    {
        public string Content { get; set; }
        public DateTime InjectionTime { get; set; }
        public int InjectionScheduleId { get; set; }
        public int? VaccineId { get; set; }
        public int ShipmentId { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }
        [ForeignKey("InjectionScheduleId")]
        public InjectionSchedule? InjectionSchedule { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }

    }
}
