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
    public class CustomerRankServices : ICustomerRankServices
    {
        private readonly IRepository<CustomerRank> repository;
        private readonly ICustomerRankDetailsServices customerRankDetailsServices;

        public CustomerRankServices(IRepository<CustomerRank> repository, ICustomerRankDetailsServices customerRankDetailsServices)
        {
            this.repository = repository;
            this.customerRankDetailsServices = customerRankDetailsServices;
        }

        public async Task<bool> DeleteCustomerRankAsync(int id)
        {
            var customerRank = await repository.GetAsync(id);
            return await repository.Delete(customerRank);
        }

        public async Task<CustomerRankModel> GetCustomerRank(int id)
        {
            var customerRank = await repository.GetAsync(id);
            var model = new CustomerRankModel
            {
                Id = customerRank.Id,
                Name = customerRank.Name,
                Point = customerRank.Point
            };
            return model;

        }

        public CustomerRankModel? GetCustomerRankByCustomerId(int customerId)
        {
            var points = customerRankDetailsServices
                .GetCustomerRankDetails()
                .Where(x => x.CustomerId == customerId)
                .Sum(x => x.Point);
            return GetCustomerRanks()
                    .OrderByDescending(x => x.Point)
                    .FirstOrDefault(x=>x.Point<=points);            
        }

        public IQueryable<CustomerRankModel> GetCustomerRanks()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Select(x => new CustomerRankModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Point = x.Point,
                    Created=x.Created
                });
        }

        public async Task<bool> InsertCustomerRankAsync(CustomerRankModel model)
        {
            var customerRank = new CustomerRank
            {
                Point = model.Point,
                Name = model.Name
            };

            var result = await repository.InsertAsync(customerRank);
            model.Id = customerRank.Id;
            model.Created = customerRank.Created;
            return result;
        }

        public async Task<bool> InsertCustomerRanksRange(IList<CustomerRankModel> customerRankModels)
        {
            var customerRanks = new List<CustomerRank>();
            foreach (var customerRankModel in customerRankModels)
            {
                customerRanks.Add(new CustomerRank
                {
                    Name = customerRankModel.Name,
                    Point= customerRankModel.Point,
                });
            }
            var result = await repository.InsertRangeAsync(customerRanks);
            for (int i = 0; i < customerRanks.Count; i++)
            {
                customerRankModels[i].Id = customerRanks[i].Id;
                customerRankModels[i].Created = customerRanks[i].Created;
            }
            return result;
        }

        public IQueryable<CustomerRankModel> SearchCustomerRanks(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.Name.ToLower().Contains(q) ||
                            x.Point.ToString().Equals(q))
                .Select(model => new CustomerRankModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Point = model.Point,
                    Created=model.Created
                });
            return results;
        }

        public async Task<bool> UpdateCustomerRankAsync(int id, CustomerRankModel model)
        {
            var customerRank = await repository.GetAsync(id);
            customerRank.Point = model.Point;
            return await repository.UpdateAsync(customerRank);
        }
    }
}
