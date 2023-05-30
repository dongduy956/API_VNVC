using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IOrderDetailServices
    {
        Task<bool> DeleteOrderDetailsByOrderId(int orderId);
        Task<bool> DeleteOrderDetail(int id);
        IQueryable<OrderDetailModel> GetOrderDetailsByOrderId(int orderId);
        OrderDetailModel? GetOrderDetailsByOrderIdVaccineIdShipmentId(int orderId, int vaccineId,int shipmentId);

        Task<OrderDetailModel> GetOrderDetail(int id);
        IQueryable<OrderDetailModel> GetOrderDetails();
        Task<bool> InsertOrderDetail(OrderDetailModel orderDetailModel);
        Task<bool> InsertOrderDetailsRange(IList<OrderDetailModel> orderDetailModels);
        Task<bool> UpdateOrderDetail(int id, OrderDetailModel orderDetailModel);
        Task<bool> DeleteOrderDetailsRange(int[] ids);
    }
}
