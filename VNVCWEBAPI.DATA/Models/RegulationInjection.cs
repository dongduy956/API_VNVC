using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class RegulationInjection : BaseEntity
    {
        public RegulationInjection() { }
        public int Distance { get; set; }
        public int Order { get; set; }
        public int RepeatInjection { get; set; }
        public int VaccineId { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }

    }
}
