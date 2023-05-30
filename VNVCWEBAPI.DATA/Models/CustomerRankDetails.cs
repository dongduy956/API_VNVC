using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class CustomerRankDetails : BaseEntity
    {
        public int Point { get; set; }
        public int CustomerId { get; set; }
        public int PayId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        [ForeignKey("PayId")]
        public Pay? Pay { get; set; }
    }
}
