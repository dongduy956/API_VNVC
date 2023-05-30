using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IRegulationInjectionServices
    {
        IQueryable<RegulationInjectionModel> GetRegulationInjections();
        RegulationInjectionModel? GetRegulationInjectionByVaccineId(int vaccineId);
        IQueryable<RegulationInjectionModel> SearchRegulationInjections(string q="");
        Task<RegulationInjectionModel> GetRegulationInjection(int id);
        Task<bool> InsertRegulationInjection(RegulationInjectionModel regulationInjectionModel);
        Task<bool> InsertRegulationInjectionsRange(IList<RegulationInjectionModel> regulationInjectionModels);
        Task<bool> UpdateRegulationInjection(int id, RegulationInjectionModel regulationInjectionModel);
        Task<bool> DeleteRegulationInjection(int id);
        Task<bool> DeleteRegulationInjectionsRange(int[] ids);
    }
}
