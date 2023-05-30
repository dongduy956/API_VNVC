using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    public class ShipmentServices : IShipmentServices
    {
        private readonly IRepository<Shipment> repository;
        public ShipmentServices(IRepository<Shipment> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteShipment(int id)
        {
            var shipment = await repository.GetAsync(id);
            return await repository.Delete(shipment);
        }

        public async Task<ShipmentModel?> GetShipment(int id)
        {
            var shipment = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(ss => ss.Supplier)
                .Include(et => et.EntrySlipDetails)
                .Include(p => p.PayDetails)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Include(v => v.InjectionScheduleDetails)
                    .ThenInclude(x => x.InjectionSchedule)
                .Where(x => x.Vaccine.isTrash == false && x.Supplier.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false)
                .FirstOrDefaultAsync(x => x.Id == id && x.ExpirationDate.CompareTo(DateTime.Now) >= 0);
            if (shipment == null)
                return null;
            return new ShipmentModel
            {
                Id = shipment.Id,
                SupplierName = shipment.Supplier.Name,
                Country = shipment.Country,
                ExpirationDate = shipment.ExpirationDate,
                ManufactureDate = shipment.ManufactureDate,
                ShipmentCode = shipment.ShipmentCode,
                SupplierId = shipment.SupplierId,
                Created = shipment.Created,
                VaccineName = shipment.Vaccine.Name,
                VaccineId = shipment.VaccineId,
                QuantityRemain = shipment.EntrySlipDetails.Sum(x => x.Number) -
                                                      shipment.PayDetails
                                                      .Sum(x => x.Number)
                                                    - shipment.InjectionScheduleDetails
                                                      .Where(x => x.InjectionSchedule.Priorities == 0 && x.Pay == false)
                                                      .Count()
            };
        }

        public IQueryable<ShipmentModel> GetShipments()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(ss => ss.Supplier)
                 .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                 .Include(et => et.EntrySlipDetails)
                 .Include(pd => pd.PayDetails)
                 .Include(v => v.InjectionScheduleDetails)
                 .ThenInclude(x => x.InjectionSchedule)
                 .Where(x => x.Vaccine.isTrash == false && x.Supplier.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false)
                    .Select(shipment => new ShipmentModel
                    {
                        Id = shipment.Id,
                        SupplierName = shipment.Supplier.Name,
                        Country = shipment.Country,
                        ExpirationDate = shipment.ExpirationDate,
                        ManufactureDate = shipment.ManufactureDate,
                        ShipmentCode = shipment.ShipmentCode,
                        SupplierId = shipment.SupplierId,
                        Created = shipment.Created,
                        VaccineName = shipment.Vaccine.Name,
                        VaccineId = shipment.VaccineId,
                        QuantityRemain = shipment.EntrySlipDetails.Sum(x => x.Number) -
                                                      shipment.PayDetails
                                                      .Sum(x => x.Number)
                                                    - shipment.InjectionScheduleDetails
                                                      .Where(x => x.InjectionSchedule.Priorities == 0 && x.Pay == false)
                                                      .Count()
                    });
        }

        public IQueryable<ShipmentModel> GetShipmentsBySupplierId(int supplierID)
        {
            return GetShipments().Where(x => x.SupplierId == supplierID && x.ExpirationDate.CompareTo(DateTime.Now) >= 0);
        }

        public async Task<IList<ShipmentModel>> GetShipmentsByIds(int[] ids)
        {
            var shipments = new List<ShipmentModel>();
            foreach (var id in ids)
            {
                var shipment = await GetShipment(id);
                if (shipment != null)
                {
                    shipments.Add(shipment);
                }
            }
            return shipments;
        }

        public async Task<bool> InsertShipment(ShipmentModel shipmentModel)
        {
            var shipment = new Shipment
            {
                Country = shipmentModel.Country,
                ManufactureDate = shipmentModel.ManufactureDate,
                ExpirationDate = shipmentModel.ExpirationDate,
                SupplierId = shipmentModel.SupplierId,
                ShipmentCode = shipmentModel.ShipmentCode,
                VaccineId = shipmentModel.VaccineId
            };
            var result = await repository.InsertAsync(shipment);
            shipmentModel.Id = shipment.Id;
            shipmentModel.Created = shipment.Created;
            return result;
        }

        public IQueryable<ShipmentModel> SearchShipments(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(ss => ss.Supplier)
                 .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                 .Include(et => et.EntrySlipDetails)
                 .Include(pd => pd.PayDetails)
                 .Include(v => v.InjectionScheduleDetails)
                    .ThenInclude(x => x.InjectionSchedule)
                .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false && x.Supplier.isTrash == false &&
                            (x.Country.Trim().ToLower().Contains(q) ||
                            x.Supplier.Name.ToLower().Contains(q) ||
                            x.ExpirationDate.ToString().Contains(q) ||
                            x.ManufactureDate.ToString().Contains(q) ||
                            x.ShipmentCode.ToLower().Equals(q) ||
                            x.Vaccine.Name.ToLower().Equals(q)))

                .Select(shipment => new ShipmentModel
                {
                    Id = shipment.Id,
                    SupplierName = shipment.Supplier.Name,
                    Country = shipment.Country,
                    ExpirationDate = shipment.ExpirationDate,
                    ManufactureDate = shipment.ManufactureDate,
                    ShipmentCode = shipment.ShipmentCode,
                    SupplierId = shipment.SupplierId,
                    Created = shipment.Created,
                    VaccineName = shipment.Vaccine.Name,
                    VaccineId = shipment.VaccineId,
                    QuantityRemain = shipment.EntrySlipDetails.Sum(x => x.Number) -
                                                      shipment.PayDetails
                                                      .Sum(x => x.Number)
                                                    - shipment.InjectionScheduleDetails
                                                      .Where(x => x.InjectionSchedule.Priorities == 0 && x.Pay == false)
                                                      .Count()
                });
            return results;
        }

        public async Task<bool> UpdateShipment(int id, ShipmentModel shipmentModel)
        {
            var shipment = await repository.GetAsync(id);
            shipment.ManufactureDate = shipmentModel.ManufactureDate;
            shipment.ExpirationDate = shipmentModel.ExpirationDate;
            return await repository.UpdateAsync(shipment);
        }

        public IQueryable<ShipmentModel> GetShipmentsByVaccineId(int vaccineId)
        {
            return GetShipments()
                   .Where(x => x.VaccineId == vaccineId && x.ExpirationDate.CompareTo(DateTime.Now) >= 0);

        }

        public async Task<bool> InsertShipmentsRange(IList<ShipmentModel> shipmentModels)
        {
            var shipments = new List<Shipment>();
            foreach (var shipmentModel in shipmentModels)
            {
                shipments.Add(new Shipment
                {
                    Country = shipmentModel.Country,
                    ManufactureDate = shipmentModel.ManufactureDate,
                    ExpirationDate = shipmentModel.ExpirationDate,
                    SupplierId = shipmentModel.SupplierId,
                    ShipmentCode = shipmentModel.ShipmentCode,
                    VaccineId = shipmentModel.VaccineId
                });
            }
            var result = await repository.InsertRangeAsync(shipments);
            for (int i = 0; i < shipments.Count; i++)
            {
                shipmentModels[i].Id = shipments[i].Id;
                shipmentModels[i].Created = shipments[i].Created;
            }
            return result;
        }
    }
}
