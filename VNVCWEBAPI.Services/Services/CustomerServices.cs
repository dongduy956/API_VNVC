using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IRepository<Customer> repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string myHostUrl = "";
        public CustomerServices(IRepository<Customer> repository, IHttpContextAccessor _httpContextAccessor)
        {
            this.repository = repository;
            this._httpContextAccessor = _httpContextAccessor;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await repository.GetAsync(id);
            return await repository.Delete(customer);
        }

        public async Task<CustomerModel> GetCustomer(int id)
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var x = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(ct => ct.CustomerType)
                .Where(x => x.CustomerType.isTrash == false)
                .FirstOrDefaultAsync(x => x.Id == id);
            var model = new CustomerModel
            {
                Id = x.Id,
                Address = x.Address,
                Avatar = string.IsNullOrEmpty(x.Avatar) ? null : $"{myHostUrl}{x.Avatar}" ,
                Country = x.Country,
                CustomerTypeId = x.CustomerTypeId,
                DateOfBirth = x.DateOfBirth,
                District = x.District,
                Email = x.Email,
                FirstName = x.FirstName,
                InsuranceCode = x.InsuranceCode,
                IdentityCard = x.IdentityCard,
                LastName = x.LastName,
                Note = x.Note,
                PhoneNumber = x.PhoneNumber,
                Province = x.Province,
                Sex = x.Sex,
                Village = x.Village,
                CustomerTypeName = x.CustomerType.Name,
                Created = x.Created
            };
            return model;
        }

        public async Task<IList<CustomerModel>> GetCustomerByIds(int[] ids)
        {
            var customerModels = new List<CustomerModel>();
            foreach (var id in ids)
            {
                var x = await GetCustomer(id);
                if (x != null)
                    customerModels.Add(x);
            }
            return customerModels;
        }

        public IQueryable<CustomerModel> GetCustomersEligible()
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            return repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                             .Include(s => s.SreeningExaminations)
                             .Include(x => x.CustomerType)
                             .Where(a => a.CustomerType.isTrash == false && a.SreeningExaminations.OrderByDescending(y => y.Created).FirstOrDefault(x => x.CustomerId == a.Id && x.isEligible) != null)
                             .Select(x => new CustomerModel
                             {
                                 Id = x.Id,
                                 Address = x.Address,
                                 Avatar = String.IsNullOrEmpty(x.Avatar) ? null : $"{myHostUrl}{x.Avatar}",
                                 Country = x.Country,
                                 CustomerTypeId = x.CustomerTypeId,
                                 DateOfBirth = x.DateOfBirth,
                                 District = x.District,
                                 Email = x.Email,
                                 FirstName = x.FirstName,
                                 InsuranceCode = x.InsuranceCode,
                                 LastName = x.LastName,
                                 Note = x.Note,
                                 PhoneNumber = x.PhoneNumber,
                                 Province = x.Province,
                                 Sex = x.Sex,
                                 Village = x.Village,
                                 CustomerTypeName = x.CustomerType.Name,
                                 Created = x.Created,
                                 IdentityCard = x.IdentityCard,

                             });
        }

        public IQueryable<CustomerModel> GetCustomers()
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            return repository
                 .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(ct => ct.CustomerType)
                 .Include(s => s.SreeningExaminations)
                 .Where(x => x.CustomerType.isTrash == false)
                 .Select(x => new CustomerModel
                 {
                     Id = x.Id,
                     Address = x.Address,
                     Avatar = string.IsNullOrEmpty(x.Avatar) ? null : $"{myHostUrl}{x.Avatar}",
                     Country = x.Country,
                     CustomerTypeId = x.CustomerTypeId,
                     DateOfBirth = x.DateOfBirth,
                     District = x.District,
                     Email = x.Email,
                     FirstName = x.FirstName,
                     InsuranceCode = x.InsuranceCode,
                     LastName = x.LastName,
                     Note = x.Note,
                     PhoneNumber = x.PhoneNumber,
                     Province = x.Province,
                     Sex = x.Sex,
                     Village = x.Village,
                     CustomerTypeName = x.CustomerType.Name,
                     Created = x.Created,
                     IdentityCard = x.IdentityCard,

                 });
        }

        public async Task<bool> InsertCustomerAsync(CustomerModel model)
        {
            var customer = new Customer
            {
                Address = model.Address,
                Avatar = model.Avatar,
                Country = model.Country,
                CustomerTypeId = model.CustomerTypeId,
                DateOfBirth = model.DateOfBirth,
                District = model.District,
                Email = model.Email,
                FirstName = model.FirstName,
                InsuranceCode = model.InsuranceCode,
                LastName = model.LastName,
                Note = model.Note,
                PhoneNumber = model.PhoneNumber,
                Province = model.Province,
                Sex = model.Sex,
                Village = model.Village,
                IdentityCard = model.IdentityCard
            };
            var result = await repository.InsertAsync(customer);
            model.Id = customer.Id;
            model.Created = customer.Created;
            return result;
        }

        public IQueryable<CustomerModel> SearchCustomers(string q = "")
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(ct => ct.CustomerType)
                .Where(x => x.CustomerType.isTrash == false &&
                            ((x.Sex ? "Nam" : "Nữ").ToLower().Equals(q) ||
                            x.IdentityCard.ToLower().Equals(q) ||
                            x.InsuranceCode.ToLower().Equals(q) ||
                            x.Note.ToLower().Contains(q) ||
                            x.Address.ToLower().Contains(q) ||
                            x.CustomerType.Name.Contains(q) ||
                            x.Country.ToLower().Contains(q) ||
                            x.Village.ToLower().Contains(q) ||
                            x.DateOfBirth.ToString().Equals(q) ||
                            x.District.ToLower().Contains(q) ||
                            x.Email.ToLower().Equals(q) ||
                            x.LastName.ToLower().Contains(q) ||
                            x.FirstName.ToLower().Contains(q) ||
                            x.PhoneNumber.ToLower().Equals(q) ||
                            x.Province.ToLower().Contains(q)))
                .Select(x => new CustomerModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    Avatar = string.IsNullOrEmpty(x.Avatar)?null: $"{myHostUrl}{x.Avatar}",
                    Country = x.Country,
                    CustomerTypeId = x.CustomerTypeId,
                    DateOfBirth = x.DateOfBirth,
                    District = x.District,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    InsuranceCode = x.InsuranceCode,
                    LastName = x.LastName,
                    Note = x.Note,
                    PhoneNumber = x.PhoneNumber,
                    Province = x.Province,
                    Sex = x.Sex,
                    Village = x.Village,
                    CustomerTypeName = x.CustomerType.Name,
                    Created = x.Created,
                    IdentityCard = x.IdentityCard,

                });
            return results;
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerModel model)
        {
            var customer = await repository.GetAsync(id);
            customer.Address = model.Address;
            customer.Avatar = model.Avatar;
            customer.Country = model.Country;
            customer.CustomerTypeId = model.CustomerTypeId;
            customer.DateOfBirth = model.DateOfBirth;
            customer.District = model.District;
            customer.Email = model.Email;
            customer.FirstName = model.FirstName;
            customer.InsuranceCode = model.InsuranceCode;
            customer.IdentityCard = model.IdentityCard;
            customer.LastName = model.LastName;
            customer.Note = model.Note;
            customer.PhoneNumber = model.PhoneNumber;
            customer.Province = model.Province;
            customer.Sex = model.Sex;
            customer.Village = model.Village;

            return await repository.UpdateAsync(customer);
        }

        public async Task<CustomerModel> GetCustomerByLoginId(int id)
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var x = await repository
                 .GetAll(Common.Enum.SelectEnum.Select.ALL)
                 .Include(ct => ct.CustomerType)
                 .Include(lg => lg.Login)
                 .Where(x => x.CustomerType.isTrash == false)
                 .FirstOrDefaultAsync(x => x.Login.Id == id);
            var model = new CustomerModel
            {
                Id = x.Id,
                Address = x.Address,
                Avatar = String.IsNullOrEmpty(x.Avatar) ? null : $"{myHostUrl}{x.Avatar}",
                Country = x.Country,
                CustomerTypeId = x.CustomerTypeId,
                DateOfBirth = x.DateOfBirth,
                District = x.District,
                Email = x.Email,
                FirstName = x.FirstName,
                InsuranceCode = x.InsuranceCode,
                LastName = x.LastName,
                Note = x.Note,
                PhoneNumber = x.PhoneNumber,
                Province = x.Province,
                Sex = x.Sex,
                Village = x.Village,
                CustomerTypeName = x.CustomerType.Name,
                Created = x.Created,
                IdentityCard = x.IdentityCard,

            };
            return model;
        }

        public async Task<bool> InsertCustomersRange(IList<CustomerModel> customerModels)
        {
            var customers = new List<Customer>();
            foreach (var model in customerModels)
            {
                customers.Add(new Customer
                {
                    Address = model.Address,
                    Avatar = model.Avatar,
                    Country = model.Country,
                    CustomerTypeId = model.CustomerTypeId,
                    DateOfBirth = model.DateOfBirth,
                    District = model.District,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    InsuranceCode = model.InsuranceCode,
                    LastName = model.LastName,
                    Note = model.Note,
                    PhoneNumber = model.PhoneNumber,
                    Province = model.Province,
                    Sex = model.Sex,
                    Village = model.Village,
                    IdentityCard = model.IdentityCard
                });
            }
            var result = await repository.InsertRangeAsync(customers);
            for (int i = 0; i < customers.Count; i++)
            {
                customerModels[i].Id = customers[i].Id;
                customerModels[i].Created = customers[i].Created;
            }
            return result;
        }
    }
}
