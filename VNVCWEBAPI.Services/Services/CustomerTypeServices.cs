using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class CustomerTypeServices : ICustomerTypeServices
    {
        private readonly IRepository<CustomerType> repository;
        public CustomerTypeServices(IRepository<CustomerType> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteCustomerTypeAsync(int id)
        {
            var customerType = await repository.GetAsync(id);
            return await repository.Delete(customerType);
        }

        public async Task<CustomerTypeModel> GetCustomerType(int id)
        {
            var customerType = await repository.GetAsync(id);
            var model = new CustomerTypeModel
            {
                Id = customerType.Id,
                Age = customerType.Age,
                Name = customerType.Name,
                 Created=customerType.Created,                 
            };
            return model;
        }

        public IQueryable<CustomerTypeModel> GetCustomerTypes()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Select(x => new CustomerTypeModel
                {
                    Id = x.Id,
                    Age = x.Age,
                    Name = x.Name,
                    Created=x.Created
                });
        }

        public async Task<bool> InsertCustomerTypeAsync(CustomerTypeModel model)
        {

            var customerType = new CustomerType
            {
                Age = model.Age,
                Name = model.Name,

            };
            var result = await repository.InsertAsync(customerType);
            model.Id = customerType.Id;
            model.Created = customerType.Created;
            return result;

        }

        public async Task<bool> InsertCustomerTypesRange(IList<CustomerTypeModel> customerTypeModels)
        {
            var customerTypes = new List<CustomerType>();
            foreach (var customerTypeModel in customerTypeModels)
            {
                customerTypes.Add(new CustomerType
                {
                    Name=customerTypeModel.Name,
                    Age= customerTypeModel.Age,
                });
            }
            var result = await repository.InsertRangeAsync(customerTypes);
            for (int i = 0; i < customerTypes.Count; i++)
            {
                customerTypeModels[i].Id = customerTypes[i].Id;
                customerTypeModels[i].Created = customerTypes[i].Created;
            }
            return result;

        }

        public IQueryable<CustomerTypeModel> SearchCustomerTypes(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x =>
                        x.Age.ToString().Equals(q) ||
                        x.Name.Trim().ToLower().Contains(q))
                .Select(model => new CustomerTypeModel
                {
                    Id = model.Id,
                    Age = model.Age,
                    Name = model.Name,
                    Created=model.Created
                });
            return results;
        }

        public async Task<bool> UpdateCustomerTypeAsync(int id, CustomerTypeModel model)
        {
            var customerType = await repository.GetAsync(id);
            customerType.Name = model.Name;
            customerType.Age = model.Age;
            return await repository.UpdateAsync(customerType);
        }
    }
}
