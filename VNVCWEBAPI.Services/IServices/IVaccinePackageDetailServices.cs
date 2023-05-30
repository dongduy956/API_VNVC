using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IVaccinePackageDetailServices
    {
        
        IQueryable<VaccinePackageDetailModel> GetVaccinePackageDetailsByVaccinePackageId(int vaccinePakageId);
        IQueryable<VaccinePackageDetailModel> GetVaccinePackageDetails();
        VaccinePackageDetailModel GetVaccinePackageDetail(int vaccinePackageId,int shipmentId);
        Task<bool> InsertVaccinePackageDetail(VaccinePackageDetailModel vaccinePackageDetailModel);
        Task<bool> UpdateVaccinePackageDetail(int id, VaccinePackageDetailModel vaccinePackageDetailModel);
        Task<bool> InsertVaccinePackageDetailsRange(IList<VaccinePackageDetailModel> vaccinePackageDetailModels);
        Task<bool> UpdateVaccinePackageDetailsRange(IList<VaccinePackageDetailModel> vaccinePackageDetailModels);
        Task<bool> DeleteVaccinePackageDetail(int id);
        Task<bool> DeleteVaccinePackageDetailByVaccinePackageId(int vaccinePackageId);
        
        Task<bool> DeleteVaccinePackageDetailsRange(int[] ids);

    }
}
