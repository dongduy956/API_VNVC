using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class ConditionPromotionServices : IConditionPromotionServices
    {
        private readonly IRepository<ConditionPromotion> repository;
        public ConditionPromotionServices(IRepository<ConditionPromotion> repository)
        {
            this.repository = repository;
        }

        public async Task<ConditionPromotionModel> GetConditionPromotionAsync(int id)
        {
            var conditionPromotion = await repository.Where(x => x.Id == id)
                .Include(cr => cr.CustomerRank)
                .Include(vp => vp.VaccinePackage)
                .Include(pm => pm.PaymentMethod)
                .Include(pmc => pmc.Promotion)
                .Include(v => v.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Where(x => x.Promotion.isTrash == false)
                .FirstOrDefaultAsync();

            var model = new ConditionPromotionModel
            {
                Id = conditionPromotion.Id,
                CustomerRankId = conditionPromotion.CustomerRankId,
                CustomerRankName = conditionPromotion.CustomerRank.Name,
                PackageVaccineId = conditionPromotion.PackageVaccineId,
                PackageVaccineName = conditionPromotion.VaccinePackage.Name,
                PaymentMethodId = conditionPromotion.PaymentMethodId,
                PaymentMethodName = conditionPromotion.PaymentMethod.Name,
                PromotionId = conditionPromotion.PromotionId,
                PromotionCode = conditionPromotion.Promotion.PromotionCode,
                VaccineId = conditionPromotion.VaccineId,
                VaccineName = conditionPromotion.Vaccine.Name,
                Created = conditionPromotion.Created,
                IsHide = (conditionPromotion.CustomerRank != null && conditionPromotion.CustomerRank.isTrash == true) ||
                        (conditionPromotion.Vaccine != null && conditionPromotion.Vaccine.isTrash == true) ||
                        (conditionPromotion.Vaccine.TypeOfVaccine != null && conditionPromotion.Vaccine.TypeOfVaccine.isTrash == true) ||
                        (conditionPromotion.PaymentMethod != null && conditionPromotion.PaymentMethod.isTrash == true) ||
                        (conditionPromotion.VaccinePackage != null && conditionPromotion.VaccinePackage.isTrash == true)
            };
            return model;
        }

        public IQueryable<ConditionPromotionModel> GetConditionPromotions()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(cr => cr.CustomerRank)
                .Include(vp => vp.VaccinePackage)
                .Include(pm => pm.PaymentMethod)
                .Include(pmc => pmc.Promotion)
                .Include(v => v.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Where(x => x.Promotion.isTrash == false)
                .Select(conditionPromotion => new ConditionPromotionModel
                {
                    Id = conditionPromotion.Id,
                    CustomerRankId = conditionPromotion.CustomerRankId,
                    CustomerRankName = conditionPromotion.CustomerRank.Name,
                    PackageVaccineId = conditionPromotion.PackageVaccineId,
                    PackageVaccineName = conditionPromotion.VaccinePackage.Name,
                    PaymentMethodId = conditionPromotion.PaymentMethodId,
                    PaymentMethodName = conditionPromotion.PaymentMethod.Name,
                    PromotionId = conditionPromotion.PromotionId,
                    PromotionCode = conditionPromotion.Promotion.PromotionCode,
                    VaccineId = conditionPromotion.VaccineId,
                    VaccineName = conditionPromotion.Vaccine.Name,
                    Created = conditionPromotion.Created,
                    IsHide = (conditionPromotion.CustomerRank != null && conditionPromotion.CustomerRank.isTrash == true) ||
                        (conditionPromotion.Vaccine != null && conditionPromotion.Vaccine.isTrash == true) ||
                        (conditionPromotion.Vaccine.TypeOfVaccine != null && conditionPromotion.Vaccine.TypeOfVaccine.isTrash == true) ||
                        (conditionPromotion.PaymentMethod != null && conditionPromotion.PaymentMethod.isTrash == true) ||
                        (conditionPromotion.VaccinePackage != null && conditionPromotion.VaccinePackage.isTrash == true)
                });
        }

        public async Task<bool> InsertConditionPromotionAsync(ConditionPromotionModel model)
        {
            var promontion = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .OrderByDescending(x => x.Created)
                .FirstOrDefault(x => x.Promotion.EndDate.CompareTo(DateTime.Now) >= 0 &&
                                   ((model.VaccineId != null && x.VaccineId == model.VaccineId) ||
                                    (model.PackageVaccineId != null && x.PackageVaccineId == model.PackageVaccineId) ||
                                    (model.PaymentMethodId != null && x.PaymentMethodId == model.PaymentMethodId) ||
                                    (model.CustomerRankId != null && x.CustomerRankId == model.CustomerRankId)));
            if (promontion != null)
                return false;
            var conditionPromition = new ConditionPromotion
            {
                VaccineId = model.VaccineId,
                PromotionId = model.PromotionId,
                PaymentMethodId = model.PaymentMethodId,
                PackageVaccineId = model.PackageVaccineId,
                CustomerRankId = model.CustomerRankId,
            };
            var result = await repository.InsertAsync(conditionPromition);
            model.Id = conditionPromition.Id;
            model.Created = conditionPromition.Created;
            return result;
        }

        public async Task<bool> UpdateConditionPromotionAsync(int id, ConditionPromotionModel model)
        {
            var promontion = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .OrderByDescending(x => x.Created)
                .FirstOrDefault(x => x.Promotion.EndDate.CompareTo(DateTime.Now) >= 0 && x.Id != id &&
                                   ((model.VaccineId != null && x.VaccineId == model.VaccineId) ||
                                    (model.PackageVaccineId != null && x.PackageVaccineId == model.PackageVaccineId) ||
                                    (model.PaymentMethodId != null && x.PaymentMethodId == model.PaymentMethodId) ||
                                    (model.CustomerRankId != null && x.CustomerRankId == model.CustomerRankId)));
            if (promontion != null)
                return false;
            var conditionPromotion = await repository.GetAsync(id);
            if (model.VaccineId != null)
                conditionPromotion.VaccineId = model.VaccineId;
            conditionPromotion.PromotionId = model.PromotionId;
            if (model.PaymentMethodId != null)
                conditionPromotion.PaymentMethodId = model.PaymentMethodId;
            if (model.PackageVaccineId != null)
                conditionPromotion.PackageVaccineId = model.PackageVaccineId;
            if (model.CustomerRankId != null)
                conditionPromotion.CustomerRankId = model.CustomerRankId;
            return await repository.UpdateAsync(conditionPromotion);
        }

        public async Task<bool> DeleteConditionPromotionAsync(int id)
        {
            var conditionPromotion = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(conditionPromotion);
        }

        public IQueryable<ConditionPromotionModel> SearchConditionPromotions(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(cr => cr.CustomerRank)
                .Include(vp => vp.VaccinePackage)
                .Include(pm => pm.PaymentMethod)
                .Include(pmc => pmc.Promotion)
                .Include(v => v.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Where(x => x.Promotion.isTrash == false && (x.CustomerRank.Name.ToLower().Contains(q) ||
                            x.VaccinePackage.Name.ToLower().Contains(q) ||
                            x.PaymentMethod.Name.ToLower().Contains(q) ||
                            x.Promotion.PromotionCode.ToLower().Equals(q) ||
                            x.Vaccine.Name.ToLower().Contains(q)))
                .Select(conditionPromotion => new ConditionPromotionModel
                {
                    Id = conditionPromotion.Id,
                    CustomerRankId = conditionPromotion.CustomerRankId,
                    CustomerRankName = conditionPromotion.CustomerRank.Name,
                    PackageVaccineId = conditionPromotion.PackageVaccineId,
                    PackageVaccineName = conditionPromotion.VaccinePackage.Name,
                    PaymentMethodId = conditionPromotion.PaymentMethodId,
                    PaymentMethodName = conditionPromotion.PaymentMethod.Name,
                    PromotionId = conditionPromotion.PromotionId,
                    PromotionCode = conditionPromotion.Promotion.PromotionCode,
                    VaccineId = conditionPromotion.VaccineId,
                    VaccineName = conditionPromotion.Vaccine.Name,
                    Created = conditionPromotion.Created,
                    IsHide = (conditionPromotion.CustomerRank != null && conditionPromotion.CustomerRank.isTrash == true) ||
                        (conditionPromotion.Vaccine != null && conditionPromotion.Vaccine.isTrash == true) ||
                        (conditionPromotion.Vaccine.TypeOfVaccine != null && conditionPromotion.Vaccine.TypeOfVaccine.isTrash == true) ||
                        (conditionPromotion.PaymentMethod != null && conditionPromotion.PaymentMethod.isTrash == true) ||
                        (conditionPromotion.VaccinePackage != null && conditionPromotion.VaccinePackage.isTrash == true)
                });
            return results;
        }

        public ConditionPromotionModel? GetConditionPromotionByVaccineId(int vaccineId)
        {
            return GetConditionPromotions()
                .Where(x => x.VaccineId == vaccineId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
        }

        public ConditionPromotionModel? GetConditionPromotionByCustomerRankId(int customerRankId)
        {
            return GetConditionPromotions()
             .Where(x => x.CustomerRankId == customerRankId)
             .OrderByDescending(x => x.Id)
             .FirstOrDefault();
        }

        public ConditionPromotionModel? GetConditionPromotionByPaymentMethodId(int paymentMethodId)
        {
            return GetConditionPromotions()
              .Where(x => x.PaymentMethodId == paymentMethodId)
              .OrderByDescending(x => x.Id)
              .FirstOrDefault();
        }

        public ConditionPromotionModel? GetConditionPromotionByVaccinePackageId(int vaccinePackageId)
        {
            return GetConditionPromotions()
              .Where(x => x.PackageVaccineId == vaccinePackageId)
              .OrderByDescending(x => x.Id)
              .FirstOrDefault();
        }
    }
}
