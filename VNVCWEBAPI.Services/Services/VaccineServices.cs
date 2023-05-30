using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class VaccineServices : IVaccineServices
    {
        private readonly IRepository<Vaccine> repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string myHostUrl = "";
        public VaccineServices(IRepository<Vaccine> repository, IHttpContextAccessor _httpContextAccessor)
        {
            this.repository = repository;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public async Task<bool> DeleteVaccine(int id)
        {
            var vaccine = await repository.GetAsync(id);
            return await repository.Delete(vaccine);
        }

        public async Task<VaccineModel> GetVaccine(int id)
        {
            //Vaccine -> Vaccine | select * from dbo.vaccine, dbo.paydetails
            //-Khoá ngoại: Paydetails = null
            //-> Include Paydetails
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var vaccine = await repository.Where(x => x.Id == id)
                .Include(r => r.EntrySlipDetails)
                .Include(pd => pd.PayDetails)
                .Include(tvc => tvc.TypeOfVaccine)
                .Include(x => x.InjectionScheduleDetails)
                .ThenInclude(x => x.InjectionSchedule)
                .Include(x => x.VaccinePrices)
                .Where(x => x.TypeOfVaccine.isTrash == false)
                .FirstOrDefaultAsync();
            return new VaccineModel
            {
                Id = vaccine.Id,
                Amount = vaccine.Amount,
                DiseasePrevention = vaccine.DiseasePrevention,
                Image = string.IsNullOrEmpty(vaccine.Image) ? "" : $"{myHostUrl}{vaccine.Image}",
                InjectionSite = vaccine.InjectionSite,
                Name = vaccine.Name,
                SideEffects = vaccine.SideEffects,
                Storage = vaccine.Storage,
                StorageTemperatures = vaccine.StorageTemperatures,
                TypeOfVaccineId = vaccine.TypeOfVaccineId,
                TypeOfVaccineName = vaccine.TypeOfVaccine.Name,
                QuantityRemain = vaccine.EntrySlipDetails.Sum(x => x.Number)
                                - vaccine.PayDetails.Sum(x => x.Number)
                                - vaccine.InjectionScheduleDetails.Where(x => x.Pay == false && x.InjectionSchedule.Priorities == 0).Count(),
                Created = vaccine.Created,
                Content = vaccine.Content,
                VaccinePrices = vaccine.VaccinePrices
                                     .Select(x => new VaccinePriceModel
                                     {
                                         Id = x.Id,
                                         VaccineId = x.VaccineId,
                                         ShipmentId = x.ShipmentId,
                                         EntryPrice = x.EntryPrice,
                                         RetailPrice = x.RetailPrice,
                                         PreOderPrice = x.PreOderPrice,

                                     }).OrderByDescending(x => x.Id).Take(2).ToList()
            };
        }

        public IQueryable<VaccineModel> GetVaccines()
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(r => r.EntrySlipDetails)
                .Include(pd => pd.PayDetails)
                .Include(tvc => tvc.TypeOfVaccine)
                .Include(x => x.InjectionScheduleDetails)
                    .ThenInclude(x => x.InjectionSchedule)
                .Include(x => x.VaccinePrices)
                .Where(x => x.TypeOfVaccine.isTrash == false)
                .Select(x => new VaccineModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Amount = x.Amount,
                    DiseasePrevention = x.DiseasePrevention,
                    Image = string.IsNullOrEmpty(x.Image) ? "" : $"{myHostUrl}{x.Image}",
                    InjectionSite = x.InjectionSite,
                    Storage = x.Storage,
                    StorageTemperatures = x.StorageTemperatures,
                    SideEffects = x.SideEffects,
                    TypeOfVaccineId = x.TypeOfVaccineId,
                    TypeOfVaccineName = x.TypeOfVaccine.Name,
                    QuantityRemain = x.EntrySlipDetails.Sum(x => x.Number) - x.PayDetails.Sum(x => x.Number) - x.InjectionScheduleDetails.Where(y => y.Pay == false && y.InjectionSchedule.Priorities == 0).Count(),
                    Created = x.Created,
                    Content = x.Content,
                    VaccinePrices = x.VaccinePrices
                                     .Select(x => new VaccinePriceModel
                                     {
                                         Id = x.Id,
                                         VaccineId = x.VaccineId,
                                         ShipmentId = x.ShipmentId,
                                         EntryPrice = x.EntryPrice,
                                         RetailPrice = x.RetailPrice,
                                         PreOderPrice = x.PreOderPrice
                                     }).OrderByDescending(x => x.Id).Take(2).ToList()
                });
        }

        public VaccineModel GetVaccineByShipmentId(int shipmentId)
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var x = repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                              .Include(r => r.EntrySlipDetails)
                              .Include(pd => pd.PayDetails)
                              .Include(tvc => tvc.TypeOfVaccine)
                              .Include(sm => sm.Shipments)
                              .Include(x => x.InjectionScheduleDetails)
                              .ThenInclude(x => x.InjectionSchedule)
                              .Include(x => x.VaccinePrices)
                              .Where(x => x.TypeOfVaccine.isTrash == false)
                              .FirstOrDefault(x => x.Shipments.Any(s => s.Id == shipmentId));
            if (x == null)
                return null;
            return new VaccineModel
            {
                Id = x.Id,
                Name = x.Name,
                Amount = x.Amount,
                DiseasePrevention = x.DiseasePrevention,
                Image = string.IsNullOrEmpty(x.Image) ? "" : $"{myHostUrl}{x.Image}",
                InjectionSite = x.InjectionSite,
                Storage = x.Storage,
                StorageTemperatures = x.StorageTemperatures,
                SideEffects = x.SideEffects,
                TypeOfVaccineId = x.TypeOfVaccineId,
                TypeOfVaccineName = x.TypeOfVaccine.Name,
                QuantityRemain = x.EntrySlipDetails.Sum(x => x.Number) - x.PayDetails.Sum(x => x.Number) - x.InjectionScheduleDetails.Where(y => y.Pay == false && y.InjectionSchedule.Priorities == 0).Count(),
                Created = x.Created,
                Content = x.Content,
                VaccinePrices = x.VaccinePrices
                                     .Select(x => new VaccinePriceModel
                                     {
                                         Id = x.Id,
                                         VaccineId = x.VaccineId,
                                         ShipmentId = x.ShipmentId,
                                         EntryPrice = x.EntryPrice,
                                         RetailPrice = x.RetailPrice,
                                         PreOderPrice = x.PreOderPrice
                                     }).OrderByDescending(x => x.Id).Take(2).ToList()
            };
        }

        public async Task<bool> InsertVaccine(VaccineModel vaccineModel)
        {
            var vaccine = new Vaccine
            {
                Name = vaccineModel.Name,
                Amount = vaccineModel.Amount,
                DiseasePrevention = vaccineModel.DiseasePrevention,
                Image = vaccineModel.Image,
                InjectionSite = vaccineModel.InjectionSite,
                Storage = vaccineModel.Storage,
                StorageTemperatures = vaccineModel.StorageTemperatures,
                SideEffects = vaccineModel.SideEffects,
                TypeOfVaccineId = vaccineModel.TypeOfVaccineId,
                Content = vaccineModel.Content
            };
            return await repository.InsertAsync(vaccine);
        }

        public IQueryable<VaccineModel> SearchVaccines(string q = "")
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(r => r.EntrySlipDetails)
                .Include(pd => pd.PayDetails)
                .Include(tvc => tvc.TypeOfVaccine)
                .Include(x => x.InjectionScheduleDetails)
                .ThenInclude(x => x.InjectionSchedule)
                .Where(x => x.TypeOfVaccine.isTrash == false && (x.Name.Trim().ToLower().Contains(q) ||
                            x.SideEffects.ToLower().Contains(q) ||
                            x.Amount.ToString().Equals(q) ||
                            x.DiseasePrevention.ToLower().Contains(q) ||
                            x.InjectionSite.ToLower().Contains(q) ||
                            x.TypeOfVaccine.Name.ToLower().Contains(q) ||
                            x.StorageTemperatures.ToLower().Contains(q)))
                .Select(vaccineModel => new VaccineModel
                {
                    Id = vaccineModel.Id,
                    Name = vaccineModel.Name,
                    Amount = vaccineModel.Amount,
                    DiseasePrevention = vaccineModel.DiseasePrevention,
                    Image = string.IsNullOrEmpty(vaccineModel.Image) ? "" : $"{myHostUrl}{vaccineModel.Image}",
                    InjectionSite = vaccineModel.InjectionSite,
                    Storage = vaccineModel.Storage,
                    StorageTemperatures = vaccineModel.StorageTemperatures,
                    SideEffects = vaccineModel.SideEffects,
                    TypeOfVaccineId = vaccineModel.TypeOfVaccineId,
                    TypeOfVaccineName = vaccineModel.TypeOfVaccine.Name,
                    QuantityRemain = vaccineModel.EntrySlipDetails.Sum(x => x.Number) - vaccineModel.PayDetails.Sum(x => x.Number) - vaccineModel.InjectionScheduleDetails.Where(y => y.Pay == false && y.InjectionSchedule.Priorities == 0).Count(),
                    Created = vaccineModel.Created,
                    Content = vaccineModel.Content,
                    VaccinePrices = vaccineModel.VaccinePrices
                                     .Select(x => new VaccinePriceModel
                                     {
                                         Id = x.Id,
                                         VaccineId = x.VaccineId,
                                         ShipmentId = x.ShipmentId,
                                         EntryPrice = x.EntryPrice,
                                         RetailPrice = x.RetailPrice,
                                         PreOderPrice = x.PreOderPrice
                                     }).OrderByDescending(x => x.Id).Take(2).ToList()
                });
            return results;
        }

        public async Task<bool> UpdateVaccine(int id, VaccineModel vaccineModel)
        {
            var vaccine = await repository.GetAsync(id);
            vaccine.Name = vaccineModel.Name;
            vaccine.Amount = vaccineModel.Amount;
            vaccine.DiseasePrevention = vaccineModel.DiseasePrevention;
            vaccine.Image = vaccineModel.Image;
            vaccine.InjectionSite = vaccineModel.InjectionSite;
            vaccine.Storage = vaccineModel.Storage;
            vaccine.StorageTemperatures = vaccineModel.StorageTemperatures;
            vaccine.SideEffects = vaccineModel.SideEffects;
            vaccine.TypeOfVaccineId = vaccineModel.TypeOfVaccineId;
            vaccine.Content = vaccineModel.Content;
            return await repository.UpdateAsync(vaccine);
        }

        public async Task<bool> InsertVaccinesRange(IList<VaccineModel> vaccineModels)
        {
            var vaccines = new List<Vaccine>();
            foreach (var vaccineModel in vaccineModels)
            {
                vaccines.Add(new Vaccine
                {
                    Name = vaccineModel.Name,
                    Amount = vaccineModel.Amount,
                    DiseasePrevention = vaccineModel.DiseasePrevention,
                    Image = vaccineModel.Image,
                    InjectionSite = vaccineModel.InjectionSite,
                    Storage = vaccineModel.Storage,
                    StorageTemperatures = vaccineModel.StorageTemperatures,
                    SideEffects = vaccineModel.SideEffects,
                    TypeOfVaccineId = vaccineModel.TypeOfVaccineId,
                    Content = vaccineModel.Content
                });
            }
            var result = await repository.InsertRangeAsync(vaccines);
            for (int i = 0; i < vaccines.Count; i++)
            {
                vaccineModels[i].Id = vaccines[i].Id;
                vaccineModels[i].Created = vaccines[i].Created;
            }
            return result;
        }
    }
}
