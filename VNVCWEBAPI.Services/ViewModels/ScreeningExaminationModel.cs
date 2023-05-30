using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class ScreeningExaminationModel : BaseModel
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int StaffId { get; set; }
        public string? StaffName { get; set; }
        public string? Diagnostic { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double Heartbeat { get; set; }
        public bool isEligible { get; set; }
        public double BloodPressure { get; set; }
        public string? Note { get; set; }
        public bool? IsHide { get; set; }
    }
}
