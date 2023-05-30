using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ICustomerTypeServices
    {
        IQueryable<CustomerTypeModel> GetCustomerTypes();
        IQueryable<CustomerTypeModel> SearchCustomerTypes(string q="");
        Task<CustomerTypeModel> GetCustomerType(int id);
        Task<bool> InsertCustomerTypeAsync(CustomerTypeModel model);
        Task<bool> InsertCustomerTypesRange(IList<CustomerTypeModel> customerTypeModels);

        Task<bool> UpdateCustomerTypeAsync(int id,CustomerTypeModel model);
        Task<bool> DeleteCustomerTypeAsync(int id);
    }
}
