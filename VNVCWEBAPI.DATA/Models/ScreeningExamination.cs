using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class ScreeningExamination : BaseEntity
    {
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public string? Diagnostic { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Temperature { get; set; }
        public double Heartbeat { get; set; }
        public double BloodPressure { get; set; }
        public bool isEligible { get; set; }
        public string? Note { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}
