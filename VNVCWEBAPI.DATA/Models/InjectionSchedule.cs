using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class InjectionSchedule : BaseEntity
    {
        public InjectionSchedule()
        {
            Pays = new HashSet<Pay>();
            InjectionScheduleDetails = new HashSet<InjectionScheduleDetail>();
        }
        public int? StaffId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int? NominatorId { get; set; }
        public string? Note { get; set; }
        public int Priorities { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdaterId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        [ForeignKey("NominatorId")]
        public Staff? Nominator { get; set; }
        [ForeignKey("UpdaterId")]
        public Staff? Updater { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public ICollection<Pay> Pays { get; set; }
        public ICollection<InjectionScheduleDetail> InjectionScheduleDetails { get; set; }

    }
}
