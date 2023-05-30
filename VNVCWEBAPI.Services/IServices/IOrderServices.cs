using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IOrderServices
    {
        IQueryable<OrderModel> GetOrders();
        IQueryable<OrderModel> SearchOrders(string q = "");
        Task<OrderModel> GetOrder(int id);
        Task<bool> InsertOrder(OrderModel orderModel);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrderStatus(int id);
    }
}
