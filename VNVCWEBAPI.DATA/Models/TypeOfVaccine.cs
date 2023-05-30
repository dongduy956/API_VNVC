using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class TypeOfVaccine : BaseEntity
    {
        public TypeOfVaccine()
        {
            Vaccines = new HashSet<Vaccine>();
        }
        public string Name { get; set; }
        public ICollection<Vaccine> Vaccines { get; set; }
    }
}
