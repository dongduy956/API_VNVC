using Microsoft.EntityFrameworkCore;
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
    public class CustomerRankDetailsServices : ICustomerRankDetailsServices
    {
        private readonly IRepository<CustomerRankDetails> repository;
        public CustomerRankDetailsServices(IRepository<CustomerRankDetails> repository)
        {
            this.repository = repository;
        }

        public CustomerRankDetailsModel? GetCustomerRankDetailByPayId(int payId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CustomerRankDetailsModel> GetCustomerRankDetails()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(ct => ct.Customer)
                .Select(x => new CustomerRankDetailsModel
                {
                    CustomerId = x.CustomerId,
                    PayId = x.PayId,
                    Point = x.Point,
                    Id = x.Id,
                    CustomerName = x.Customer.FirstName + ' ' + x.Customer.LastName,
                    Created = x.Created,
                });
        }

        public async Task<CustomerRankDetailsModel> GetCustomerRankDetails(int id)
        {
            var customerRankDetails = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(ct => ct.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            var model = new CustomerRankDetailsModel
            {
                CustomerId = customerRankDetails.CustomerId,
                PayId = customerRankDetails.PayId,
                Point = customerRankDetails.Point,
                Id = customerRankDetails.Id,
                CustomerName = customerRankDetails.Customer.FirstName + ' ' + customerRankDetails.Customer.LastName,
                Created = customerRankDetails.Created
            };
            return model;
        }

        public async Task<bool> InsertCustomerRankDetailRangesAsync(IEnumerable<CustomerRankDetailsModel> models)
        {
            var ListCustomerRankDetails = new List<CustomerRankDetails>();
            foreach (var model in models)
            {
                var customerRankDetail = new CustomerRankDetails
                {
                    CustomerId = model.CustomerId,
                    PayId = model.PayId,
                    Point = model.Point
                };
                ListCustomerRankDetails.Add(customerRankDetail);
            }
            return await repository.InsertRangeAsync(ListCustomerRankDetails);
        }

        public async Task<bool> InsertCustomerRankDetailsAsync(CustomerRankDetailsModel model)
        {
            var customerRankDetail = new CustomerRankDetails
            {
                CustomerId = model.CustomerId,
                PayId = model.PayId,
                Point = model.Point
            };
            var result = await repository.InsertAsync(customerRankDetail);
            model.Id = customerRankDetail.Id;
            model.Created = customerRankDetail.Created;
            return result;
        }

        public IQueryable<CustomerRankDetailsModel> SearchCustomerRankDetails(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(ct => ct.Customer)
                .Where(x => x.Point.ToString().Equals(q) ||
                x.PayId.ToString().Equals(q) ||
                (x.Customer.FirstName + ' ' + x.Customer.LastName).ToLower().Contains(q))
                .Select(customerRankDetails => new CustomerRankDetailsModel
                {
                    CustomerId = customerRankDetails.CustomerId,
                    PayId = customerRankDetails.PayId,
                    Point = customerRankDetails.Point,
                    Id = customerRankDetails.Id,
                    CustomerName = customerRankDetails.Customer.FirstName + ' ' + customerRankDetails.Customer.LastName,
                    Created = customerRankDetails.Created
                });
            return results;
        }

        public async Task<bool> UpdateCustomerRankDetail(int id, int point)
        {
            var customerRankDetail = await repository.GetAsync(id);
            if (customerRankDetail == null)
                return false;
            customerRankDetail.Point = point;
            return await repository.UpdateAsync(customerRankDetail);
        }
    }
}
