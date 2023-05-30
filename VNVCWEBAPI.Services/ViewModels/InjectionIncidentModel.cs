using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class InjectionIncidentModel : BaseModel
    {
        public string Content { get; set; }
        public DateTime InjectionTime { get; set; }
        public int InjectionScheduleId { get; set; }
        public int? VaccineId { get; set; }
        public int ShipmentId { get; set; }
        public string? VaccineName { get; set; }
        public string? ShipmentCode { get; set; }
        

        
    }
}
