using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IPayServices
    {
        IQueryable<PayModel> GetPays();
        PayModel? GetPayByInjectionScheduleId(int injectionScheduleId);
        IQueryable<PayModel> SearchPays(string q="");
        Task<PayModel> GetPay(int id);
        Task<bool> InsertPay(PayModel payModel);
    }
}
