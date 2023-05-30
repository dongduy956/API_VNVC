using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class PaymentMethod : BaseEntity
    {
        public PaymentMethod()
        {
            Pays = new HashSet<Pay>();
        }
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<Pay> Pays { get; set; }
    }
}
