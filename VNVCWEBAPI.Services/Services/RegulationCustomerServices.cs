using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class RegulationCustomerServices : IRegulationCustomerServices
    {
        private readonly IRepository<RegulationCustomer> repository;
        public RegulationCustomerServices(IRepository<RegulationCustomer> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteRegulationCustomer(int id)
        {
            var regulationCustomer = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(regulationCustomer);
        }

        public async Task<bool> DeleteRegulationCustomersRange(int[] ids)
        {
            var regulationCustomers = new List<RegulationCustomer>();
            foreach (var id in ids)
            {
                var regulationCustomer = await repository.GetAsync(id);
                if (regulationCustomer != null)
                {
                    regulationCustomers.Add(regulationCustomer);
                }
            }
            return await repository.DeleteFromTrashRange(regulationCustomers);
        }

        public async Task<RegulationCustomerModel> GetRegulationCustomer(int id)
        {
            var regulationCustomer = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Include(ct => ct.CustomerType)
                .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false && x.CustomerType.isTrash == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            return new RegulationCustomerModel
            {
                Id = regulationCustomer.Id,
                Amount = regulationCustomer.Amount,
                CustomerTypeId = regulationCustomer.CustomerTypeId,
                VaccineId = regulationCustomer.VaccineId,
                CustomerTypeName = regulationCustomer.CustomerType.Name,
                VaccineName = regulationCustomer.Vaccine.Name,
                Created = regulationCustomer.Created,
            };
        }

        public async Task<RegulationCustomerModel?> GetRegulationCustomerByCustomerTypeIdAndVaccineId(int customerTypeId, int vaccineId)
        {
            return await GetRegulationCustomers()
                .FirstOrDefaultAsync(x => x.VaccineId == vaccineId && x.CustomerTypeId == customerTypeId);
        }

        public IQueryable<RegulationCustomerModel> GetRegulationCustomers()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Include(vc => vc.Vaccine)
                        .ThenInclude(x => x.TypeOfVaccine)
                    .Include(ct => ct.CustomerType)
                    .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false && x.CustomerType.isTrash == false)
                    .Select(regulationCustomer => new RegulationCustomerModel
                    {
                        Id = regulationCustomer.Id,
                        Amount = regulationCustomer.Amount,
                        CustomerTypeId = regulationCustomer.CustomerTypeId,
                        VaccineId = regulationCustomer.VaccineId,
                        CustomerTypeName = regulationCustomer.CustomerType.Name,
                        VaccineName = regulationCustomer.Vaccine.Name,
                        Created = regulationCustomer.Created

                    });
        }

        public async Task<bool> InsertRegulationCustomer(RegulationCustomerModel regulationCustomerModel)
        {
            var regulationCustomer = new RegulationCustomer
            {
                VaccineId = regulationCustomerModel.VaccineId ?? 0,
                Amount = regulationCustomerModel.Amount,
                CustomerTypeId = regulationCustomerModel.CustomerTypeId ?? 0,
            };
            var result = await repository.InsertAsync(regulationCustomer);
            regulationCustomerModel.Id = regulationCustomer.Id;
            regulationCustomerModel.Created = regulationCustomer.Created;
            return result;
        }

        public async Task<bool> InsertRegulationCustomersRange(IList<RegulationCustomerModel> regulationCustomerModels)
        {
            var regulationCustomers = new List<RegulationCustomer>();
            foreach (var regulationCustomerModel in regulationCustomerModels)
            {
                regulationCustomers.Add(new RegulationCustomer
                {
                    VaccineId = regulationCustomerModel.VaccineId ?? 0,
                    Amount = regulationCustomerModel.Amount,
                    CustomerTypeId = regulationCustomerModel.CustomerTypeId ?? 0,
                });
            }
            var result = await repository.InsertRangeAsync(regulationCustomers);
            for (int i = 0; i < regulationCustomers.Count; i++)
            {
                regulationCustomerModels[i].Id = regulationCustomers[i].Id;
                regulationCustomerModels[i].Created = regulationCustomers[i].Created;
            }
            return result;
        }

        public IQueryable<RegulationCustomerModel> SearchRegulationCustomers(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Include(ct => ct.CustomerType)
                .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false && x.CustomerType.isTrash == false &&
                        (x.Amount.ToString().Equals(q) ||
                        x.CustomerType.Name.ToLower().Contains(q) ||
                        x.Vaccine.Name.ToLower().Contains(q)))
                .Select(model => new RegulationCustomerModel
                {
                    Id = model.Id,
                    Amount = model.Amount,
                    CustomerTypeId = model.CustomerTypeId,
                    VaccineId = model.VaccineId,
                    CustomerTypeName = model.CustomerType.Name,
                    VaccineName = model.Vaccine.Name,
                    Created = model.Created

                });
            return results;
        }

        public async Task<bool> UpdateRegulationCustomer(int id, RegulationCustomerModel regulationCustomerModel)
        {
            var regulationCustomer = await repository.GetAsync(id);

            regulationCustomer.Amount = regulationCustomerModel.Amount;

            return await repository.UpdateAsync(regulationCustomer);
        }
    }
}
