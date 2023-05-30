using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class PromotionServices : IPromotionServices
    {
        private readonly IRepository<Promotion> repository;
        public PromotionServices(IRepository<Promotion> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeletePromotion(int id)
        {
            var promotion = await repository.GetAsync(id);
            return await repository.Delete(promotion);
        }

        public async Task<PromotionModel?> GetPromotion(int id)
        {
            var model = await repository.GetAsync(id);
            if (model.StartDate.CompareTo(DateTime.Now) <= 0 && model.EndDate.CompareTo(DateTime.Now) >= 0 && model.Count!=0)
                return new PromotionModel
                {
                    Id = model.Id,
                    PromotionCode = model.PromotionCode,
                    Count = model.Count,
                    Discount = model.Discount,
                    EndDate = model.EndDate,
                    StartDate = model.StartDate,
                    Created = model.Created
                };
            return null;
        }

        public IQueryable<PromotionModel> GetPromotions()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Select(model => new PromotionModel
                    {
                        Id = model.Id,
                        PromotionCode = model.PromotionCode,
                        Discount = model.Discount,
                        EndDate = model.EndDate,
                        StartDate = model.StartDate,
                        Created = model.Created,
                        Count = model.Count,
                    });
        }

        public async Task<bool> InsertPromotion(PromotionModel promotionModel)
        {
            var promotion = new Promotion
            {
                Discount = promotionModel.Discount,
                EndDate = promotionModel.EndDate,
                PromotionCode = promotionModel.PromotionCode,
                StartDate = promotionModel.StartDate,
                Count = promotionModel.Count,
            };
            var result = await repository.InsertAsync(promotion);
            promotionModel.Id = promotion.Id;
            promotionModel.Created = promotion.Created;
            return result;
        }

        public IQueryable<PromotionModel> SearchPromotions(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.EndDate.ToString().Contains(q) ||
                            x.StartDate.ToString().Contains(q) ||
                            x.Discount.ToString().Equals(q) ||
                            x.PromotionCode.ToLower().Equals(q))
                .Select(model => new PromotionModel
                {
                    Id = model.Id,
                    PromotionCode = model.PromotionCode,
                    Discount = model.Discount,
                    EndDate = model.EndDate,
                    StartDate = model.StartDate,
                    Created = model.Created,
                    Count = model.Count,
                });
            return results;
        }

        public async Task<bool> UpdatePromotion(int id, PromotionModel promotionModel)
        {
            var promotion = await repository.GetAsync(id);

            promotion.Discount = promotionModel.Discount;
            promotion.EndDate = promotionModel.EndDate;
            promotion.StartDate = promotionModel.StartDate;
            promotion.Count = promotionModel.Count;
            return await repository.UpdateAsync(promotion);
        }

        public async Task<bool> UpdateCountPromotionsRange(int[] ids)
        {
            var promotions = new List<Promotion>();
            foreach (var id in ids)
            {
                var promotion = await repository.GetAsync(id);
                if (promotion.Count > 0)
                {
                    promotion.Count -= 1;
                    promotions.Add(promotion);
                }
            }
            return await repository.UpdateRangeAsync(promotions);
        }
    }
}
