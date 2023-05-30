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
    public class EntrySlipDetailsServices : IEntrySlipDetailsServices
    {
        private readonly IRepository<EntrySlipDetails> repository;
        private readonly IVaccineServices vaccineServices;

        public EntrySlipDetailsServices(IRepository<EntrySlipDetails> repository, IVaccineServices vaccineServices)
        {
            this.repository = repository;
            this.vaccineServices = vaccineServices;
        }

        public async Task<bool> DeleteEntrySlipDetailsAsync(int id)
        {
            var entrySlip = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(entrySlip);
        }

        public async Task<bool> DeleteEntrySlipDetailsByEntrySlipId(int entrySlipId)
        {
            var entrySlipDetails = repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                                       .Where(x => x.EntrySlipId == entrySlipId);
            return await repository.DeleteFromTrashRange(entrySlipDetails);
        }

        public async Task<bool> DeleteEntrySlipDetailsRangeAsync(int[] ids)
        {
            var lstEntrySlipDetail = new List<EntrySlipDetails>();
            foreach (var id in ids)
            {
                var entrySlip = await repository.GetAsync(id);
                if (entrySlip != null)
                    lstEntrySlipDetail.Add(entrySlip);
            }
            return await repository.DeleteFromTrashRange(lstEntrySlipDetail);
        }

        public EntrySlipDetailsModel? GetEntrySlipDetailByShipmentId(int shipmentId)
        {
            return GetEntrySlipDetails().SingleOrDefault(x => x.ShipmentId == shipmentId);
        }

        public IQueryable<EntrySlipDetailsModel> GetEntrySlipDetails()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(sm => sm.Shipment)
                .Include(v => v.Vaccine)
                .Select(entrySlipDetail => new EntrySlipDetailsModel
                {
                    Id = entrySlipDetail.Id,
                    Number = entrySlipDetail.Number,
                    EntrySlipId = entrySlipDetail.EntrySlipId,
                    Price = entrySlipDetail.Price,
                    ShipmentId = entrySlipDetail.ShipmentID,
                    VaccineId = entrySlipDetail.VaccineId,
                    ShipmentCode = entrySlipDetail.Shipment.ShipmentCode,
                    VaccineName = entrySlipDetail.Vaccine.Name,
                    Created = entrySlipDetail.Created
                });
        }

        public async Task<EntrySlipDetailsModel> GetEntrySlipDetailsAsync(int id)
        {
            var entrySlip = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(sm => sm.Shipment)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(x => x.Id == id);
            var model = new EntrySlipDetailsModel
            {
                Id = entrySlip.Id,
                EntrySlipId = entrySlip.EntrySlipId,
                Number = entrySlip.Number,
                Price = entrySlip.Price,
                VaccineId = entrySlip.VaccineId,
                ShipmentId = entrySlip.ShipmentID,
                ShipmentCode = entrySlip.Shipment.ShipmentCode,
                VaccineName = entrySlip.Vaccine.Name,
                Created = entrySlip.Created,
               
               
            };
            return model;
        }

        public IQueryable<EntrySlipDetailsModel> GetEntrySlipDetailsByEntrySlipId(int entrySlipId)
        {
            return GetEntrySlipDetails().Where(x => x.EntrySlipId == entrySlipId);
        }

        public IList<EntrySlipDetailsModel> GetEntrySlipDetailsByEntrySlipIds(int[] entrySlipIds)
        {
            var listEntrySlipDetails = new List<EntrySlipDetailsModel>();
            foreach (var entrySlipId in entrySlipIds)
            {
                var entrySlipDetails = GetEntrySlipDetailsByEntrySlipId(entrySlipId);
                foreach (var entrySlipDetail in entrySlipDetails)
                {
                    var itemEntrySlipDetail = listEntrySlipDetails.FirstOrDefault(x => x.Id == entrySlipDetail.Id);
                    if (itemEntrySlipDetail != null)
                        itemEntrySlipDetail.Number += entrySlipDetail.Number;
                    else
                        listEntrySlipDetails.Add(entrySlipDetail);
                }
            }
            return listEntrySlipDetails;
        }

        public async Task<bool> InsertEntrySlipDetailsAsync(EntrySlipDetailsModel model)
        {
            var entrySlipDetails = new EntrySlipDetails
            {
                Number = model.Number,
                EntrySlipId = model.EntrySlipId,
                Price = model.Price,
                ShipmentID = model.ShipmentId,
                VaccineId = model.VaccineId,
            };
            var result = await repository.InsertAsync(entrySlipDetails);
            model.Id = entrySlipDetails.Id;
            model.Created = entrySlipDetails.Created;
            return result;
        }

        public async Task<bool> InsertEntrySlipDetailsRangesAsync(IList<EntrySlipDetailsModel> models)
        {
            var lstEntrySlipDetail = new List<EntrySlipDetails>();

            foreach (var model in models)
            {
                var entrySlipDetails = new EntrySlipDetails
                {
                    EntrySlipId = model.EntrySlipId,
                    Number = model.Number,
                    ShipmentID = model.ShipmentId,
                    VaccineId = model.VaccineId,
                    Price = model.Price,
                };
                lstEntrySlipDetail.Add(entrySlipDetails);
            }
            var result = await repository.InsertRangeAsync(lstEntrySlipDetail);
            for (int i = 0; i < lstEntrySlipDetail.Count; i++)
            {
                models[i].Id = lstEntrySlipDetail[i].Id;
                models[i].Created = lstEntrySlipDetail[i].Created;
            }
            return result;
        }

        public async Task<bool> UpdateEntrySlipDetailsAsync(int id, EntrySlipDetailsModel model)
        {
            var entrySlipDetail = await repository.GetAsync(id);
            entrySlipDetail.Number = model.Number;
            return await repository.UpdateAsync(entrySlipDetail);
        }
    }
}
