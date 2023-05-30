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
using static VNVCWEBAPI.Common.Constraint.Permissions;

namespace VNVCWEBAPI.Services.Services
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> repository;
        public CartServices(IRepository<Cart> repository)
        {
            this.repository = repository;
        }
        public async Task<bool> DeleteCartAsync(int id, int userId)
        {
            var cart = await repository.Where(x => x.Id == id && x.LoginId == userId).FirstOrDefaultAsync();
            if (cart == null)
                return false;
            return await repository.DeleteFromTrash(cart);
        }

        public async Task<bool> DeleteCartRangeAsync(int[] ids, int userId)
        {
            var items = new List<Cart>();
            foreach (var id in ids)
            {
                var cart = await repository.Where(x => x.Id == id && x.LoginId == userId).FirstOrDefaultAsync();
                items.Add(cart);
            }
            return await repository.DeleteFromTrashRange(items);
        }

        public CartModel? GetCart(int id)
        {
            var cart = GetCarts().SingleOrDefault(x => x.Id == id);
            if (cart == null)
                return null;
            return new CartModel
            {
                Id = cart.Id,
                Created = cart.Created,
                PackageId = cart.PackageId,
                VaccineId = cart.VaccineId,

            };
        }

        public IQueryable<CartModel> GetCarts()
        {
            return repository.GetAll(Common.Enum.SelectEnum.Select.ALL)
                         .Include(x => x.Vaccine)
                             .ThenInclude(r => r.EntrySlipDetails)
                         .Include(x => x.Vaccine)
                             .ThenInclude(x => x.PayDetails)
                         .Include(x => x.Vaccine)
                             .ThenInclude(tvc => tvc.TypeOfVaccine)
                         .Include(x => x.Vaccine)
                             .ThenInclude(x => x.InjectionScheduleDetails)
                                 .ThenInclude(x => x.InjectionSchedule)
                         .Include(x => x.Vaccine)
                              .ThenInclude(x => x.VaccinePrices)
                         .Include(x => x.VaccinePackage)
                              .Select(cart => new CartModel
                              {
                                  Id = cart.Id,
                                  Created = cart.Created,
                                  PackageId = cart.PackageId,
                                  VaccineId = cart.VaccineId,
                                  LoginId = cart.LoginId,
                                  Vaccine = cart.Vaccine == null ? null : new VaccineModel
                                  {
                                      Id = cart.Vaccine.Id,
                                      Amount = cart.Vaccine.Amount,
                                      DiseasePrevention = cart.Vaccine.DiseasePrevention,
                                      Image = cart.Vaccine.Image,
                                      InjectionSite = cart.Vaccine.InjectionSite,
                                      Name = cart.Vaccine.Name,
                                      SideEffects = cart.Vaccine.SideEffects,
                                      Storage = cart.Vaccine.Storage,
                                      StorageTemperatures = cart.Vaccine.StorageTemperatures,
                                      TypeOfVaccineId = cart.Vaccine.TypeOfVaccineId,
                                      TypeOfVaccineName = cart.Vaccine.TypeOfVaccine.Name,
                                      QuantityRemain = cart.Vaccine.EntrySlipDetails.Sum(x => x.Number)
                                                       - cart.Vaccine.PayDetails.Sum(x => x.Number)
                                                       - cart.Vaccine.InjectionScheduleDetails.Where(x => x.Pay == false && x.InjectionSchedule.Priorities == 0).Count(),
                                      Created = cart.Vaccine.Created,
                                      VaccinePrices = cart.Vaccine.VaccinePrices
                                      .Select(x => new VaccinePriceModel
                                      {
                                          Id = x.Id,
                                          VaccineId = x.VaccineId,
                                          ShipmentId = x.ShipmentId,
                                          EntryPrice = x.EntryPrice,
                                          RetailPrice = x.RetailPrice,
                                          PreOderPrice = x.PreOderPrice
                                      }).ToList()
                                  },
                                  vaccinePackageModel = cart.VaccinePackage == null ? null : new VaccinePackageModel
                                  {
                                      Id = cart.VaccinePackage.Id,
                                      Name = cart.VaccinePackage.Name,
                                      ObjectInjection = cart.VaccinePackage.ObjectInjection,
                                      Created = cart.VaccinePackage.Created
                                  }
                              });
        }

        public async Task<bool> InsertCart(CartModel cartModel)
        {
            var cart = new Cart
            {
                VaccineId = cartModel.VaccineId,
                PackageId = cartModel.PackageId,
                LoginId = cartModel.LoginId
            };
            return await repository.InsertAsync(cart);
        }
    }
}
