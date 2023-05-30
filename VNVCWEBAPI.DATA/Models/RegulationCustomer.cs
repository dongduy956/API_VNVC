using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class RegulationCustomer : BaseEntity
    {
        public int Amount { get; set; }
        public int VaccineId { get; set; }
        public int CustomerTypeId { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("CustomerTypeId")]
        public CustomerType? CustomerType { get; set; }

    }
}
