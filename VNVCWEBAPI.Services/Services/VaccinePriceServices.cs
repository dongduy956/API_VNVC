using Microsoft.EntityFrameworkCore;
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
    public class VaccinePriceServices : IVaccinePriceServices
    {
        private readonly IRepository<VaccinePrice> repository;
        public VaccinePriceServices(IRepository<VaccinePrice> repository)
        {
            this.repository = repository;
        }
        public async Task<bool> DeleteVaccinePrice(int id)
        {
            var vaccinePrice = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(vaccinePrice);
        }
        public async Task<VaccinePriceModel> GetVaccinePrice(int id)
        {
            var vaccinePrice = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(sm => sm.Shipment)
                    .ThenInclude(x => x.Supplier)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false &&
                          x.Shipment.isTrash == false && x.Shipment.Supplier.isTrash == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            return new VaccinePriceModel
            {
                Id = vaccinePrice.Id,
                Created = vaccinePrice.Created,
                ShipmentCode = vaccinePrice.Shipment.ShipmentCode,
                VaccineName = vaccinePrice.Vaccine.Name,
                EntryPrice = vaccinePrice.EntryPrice,
                PreOderPrice = vaccinePrice.PreOderPrice,
                RetailPrice = vaccinePrice.RetailPrice,
                ShipmentId = vaccinePrice.ShipmentId,
                VaccineId = vaccinePrice.VaccineId,
            };
        }

        public VaccinePriceModel? GetVaccinePriceLastByVaccineIdAndShipmentId(int vaccineId, int shipmentId)
        {
            return GetVaccinePrices()
            .Where(x => x.VaccineId == vaccineId && x.ShipmentId == shipmentId)
            .OrderByDescending(x => x.Created)
            .FirstOrDefault();
        }

        public IQueryable<VaccinePriceModel> GetVaccinePrices()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(sm => sm.Shipment)
                    .ThenInclude(x => x.Supplier)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Select(vaccinePrice => new VaccinePriceModel
                {
                    Id = vaccinePrice.Id,
                    Created = vaccinePrice.Created,
                    ShipmentCode = vaccinePrice.Shipment.ShipmentCode,
                    VaccineName = vaccinePrice.Vaccine.Name,
                    EntryPrice = vaccinePrice.EntryPrice,
                    PreOderPrice = vaccinePrice.PreOderPrice,
                    RetailPrice = vaccinePrice.RetailPrice,
                    ShipmentId = vaccinePrice.ShipmentId,
                    VaccineId = vaccinePrice.VaccineId,
                    IsHide = vaccinePrice.Vaccine.isTrash == true || vaccinePrice.Vaccine.TypeOfVaccine.isTrash == true ||
                            vaccinePrice.Shipment.isTrash == true || vaccinePrice.Shipment.Supplier.isTrash == true
                });
        }

        public async Task<bool> InsertVaccinePrice(VaccinePriceModel vaccinePriceModel)
        {
            var vaccinePrice = new VaccinePrice
            {
                VaccineId = vaccinePriceModel.VaccineId,
                ShipmentId = vaccinePriceModel.ShipmentId,
                PreOderPrice = vaccinePriceModel.PreOderPrice,
                EntryPrice = vaccinePriceModel.EntryPrice,
                RetailPrice = vaccinePriceModel.RetailPrice,
            };
            var result = await repository.InsertAsync(vaccinePrice);
            vaccinePriceModel.Id = vaccinePrice.Id;
            vaccinePriceModel.Created = vaccinePrice.Created;
            return result;
        }

        public async Task<bool> InsertVaccinePricesRange(IList<VaccinePriceModel> vaccinePriceModels)
        {
            var vaccinePrices = new List<VaccinePrice>();
            foreach (var vaccinePriceModel in vaccinePriceModels)
            {
                vaccinePrices.Add(new VaccinePrice
                {
                    VaccineId = vaccinePriceModel.VaccineId,
                    ShipmentId = vaccinePriceModel.ShipmentId,
                    PreOderPrice = vaccinePriceModel.PreOderPrice,
                    EntryPrice = vaccinePriceModel.EntryPrice,
                    RetailPrice = vaccinePriceModel.RetailPrice,
                });
            }
            var result = await repository.InsertRangeAsync(vaccinePrices);
            for (int i = 0; i < vaccinePrices.Count; i++)
            {
                vaccinePriceModels[i].Id = vaccinePrices[i].Id;
                vaccinePriceModels[i].Created = vaccinePrices[i].Created;
            }
            return result;
        }

        public IQueryable<VaccinePriceModel> SearchVaccinePrices(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(sm => sm.Shipment)
                    .ThenInclude(x => x.Supplier)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Where(x => x.Created.ToString().Contains(q) ||
                            x.EntryPrice.ToString().Equals(q) ||
                            x.RetailPrice.ToString().Equals(q) ||
                            x.PreOderPrice.ToString().Equals(q) ||
                            x.Vaccine.Name.ToLower().Contains(q) ||
                            x.Shipment.ShipmentCode.Equals(q))
                .Select(vaccinePrice => new VaccinePriceModel
                {
                    Id = vaccinePrice.Id,
                    Created = vaccinePrice.Created,
                    ShipmentCode = vaccinePrice.Shipment.ShipmentCode,
                    VaccineName = vaccinePrice.Vaccine.Name,
                    EntryPrice = vaccinePrice.EntryPrice,
                    PreOderPrice = vaccinePrice.PreOderPrice,
                    RetailPrice = vaccinePrice.RetailPrice,
                    ShipmentId = vaccinePrice.ShipmentId,
                    VaccineId = vaccinePrice.VaccineId,
                    IsHide = vaccinePrice.Vaccine.isTrash == true || vaccinePrice.Vaccine.TypeOfVaccine.isTrash == true ||
                            vaccinePrice.Shipment.isTrash == true || vaccinePrice.Shipment.Supplier.isTrash == true
                });
            return results;
        }

        public async Task<bool> UpdateVaccinePrice(VaccinePriceModel vaccinePriceModel)
        {
            return await InsertVaccinePrice(vaccinePriceModel);
        }
    }
}
