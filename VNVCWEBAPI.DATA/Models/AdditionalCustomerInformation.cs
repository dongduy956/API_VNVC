using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class AdditionalCustomerInformation : BaseEntity
    {
        public double WeightAtBirth { get; set; }
        public double HeightAtBirth { get; set; }
        [StringLength(100)]
        public string? FatherFullName { get; set; }
        [StringLength(11)]
        public string? FatherPhoneNumber { get; set; }
        [StringLength(100)]
        public string? MotherFullName { get; set; }
        [StringLength(11)]
        public string? MotherPhoneNumber { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

    }
}
