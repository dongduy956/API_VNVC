using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Cart : BaseEntity
    {
        public int? VaccineId { get; set; }
        public int? PackageId { get; set; }
        public int? LoginId { get; set; }
        [ForeignKey("LoginId")]
        public Login? Login { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("PackageId")]
        public VaccinePackage? VaccinePackage { get; set; }
    }
}
