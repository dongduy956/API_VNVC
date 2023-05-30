using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.DATA.Models
{
    public interface IVaccinePackageServices
    {
        IQueryable<VaccinePackageModel> GetVaccinePackages();
        IQueryable<VaccinePackageModel> SearchVaccinePackages(string q = "");
        Task<VaccinePackageModel> GetVaccinePackage(int id);
        Task<bool> InsertVaccinePackage(VaccinePackageModel vaccinePackageModel);
        Task<bool> InsertVaccinePackagesRange(IList<VaccinePackageModel> vaccinePackageModels);

        Task<bool> UpdateVaccinePackage(int id, VaccinePackageModel vaccinePackageModel);
        Task<bool> DeleteVaccinePackage(int id);
    }
}
