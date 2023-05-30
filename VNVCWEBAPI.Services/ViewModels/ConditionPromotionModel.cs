using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class ConditionPromotionModel : BaseModel
    {
        public int? PackageVaccineId { get; set; }
        public string? PackageVaccineName { get; set; }
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public int? CustomerRankId { get; set; }
        public string? CustomerRankName { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }
        public int? PromotionId { get; set; }
        public string? PromotionCode { get; set; }
        public bool? IsHide { get; set; }
    }
}
