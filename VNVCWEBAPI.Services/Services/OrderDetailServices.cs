using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly IRepository<OrderDetails> repository;
        public OrderDetailServices(IRepository<OrderDetails> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteOrderDetail(int id)
        {
            var orderDetail = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(orderDetail);
        }

        public async Task<bool> DeleteOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                                         .Where(x => x.OrderId == orderId);
            return await repository.DeleteFromTrashRange(orderDetails);
        }

        public async Task<bool> DeleteOrderDetailsRange(int[] ids)
        {
            var orderDetails = new List<OrderDetails>();
            foreach (var id in ids)
            {
                var orderDetail = await repository.GetAsync(id);
                if (orderDetail != null)
                {
                    orderDetails.Add(orderDetail);
                }
            }
            return await repository.DeleteFromTrashRange(orderDetails);
        }

        public async Task<OrderDetailModel> GetOrderDetail(int id)
        {
            var orderDetail = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(vc => vc.Vaccine)
                .Include(sm => sm.Shipment)
                .FirstOrDefaultAsync(x => x.Id == id);
            return new OrderDetailModel
            {
                Id  = orderDetail.Id,
                Number = orderDetail.Number,
                OrderId = orderDetail.OrderId,
                Price = orderDetail.Price,
                ShipmentId = orderDetail.ShipmentId,
                ShipmentCode = orderDetail.Shipment.ShipmentCode,
                VaccineId = orderDetail.VaccineId,
                VaccineName = orderDetail.Vaccine.Name,
                Created = orderDetail.Created
            };
        }

        public IQueryable<OrderDetailModel> GetOrderDetails()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Include(vc => vc.Vaccine)
                    .Include(sm => sm.Shipment)
                    .Select(orderDetail => new OrderDetailModel
                    {
                        Id = orderDetail.Id,
                        Number = orderDetail.Number,
                        OrderId = orderDetail.OrderId,
                        Price = orderDetail.Price,
                        ShipmentId = orderDetail.ShipmentId,
                        ShipmentCode = orderDetail.Shipment.ShipmentCode,
                        VaccineName = orderDetail.Vaccine.Name,
                        Created = orderDetail.Created,
                        VaccineId = orderDetail.VaccineId,

                    });
        }

        public IQueryable<OrderDetailModel> GetOrderDetailsByOrderId(int orderId)
        {
            return GetOrderDetails().Where(x => x.OrderId == orderId);
        }

        public OrderDetailModel? GetOrderDetailsByOrderIdVaccineIdShipmentId(int orderId, int vaccineId,int shipmentId)
        {
            return GetOrderDetails().FirstOrDefault(x => x.OrderId == orderId && x.VaccineId==vaccineId && x.ShipmentId==shipmentId );
        }

        public async Task<bool> InsertOrderDetail(OrderDetailModel model)
        {
            var orderDetail = new OrderDetails()
            {
                Number = model.Number,
                OrderId = model.OrderId,
                Price = model.Price,
                ShipmentId = model.ShipmentId,
                VaccineId = model.VaccineId,
            };
            var result = await repository.InsertAsync(orderDetail);
            model.Id = orderDetail.Id;
            model.Created = orderDetail.Created;
            return result;
        }

        public async Task<bool> InsertOrderDetailsRange(IList<OrderDetailModel> orderDetailModels)
        {
            var orderDetails = new List<OrderDetails>();
            foreach (var orderDetail in orderDetailModels)
            {
                orderDetails.Add(new OrderDetails
                {
                    Number = orderDetail.Number,
                    OrderId = orderDetail.OrderId,
                    Price = orderDetail.Price,
                    ShipmentId = orderDetail.ShipmentId,
                    VaccineId = orderDetail.VaccineId,
                });
            }
            var result = await repository.InsertRangeAsync(orderDetails);
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetailModels[i].Id = orderDetails[i].Id;
                orderDetailModels[i].Created = orderDetails[i].Created;
            }
            return result;
        }

        public async Task<bool> UpdateOrderDetail(int id, OrderDetailModel orderDetailModel)
        {
            var orderDetail = await repository.GetAsync(id);

            orderDetail.Number = orderDetailModel.Number;

            return await repository.UpdateAsync(orderDetail);
        }


    }
}
