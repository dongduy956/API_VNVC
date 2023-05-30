using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class VaccinePackage : BaseEntity
    {
        public VaccinePackage()
        {
            VaccinePackageDetails = new HashSet<VaccinePackageDetails>();
            PayDetails = new HashSet<PayDetail>();
            Carts = new HashSet<Cart>();
        }
        public string Name { get; set; }
        public string ObjectInjection { get; set; }
        public ICollection<VaccinePackageDetails> VaccinePackageDetails { get; set; }
        public ICollection<PayDetail> PayDetails { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
