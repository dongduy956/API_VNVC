using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Staff : BaseEntity
    {
        public Staff()
        {
            LoginSessions = new HashSet<LoginSession>();
            ScreeningExaminations = new HashSet<ScreeningExamination>();
            Orders = new HashSet<Order>();
            EntrySlips = new HashSet<EntrySlip>();
            Pays = new HashSet<Pay>();
            InjectionSchedules = new HashSet<InjectionSchedule>();
        }
        public string StaffName { get; set; }
        [Column(TypeName = ("varchar(100)"))]
        public string? Email { get; set; }
        public bool Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = ("varchar(25)"))]
        public string IdentityCard { get; set; }
        [Column(TypeName = ("varchar(200)"))]
        public string Avatar { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Province { get; set; }
        [StringLength(50)]
        public string District { get; set; }
        [StringLength(50)]
        public string Village { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [Column(TypeName = "varchar(11)")]
        public string? PhoneNumber { get; set; }
        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public Permission? Permission { get; set; }
        public ICollection<LoginSession> LoginSessions { get; set; }
        public ICollection<ScreeningExamination> ScreeningExaminations { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<EntrySlip> EntrySlips { get; set; }
        public ICollection<Pay> Pays { get; set; }
        public ICollection<InjectionSchedule> InjectionSchedules { get; set; }
        public ICollection<InjectionSchedule> Nominators { get; set; }
        public ICollection<InjectionSchedule> Updaters { get; set; }
        public Login Login { get; set; }
    }
}
