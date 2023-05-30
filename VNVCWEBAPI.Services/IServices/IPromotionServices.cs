using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IPromotionServices
    {
        IQueryable<PromotionModel> GetPromotions();
        IQueryable<PromotionModel> SearchPromotions(string q="");
        Task<PromotionModel?> GetPromotion(int id);
        Task<bool> InsertPromotion(PromotionModel promotionModel);
        Task<bool> UpdatePromotion(int id, PromotionModel promotionModel);
        Task<bool> UpdateCountPromotionsRange(int[] ids);
        Task<bool> DeletePromotion(int id);
    }
}
