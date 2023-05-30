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
    public interface IVaccinePriceServices
    {
        VaccinePriceModel? GetVaccinePriceLastByVaccineIdAndShipmentId(int vaccineId,int shipmentId);
        IQueryable<VaccinePriceModel> GetVaccinePrices();
        IQueryable<VaccinePriceModel> SearchVaccinePrices(string q="");
        Task<VaccinePriceModel> GetVaccinePrice(int id);
        Task<bool> InsertVaccinePrice(VaccinePriceModel vaccinePriceModel);
        Task<bool> InsertVaccinePricesRange(IList<VaccinePriceModel> vaccinePriceModels);
        Task<bool> UpdateVaccinePrice(VaccinePriceModel vaccinePriceModel);
        Task<bool> DeleteVaccinePrice(int id);
    }
}
