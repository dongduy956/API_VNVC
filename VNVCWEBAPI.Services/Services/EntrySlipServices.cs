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
    public class EntrySlipServices : IEntrySlipServices
    {
        private readonly IRepository<EntrySlip> repository;
        public EntrySlipServices(IRepository<EntrySlip> repository, IEntrySlipDetailsServices entrySlipDetailsServices)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteEntrySlipAsync(int id)
        {
            var entrySlip = await repository.GetAsync(id);
            return await repository.Delete(entrySlip);
        }



        public async Task<EntrySlipModel> GetEntrySlip(int id)
        {
            var entrySlip = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(st => st.Staff)
                .Include(sp => sp.Supplier)
                .Include(ed => ed.EntrySlipDetails)
                .FirstOrDefaultAsync(x => x.Id == id);

            var model = new EntrySlipModel
            {
                Id = entrySlip.Id,
                OrderId = entrySlip.OrderId,
                SupplierId = entrySlip.SupplierId,
                StaffId = entrySlip.StaffId,
                StaffName = entrySlip.Staff.StaffName,
                SupplierName = entrySlip.Supplier.Name,
                Created = entrySlip.Created,
                Total = entrySlip.Total
            };
            return model;
        }

        public IQueryable<EntrySlipModel> GetEntrySlips()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Include(st => st.Staff)
                    .Include(sp => sp.Supplier)
                    .Include(ed => ed.EntrySlipDetails)

                    .Select(entrySlip => new EntrySlipModel
                    {
                        Id = entrySlip.Id,
                        OrderId = entrySlip.OrderId,
                        SupplierId = entrySlip.SupplierId,
                        StaffId = entrySlip.StaffId,
                        StaffName = entrySlip.Staff.StaffName,
                        SupplierName = entrySlip.Supplier.Name,
                        Created = entrySlip.Created,
                        Total = entrySlip.Total,
                    });
        }

        public IQueryable<EntrySlipModel> GetEntrySlipsByOrderId(int orderId)
        {
            return GetEntrySlips().Where(x => x.OrderId == orderId);
        }

        public async Task<bool> InsertEntrySlipAsync(EntrySlipModel model)
        {
            var EntrySlip = new EntrySlip
            {
                StaffId = model.StaffId,
                OrderId = model.OrderId,
                SupplierId = model.SupplierId,
            };
            var result = await repository.InsertAsync(EntrySlip);
            model.Id = EntrySlip.Id;
            model.Created = EntrySlip.Created;
            return result;
        }

        public IQueryable<EntrySlipModel> SearchEntrySlips(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(st => st.Staff)
                .Include(sp => sp.Supplier)
                .Include(ed => ed.EntrySlipDetails)
                .Where(x => x.Supplier.Name.ToLower().Contains(q) ||
                            x.Staff.StaffName.ToLower().Contains(q) ||
                            x.Created.ToString().Contains(q))
                .OrderBy(x => x.Id)
                .Select(entrySlip => new EntrySlipModel
                {
                    Id = entrySlip.Id,
                    OrderId = entrySlip.OrderId,
                    SupplierId = entrySlip.SupplierId,
                    StaffId = entrySlip.StaffId,
                    StaffName = entrySlip.Staff.StaffName,
                    SupplierName = entrySlip.Supplier.Name,
                    Created = entrySlip.Created,
                    Total = entrySlip.Total
                });
            return results;
        }
    }
}
