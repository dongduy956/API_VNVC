using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ICartServices
    {
        IQueryable<CartModel> GetCarts();
        CartModel? GetCart(int id);
        Task<bool> InsertCart(CartModel cartModel);
        Task<bool> DeleteCartAsync(int id, int userId);
        Task<bool> DeleteCartRangeAsync(int[] ids, int userId);
    }
}
