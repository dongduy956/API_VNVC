using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class InjectionScheduleModel : BaseModel
    {
        public int? StaffId { get; set; }
        public string? StaffName { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int? NominatorId { get; set; }
        public string? NominatorName { get; set; }
        public string? Note { get; set; }
        public int Priorities { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdaterId { get; set; }
        public string? UpdaterName { get; set; }
        public bool? CheckPay { get; set; }

    }
}
