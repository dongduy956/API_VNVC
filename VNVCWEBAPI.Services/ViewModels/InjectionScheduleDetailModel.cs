using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class InjectionScheduleDetailModel : BaseModel
    {
        public int Injections { get; set; }
        public DateTime? InjectionTime { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public bool Injection { get; set; }
        public bool Pay { get; set; }
        public int? InjectionStaffId { get; set; }
        public string? InjectionStaffName { get; set; }
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public int InjectionScheduleId { get; set; }
        public int? VaccinePackageId { get; set; }
        public string? VaccinePackageName { get; set; }
        public int ShipmentId { get; set; }
        public string? ShipmentCode { get; set; }
        public bool? CheckReport { get; set; }
        public DateTime? ScheduleTime { get; set; }
    }
}
