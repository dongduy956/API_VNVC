using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class CustomerType : BaseEntity
    {
        public CustomerType()
        {
            Customers = new HashSet<Customer>();
            RegulationCustomers = new HashSet<RegulationCustomer>();
        }
        [StringLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<RegulationCustomer> RegulationCustomers { get; set; }
    }
}
