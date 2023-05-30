using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace VNVCWEBAPI.Services.Services
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IRepository<Supplier> repository;
        public SupplierServices(IRepository<Supplier> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteSupplier(int id)
        {
            var supplier = await repository.GetAsync(id);
            return await repository.Delete(supplier);
        }

        public async Task<SupplierModel> GetSupplier(int id)
        {
            var supplier = await repository.GetAsync(id);
            return new SupplierModel
            {
                Id = supplier.Id,
                Address = supplier.Address,
                Name = supplier.Name,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber,
                TaxCode = supplier.TaxCode,
                Created = supplier.Created
            };
        }

        public IQueryable<SupplierModel> GetSuppliers()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Select(supplier => new SupplierModel
                {
                    Id = supplier.Id,
                    Address = supplier.Address,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    PhoneNumber = supplier.PhoneNumber,
                    TaxCode = supplier.TaxCode,
                    Created = supplier.Created

                });
        }

        public async Task<bool> InsertSupplier(SupplierModel supplierModel)
        {
            var supplier = new Supplier
            {
                Name = supplierModel.Name,
                Address = supplierModel.Address,
                Email = supplierModel.Email,
                PhoneNumber = supplierModel.PhoneNumber,
                TaxCode = supplierModel.TaxCode,
            };
            var result = await repository.InsertAsync(supplier);
            supplierModel.Id = supplier.Id;
            supplierModel.Created = supplier.Created;
            return result;
        }

        public async Task<bool> InsertSuppliersRange(IList<SupplierModel> supplierModels)
        {
            var suppliers = new List<Supplier>();
            foreach (var supplierModel in supplierModels)
            {
                suppliers.Add(new Supplier
                {
                    Name = supplierModel.Name,
                    Address = supplierModel.Address,
                    Email = supplierModel.Email,
                    PhoneNumber = supplierModel.PhoneNumber,
                    TaxCode = supplierModel.TaxCode,
                });
            }
            var result = await repository.InsertRangeAsync(suppliers);
            for (int i = 0; i < suppliers.Count; i++)
            {
                supplierModels[i].Id = suppliers[i].Id;
                supplierModels[i].Created = suppliers[i].Created;
            }
            return result;
        }

        public IQueryable<SupplierModel> SearchSuppliers(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.Name.Trim().ToLower().Contains(q) ||
                        x.Address.ToLower().Contains(q) ||
                        x.Email.ToLower().Equals(q) ||
                        x.PhoneNumber.ToLower().Equals(q) ||
                        x.TaxCode.ToLower().Equals(q))
                .Select(supplierModel => new SupplierModel
                {
                    Id = supplierModel.Id,
                    Name = supplierModel.Name,
                    Address = supplierModel.Address,
                    Email = supplierModel.Email,
                    PhoneNumber = supplierModel.PhoneNumber,
                    TaxCode = supplierModel.TaxCode,
                    Created = supplierModel.Created

                });
            return results;
        }

        public async Task<bool> UpdateSupplier(int id, SupplierModel supplierModel)
        {
            var supplier = await repository.GetAsync(id);
            supplier.Name = supplierModel.Name;
            supplier.Address = supplierModel.Address;
            supplier.Email = supplierModel.Email;
            supplier.PhoneNumber = supplierModel.PhoneNumber;
            supplier.TaxCode = supplierModel.TaxCode;
            return await repository.UpdateAsync(supplier);
        }
    }
}
