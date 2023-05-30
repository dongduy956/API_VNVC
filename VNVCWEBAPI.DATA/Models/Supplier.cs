using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Supplier : BaseEntity
    {
        public Supplier()
        {
            Orders = new HashSet<Order>();
        }
        public string Name { get; set; }
        public string Address { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string TaxCode { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
