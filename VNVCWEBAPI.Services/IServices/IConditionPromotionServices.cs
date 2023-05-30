using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IConditionPromotionServices
    {
        IQueryable<ConditionPromotionModel> GetConditionPromotions();
        ConditionPromotionModel? GetConditionPromotionByVaccineId(int vaccineId);
        ConditionPromotionModel? GetConditionPromotionByCustomerRankId(int customerRankId);
        ConditionPromotionModel? GetConditionPromotionByPaymentMethodId(int paymentMethodId);
        ConditionPromotionModel? GetConditionPromotionByVaccinePackageId(int vaccinePackageId);
        IQueryable<ConditionPromotionModel> SearchConditionPromotions(string q="");
        Task<ConditionPromotionModel> GetConditionPromotionAsync(int id);
        Task<bool> InsertConditionPromotionAsync(ConditionPromotionModel model);
        Task<bool> UpdateConditionPromotionAsync(int id,ConditionPromotionModel model);
        Task<bool> DeleteConditionPromotionAsync(int id);
    }
}
