using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ICustomerRankDetailsServices
    {
        IQueryable<CustomerRankDetailsModel> GetCustomerRankDetails();
        IQueryable<CustomerRankDetailsModel> SearchCustomerRankDetails(string q="");
        Task<CustomerRankDetailsModel> GetCustomerRankDetails(int id);
        CustomerRankDetailsModel? GetCustomerRankDetailByPayId(int payId);
        Task<bool> InsertCustomerRankDetailsAsync(CustomerRankDetailsModel model);
        Task<bool> InsertCustomerRankDetailRangesAsync(IEnumerable<CustomerRankDetailsModel> models);
        Task<bool> UpdateCustomerRankDetail(int id,int point);


    }
}
