using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IVaccineServices
    {
        IQueryable<VaccineModel> GetVaccines();
        VaccineModel GetVaccineByShipmentId(int shipmentId);
        IQueryable<VaccineModel> SearchVaccines(string q="");
        Task<VaccineModel> GetVaccine(int id);
        Task<bool> InsertVaccine(VaccineModel vaccineModel);
        Task<bool> InsertVaccinesRange(IList<VaccineModel> vaccineModels);

        Task<bool> UpdateVaccine(int id, VaccineModel vaccineModel);
        Task<bool> DeleteVaccine(int id);
    }
}
