using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IRegulationCustomerServices
    {
        IQueryable<RegulationCustomerModel> GetRegulationCustomers();
        IQueryable<RegulationCustomerModel> SearchRegulationCustomers(string q="");
        Task<RegulationCustomerModel> GetRegulationCustomer(int id);
        Task<RegulationCustomerModel?> GetRegulationCustomerByCustomerTypeIdAndVaccineId(int customerTypeId,int vaccineId);
        Task<bool> InsertRegulationCustomer(RegulationCustomerModel regulationCustomerModel);
        Task<bool> InsertRegulationCustomersRange(IList<RegulationCustomerModel> regulationCustomerModels);

        Task<bool> UpdateRegulationCustomer(int id, RegulationCustomerModel regulationCustomerModel);
        Task<bool> DeleteRegulationCustomer(int id);
        Task<bool> DeleteRegulationCustomersRange(int[] ids);
    }
}
