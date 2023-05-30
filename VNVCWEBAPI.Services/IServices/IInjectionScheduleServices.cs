using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IInjectionScheduleServices
    {
        IQueryable<InjectionScheduleModel> GetInjectionSchedules();
        IQueryable<InjectionScheduleModel> SearchInjectionSchedules(string q = "");
        InjectionScheduleModel? GetInjectionScheduleAsync(int id);
        Task<bool> InsertInjectionScheduleAsync(InjectionScheduleModel model);
        Task<bool> UpdateInjectionScheduleAsync(int id, InjectionScheduleModel model);
        Task<bool> DeleteInjectionScheduleAsync(int id);
    }
}
