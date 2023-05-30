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
    public interface ICustomerServices
    {
        IQueryable<CustomerModel> GetCustomers();
        IQueryable<CustomerModel> GetCustomersEligible();
        Task<IList<CustomerModel>> GetCustomerByIds(int[] ids);
        IQueryable<CustomerModel> SearchCustomers(string q = "");
        Task<bool> DeleteCustomerAsync(int id);
        Task<CustomerModel> GetCustomer(int id);
        Task<CustomerModel> GetCustomerByLoginId(int id);
        Task<bool> InsertCustomerAsync(CustomerModel model);
        Task<bool> InsertCustomersRange(IList<CustomerModel> customerModels);
        Task<bool> UpdateCustomerAsync(int id, CustomerModel model);
    }
}
