using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class PayDetailServices : IPayDetailServices
    {
        private readonly IRepository<PayDetail> repository;
        public PayDetailServices(IRepository<PayDetail> repository)
        {
            this.repository = repository;
        }

      
        public async Task<PayDetailModel> GetPayDetail(int id)
        {
            var payDetail = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(vc => vc.Vaccine)
                .Include(s => s.Shipment)
                .Include(s => s.VaccinePackage)
                .FirstOrDefaultAsync(x => x.Id == id);
            return new PayDetailModel
            {
                Id = payDetail.Id,
                Number = payDetail.Number,
                Discount = payDetail.Discount,
                PayId = payDetail.PayId,
                Price = payDetail.Price,
                VaccineId = payDetail.VaccineId,
                VaccineName = payDetail.Vaccine.Name,
                Created = payDetail.Created,
                ShipmentCode = payDetail.Shipment.ShipmentCode,
                ShipmentId = payDetail.ShipmentId,
                VaccinePackageId = payDetail.VaccinePackageId,
                VaccinePackageName = payDetail.VaccinePackage.Name,
                DiscountPackage = payDetail.DiscountPackage
            };
        }

        public IQueryable<PayDetailModel> GetPayDetails(int payId)
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Include(vc => vc.Vaccine)
                    .Include(s => s.Shipment)
                    .Include(vp => vp.VaccinePackage)
                    .Where(x => x.PayId == payId)
                    .Select(payDetail => new PayDetailModel
                    {
                        Id = payDetail.Id,
                        Number = payDetail.Number,
                        Discount = payDetail.Discount,
                        PayId = payDetail.PayId,
                        Price = payDetail.Price,
                        VaccineId = payDetail.VaccineId,
                        VaccineName = payDetail.Vaccine.Name,
                        Created = payDetail.Created,
                        ShipmentCode = payDetail.Shipment.ShipmentCode,
                        ShipmentId = payDetail.ShipmentId,
                        VaccinePackageId = payDetail.VaccinePackageId,
                        VaccinePackageName = payDetail.VaccinePackage.Name,
                        DiscountPackage = payDetail.DiscountPackage

                    });
        }

        public async Task<bool> InsertPayDetail(PayDetailModel payDetailModel)
        {
            var payDetail = new PayDetail
            {
                Number = payDetailModel.Number,
                Discount = payDetailModel.Discount,
                PayId = payDetailModel.PayId,
                Price = payDetailModel.Price,
                VaccineId = payDetailModel.VaccineId,
                ShipmentId = payDetailModel.ShipmentId,
                VaccinePackageId = payDetailModel.VaccinePackageId,
                DiscountPackage = payDetailModel.DiscountPackage
            };
            var result = await repository.InsertAsync(payDetail);
            payDetailModel.Id = payDetail.Id;
            payDetailModel.Created = payDetail.Created;
            return result;
        }

        public async Task<bool> InsertPayDetailsRange(IList<PayDetailModel> payDetailModels)
        {
            var payDetails = new List<PayDetail>();
            foreach (var payDetailModel in payDetailModels)
            {
                payDetails.Add(new PayDetail
                {
                    Number = payDetailModel.Number,
                    Discount = payDetailModel.Discount,
                    PayId = payDetailModel.PayId,
                    Price = payDetailModel.Price,
                    VaccineId = payDetailModel.VaccineId,
                    ShipmentId = payDetailModel.ShipmentId,
                    VaccinePackageId = payDetailModel.VaccinePackageId,
                    DiscountPackage = payDetailModel.DiscountPackage
                });
            }
            var result = await repository.InsertRangeAsync(payDetails);
            for (int i = 0; i < payDetails.Count; i++)
            {
                payDetailModels[i].Id = payDetails[i].Id;
                payDetailModels[i].Created = payDetails[i].Created;
            }
            return result;
        }
    }
}
