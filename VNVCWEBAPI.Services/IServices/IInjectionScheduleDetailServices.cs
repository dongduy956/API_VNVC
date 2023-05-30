using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IInjectionScheduleDetailServices
    {
        IQueryable<InjectionScheduleDetailModel> GetInjectionScheduleDetails(int injectionScheduleId);
        IQueryable<InjectionScheduleDetailModel> GetInjectionScheduleDetailsByCustomerId(int customerId);
        Task<InjectionScheduleDetailModel> GetInjectionScheduleDetail(int id);
        Task<bool> InsertInjectionScheduleDetail(InjectionScheduleDetailModel injectionScheduleDetailModel);
        Task<bool> InsertInjectionScheduleDetailsRange(IList<InjectionScheduleDetailModel> injectionScheduleDetailModels);
        Task<bool> UpdateInjectionInjectionScheduleDetail(int id);
        Task<bool> UpdateAddressInjectionStaffInjectionScheduleDetail(int id,string address,int injectionStaffId);       
        Task<bool> UpdatePayInjectionScheduleDetails(int[] ids);
        Task<bool> DeleteInjectionScheduleDetail(int id);
        Task<bool> DeleteInjectionScheduleDetailsRange(int[] ids);
        Task<bool> DeleteInjectionScheduleDetailsByInjectionScheduleId(int injectionScheduleId);
    }
}
