using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
            EntrySlips = new HashSet<EntrySlip>();
        }
        public int SupplierId { get; set; }
        public int Status { get; set; }
        public int StaffId { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        public ICollection<EntrySlip> EntrySlips { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        [NotMapped]
        public double Total => this.OrderDetails.Sum(x => x.Total);
    }
}
