using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Pay : BaseEntity
    {
        public Pay()
        {
            CustomerRankDetails = new HashSet<CustomerRankDetails>();
            PayDetails = new HashSet<PayDetail>();
        }
        public int? StaffId { get; set; }
        public int InjectionScheduleId { get; set; }
        public string Payer { get; set; }
        public int PaymentMethodId { get; set; }
        public double GuestMoney { get; set; }
        public double ExcessMoney { get; set; }
        public double Discount { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod? PaymentMethod { get; set; }
        [ForeignKey("InjectionScheduleId")]
        public InjectionSchedule? InjectionSchedule { get; set; }
        public ICollection<PayDetail> PayDetails { get; set; }
        public ICollection<CustomerRankDetails> CustomerRankDetails { get; set; }
        [NotMapped]
        public double Total
        {
            get
            {
                var listPackageIds = new List<int>();
                foreach (var item in PayDetails)
                {
                    if (item.VaccinePackageId != null && listPackageIds.FindIndex(x => x == item.VaccinePackageId) == -1)
                        listPackageIds.Add(item.VaccinePackageId.Value);
                }
                double totalDiscountPackage = 0;
                foreach (var id in listPackageIds)
                {
                    var discountPackage = this.PayDetails.FirstOrDefault(x => x.VaccinePackageId != null && x.VaccinePackageId == id)?.DiscountPackage.Value;
                    var resultPayDetails = this.PayDetails.Where(x => x.VaccinePackageId != null && x.VaccinePackageId == id);
                    var sumTotal = resultPayDetails.Sum(x => x.Total);
                    totalDiscountPackage += sumTotal * (1 - resultPayDetails.Sum(x => x.Number) / 100.0) * (1 - discountPackage.Value);
                }
                return (1 - Discount) * (this.PayDetails.Where(x => x.VaccinePackageId == null).Sum(x => x.Total) + totalDiscountPackage);
            }
        }
    }
}
