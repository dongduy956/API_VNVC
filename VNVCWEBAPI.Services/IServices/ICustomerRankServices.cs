using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ICustomerRankServices
    {
        IQueryable<CustomerRankModel> GetCustomerRanks();
        CustomerRankModel? GetCustomerRankByCustomerId(int customerId);
        IQueryable<CustomerRankModel> SearchCustomerRanks(string q="");
        Task<CustomerRankModel> GetCustomerRank(int id);
        Task<bool> InsertCustomerRankAsync(CustomerRankModel model);
        Task<bool> InsertCustomerRanksRange(IList<CustomerRankModel> customerRankModels);

        Task<bool> UpdateCustomerRankAsync(int id,CustomerRankModel model);
        Task<bool> DeleteCustomerRankAsync(int id);
    }
}
