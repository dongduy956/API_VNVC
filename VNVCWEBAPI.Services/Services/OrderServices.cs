using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Order> repository;
        private readonly IEntrySlipDetailsServices entrySlipDetailServices;
        public OrderServices(IRepository<Order> repository, IEntrySlipDetailsServices entrySlipDetailServices)
        {
            this.repository = repository;
            this.entrySlipDetailServices = entrySlipDetailServices;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await repository.GetAsync(id);
            return await repository.Delete(order);
        }

        public async Task<OrderModel> GetOrder(int id)
        {
            var order = await repository
                .Where(x => x.Id == id)
                .Include(dt => dt.OrderDetails)
                .Include(st => st.Staff)
                .Include(sl => sl.Supplier)
                .FirstOrDefaultAsync();
            return new OrderModel
            {
                Id = order.Id,
                StaffId = order.StaffId,
                Status = order.Status,
                SupplierId = order.SupplierId,
                StaffName = order.Staff.StaffName,
                SupplierName = order.Supplier.Name,
                Total = order.Total,
                Created = order.Created
            };
        }

        public IQueryable<OrderModel> GetOrders()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                   .Include(dt => dt.OrderDetails)
                    .Include(st => st.Staff)
                    .Include(sl => sl.Supplier)
                    .Select(order => new OrderModel
                    {
                        Id = order.Id,
                        StaffId = order.StaffId,
                        Status = order.Status,
                        SupplierId = order.SupplierId,
                        StaffName = order.Staff.StaffName,
                        SupplierName = order.Supplier.Name,
                        Created = order.Created,
                        Total = order.Total
                    });
        }

        public async Task<bool> InsertOrder(OrderModel orderModel)
        {
            var order = new Order
            {
                StaffId = orderModel.StaffId,
                Status = 0,
                SupplierId = orderModel.SupplierId
            };
            var result = await repository.InsertAsync(order);
            orderModel.Id = order.Id;
            orderModel.Created = order.Created;
            return result;
        }

        public IQueryable<OrderModel> SearchOrders(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(dt => dt.OrderDetails)
                .Include(st => st.Staff)
                .Include(sl => sl.Supplier)
                .Where(x => (x.Status == 2 ? "đã nhập" : x.Status == 0 ? "chưa nhập" : "đang nhập").Contains(q) ||
                            x.Supplier.Name.ToLower().Contains(q) ||
                            x.Staff.StaffName.ToLower().Contains(q) ||                          
                            x.Created.ToString().Contains(q))
                .Select(order => new OrderModel
                {
                    Id = order.Id,
                    StaffId = order.StaffId,
                    Status = order.Status,
                    SupplierId = order.SupplierId,
                    StaffName = order.Staff.StaffName,
                    SupplierName = order.Supplier.Name,
                    Total = order.Total,
                    Created = order.Created
                });
            return results;
        }

        public async Task<bool> UpdateOrderStatus(int id)
        {
            var check = 2;
            var order = await repository.Where(x => x.Id == id)
                .Include(es => es.EntrySlips)
                .ThenInclude(esd => esd.EntrySlipDetails)
                .Include(od => od.OrderDetails)
                .FirstOrDefaultAsync();
            if (order == null) return false;
            var entrySlips = order.EntrySlips;
            if (entrySlips == null || entrySlips.Count() == 0) return false;
            var orderDetails = order.OrderDetails;
            var listEntrySlipDetails = new List<EntrySlipDetailsModel>();
            foreach (var entrySlip in entrySlips)
            {
                var entrySlipDetails = entrySlipDetailServices
                    .GetEntrySlipDetailsByEntrySlipId(entrySlip.Id);
                foreach (var entrySlipDetail in entrySlipDetails)
                {
                    var index = listEntrySlipDetails.FindIndex(x => x.ShipmentId == entrySlipDetail.ShipmentId && x.VaccineId == entrySlipDetail.VaccineId);
                    if (index == -1)
                        listEntrySlipDetails.Add(entrySlipDetail);
                    else
                        listEntrySlipDetails[index].Number += entrySlipDetail.Number;
                }
            }
            foreach (var orderDetail in orderDetails)
            {
                var entrySlipDetail = listEntrySlipDetails
                                      .FirstOrDefault(x => x.VaccineId == orderDetail.VaccineId && x.ShipmentId == orderDetail.ShipmentId);
                if (entrySlipDetail == null || orderDetail.Number != entrySlipDetail.Number)
                {
                    check = 1;
                    break;
                }
            }
            if ((check == 1 && order.Status != 1) || check == 2)
            {
                order.Status = check;

                return await repository.UpdateAsync(order);
            }
            else
                return true;

        }
    }
}