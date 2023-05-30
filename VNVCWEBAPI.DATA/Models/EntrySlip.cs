using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class EntrySlip : BaseEntity
    {
        public EntrySlip()
        {
            EntrySlipDetails = new HashSet<EntrySlipDetails>();
        }
        public int? OrderId { get; set; }
        public int? SupplierId { get; set; }
        public int? StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        public ICollection<EntrySlipDetails> EntrySlipDetails { get; set; }
        [NotMapped]
        public double Total => this.EntrySlipDetails.Sum(x=>x.Total);
    }
}
