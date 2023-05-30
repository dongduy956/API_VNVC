using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class ConditionPromotion : BaseEntity
    {
        public int? PackageVaccineId { get; set; }
        public int? VaccineId { get; set; }
        public int? CustomerRankId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? PromotionId { get; set; }

        [ForeignKey("PromotionId")]
        public Promotion? Promotion { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod? PaymentMethod { get; set; }
        [ForeignKey("CustomerRankId")]
        public CustomerRank? CustomerRank { get; set; }
        [ForeignKey("VaccineId")]
        public Vaccine? Vaccine { get; set; }
        [ForeignKey("PackageVaccineId")]
        public VaccinePackage? VaccinePackage { get; set; }

    }
}
