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
    public class VaccinePackageDetailServices : IVaccinePackageDetailServices
    {
        private readonly IRepository<VaccinePackageDetails> repository;
        public VaccinePackageDetailServices(IRepository<VaccinePackageDetails> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteVaccinePackageDetail(int id)
        {
            var vaccinePackageDetails = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(vaccinePackageDetails);
        }

        public async Task<bool> DeleteVaccinePackageDetailByVaccinePackageId(int vaccinePackageId)
        {
            var vaccinePackageDetails = repository
                                      .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                                      .Where(x => x.VaccinePackageId == vaccinePackageId);
            return await repository.DeleteFromTrashRange(vaccinePackageDetails);
        }

        public async Task<bool> DeleteVaccinePackageDetailsRange(int[] ids)
        {
            var vaccinePackageDetails = new List<VaccinePackageDetails>();
            foreach (var id in ids)
            {
                var vaccinePackageDetail = await repository.GetAsync(id);
                if (vaccinePackageDetail != null)
                {
                    vaccinePackageDetails.Add(vaccinePackageDetail);
                }
            }
            return await repository.DeleteFromTrashRange(vaccinePackageDetails);
        }


        public VaccinePackageDetailModel GetVaccinePackageDetail(int vaccinePackageId, int shipmentId)
        {
            var vaccinePackageDetail = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(vc => vc.Vaccine)
                .Include(s => s.Shipment)
                .FirstOrDefault(x => x.VaccinePackageId == vaccinePackageId && x.ShipmentId == shipmentId);
            return new VaccinePackageDetailModel
            {
                Id = vaccinePackageDetail.Id,
                NumberOfInjections = vaccinePackageDetail.NumberOfInjections,
                OrderInjection = vaccinePackageDetail.OrderInjection,
                VaccineId = vaccinePackageDetail.VaccineId,
                VaccineName = vaccinePackageDetail.Vaccine.Name,
                VaccinePackageId = vaccinePackageDetail.VaccinePackageId,
                Created = vaccinePackageDetail.Created,
                ShipmentId = vaccinePackageDetail.ShipmentId,
                ShipmentCode = vaccinePackageDetail.Shipment.ShipmentCode,
                isGeneral = vaccinePackageDetail.isGeneral
            };
        }

        public IQueryable<VaccinePackageDetailModel> GetVaccinePackageDetails()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(vc => vc.Vaccine)
                .Include(s => s.Shipment)
                .Select(vaccinePackageDetail => new VaccinePackageDetailModel
                {
                    Id = vaccinePackageDetail.Id,
                    NumberOfInjections = vaccinePackageDetail.NumberOfInjections,
                    OrderInjection = vaccinePackageDetail.OrderInjection,
                    VaccineId = vaccinePackageDetail.VaccineId,
                    VaccineName = vaccinePackageDetail.Vaccine.Name,
                    VaccinePackageId = vaccinePackageDetail.VaccinePackageId,
                    Created = vaccinePackageDetail.Created,
                    ShipmentId = vaccinePackageDetail.ShipmentId,
                    ShipmentCode = vaccinePackageDetail.Shipment.ShipmentCode,
                    isGeneral = vaccinePackageDetail.isGeneral
                });
        }

        public IQueryable<VaccinePackageDetailModel> GetVaccinePackageDetailsByVaccinePackageId(int vaccinePackageId)
        {
            return repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.VaccinePackageId == vaccinePackageId)
                .Include(vc => vc.Vaccine)
                .Include(vp => vp.VaccinePackage)
                .Include(s => s.Shipment)
                .Select(vaccinePackageDetail => new VaccinePackageDetailModel
                {
                    Id = vaccinePackageDetail.Id,
                    NumberOfInjections = vaccinePackageDetail.NumberOfInjections,
                    OrderInjection = vaccinePackageDetail.OrderInjection,
                    VaccineId = vaccinePackageDetail.VaccineId,
                    VaccineName = vaccinePackageDetail.Vaccine.Name,
                    VaccinePackageId = vaccinePackageDetail.VaccinePackageId,
                    Created = vaccinePackageDetail.Created,
                    VaccinePackageName = vaccinePackageDetail.VaccinePackage.Name,
                    ShipmentId = vaccinePackageDetail.ShipmentId,
                    ShipmentCode = vaccinePackageDetail.Shipment.ShipmentCode,
                    isGeneral = vaccinePackageDetail.isGeneral
                });
        }

        public Task<bool> InsertVaccinePackageDetail(VaccinePackageDetailModel vaccinePackageDetailModel)
        {
            var vaccinePackageDetail = new VaccinePackageDetails
            {
                NumberOfInjections = vaccinePackageDetailModel.NumberOfInjections,
                OrderInjection = vaccinePackageDetailModel.OrderInjection,
                VaccineId = vaccinePackageDetailModel.VaccineId,
                VaccinePackageId = vaccinePackageDetailModel.VaccinePackageId,
                ShipmentId = vaccinePackageDetailModel.ShipmentId,
                isGeneral = vaccinePackageDetailModel.isGeneral
            };
            var result = repository.InsertAsync(vaccinePackageDetail);
            vaccinePackageDetailModel.Id = vaccinePackageDetail.Id;
            vaccinePackageDetailModel.Created = vaccinePackageDetail.Created;
            return result;
        }

        public async Task<bool> InsertVaccinePackageDetailsRange(IList<VaccinePackageDetailModel> vaccinePackageDetailModels)
        {
            var vaccinePackageDetails = new List<VaccinePackageDetails>();
            foreach (var vaccinePackageDetailModel in vaccinePackageDetailModels)
            {
                vaccinePackageDetails.Add(new VaccinePackageDetails
                {
                    NumberOfInjections = vaccinePackageDetailModel.NumberOfInjections,
                    OrderInjection = vaccinePackageDetailModel.OrderInjection,
                    VaccineId = vaccinePackageDetailModel.VaccineId,
                    VaccinePackageId = vaccinePackageDetailModel.VaccinePackageId,
                    ShipmentId = vaccinePackageDetailModel.ShipmentId,
                    isGeneral = vaccinePackageDetailModel.isGeneral
                });
            }
            var result = await repository.InsertRangeAsync(vaccinePackageDetails);
            for (int i = 0; i < vaccinePackageDetails.Count; i++)
            {
                vaccinePackageDetailModels[i].Id = vaccinePackageDetails[i].Id;
                vaccinePackageDetailModels[i].Created = vaccinePackageDetails[i].Created;
            }
            return result;
        }

        public async Task<bool> UpdateVaccinePackageDetail(int id, VaccinePackageDetailModel vaccinePackageDetailModel)
        {
            var vaccinePackageDetail = await repository.GetAsync(id);
            vaccinePackageDetail.NumberOfInjections = vaccinePackageDetailModel.NumberOfInjections;
            vaccinePackageDetail.isGeneral = vaccinePackageDetailModel.isGeneral;
            return await repository.UpdateAsync(vaccinePackageDetail);
        }

        public async Task<bool> UpdateVaccinePackageDetailsRange(IList<VaccinePackageDetailModel> vaccinePackageDetailModels)
        {
            var vaccinePackageDetails = new List<VaccinePackageDetails>();
            foreach (var vaccinePackageDetailModel in vaccinePackageDetailModels)
            {
                var vaccinePackageDetail = await repository.GetAsync(vaccinePackageDetailModel.Id);
                vaccinePackageDetail.NumberOfInjections = vaccinePackageDetailModel.NumberOfInjections;
                vaccinePackageDetails.Add(vaccinePackageDetail);
            }
            return await repository.UpdateRangeAsync(vaccinePackageDetails);

        }
    }
}
