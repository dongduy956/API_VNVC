using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IPayDetailServices
    {
        IQueryable<PayDetailModel> GetPayDetails(int payId);
        Task<PayDetailModel> GetPayDetail(int id);
        Task<bool> InsertPayDetail(PayDetailModel payDetailModel);
        Task<bool> InsertPayDetailsRange(IList<PayDetailModel> payDetailModels);   
    }

}
