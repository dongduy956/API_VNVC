using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices orderServices;
        public OrderController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }
        [Authorize(Roles = Permissions.Order.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await orderServices.GetOrder(id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = order,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Order.View)]
        public IActionResult GetAllOrders(bool checkStatus = true)
        {

            var orders = orderServices
                         .GetOrders()
                         .OrderBy(x => x.Id).ToList();
            if (checkStatus)
                orders = orders.Where(x => x.Status < 2).ToList();
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = orders,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Order.View)]
        public IActionResult GetOrders(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var orders = orderServices.GetOrders()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = orders.PageCount,
                PageNumber = orders.PageNumber,
                TotalItems = orders.TotalItemCount,
                Data = orders,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Order.View)]
        public IActionResult SearchOrders(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var orders = orderServices.SearchOrders(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = orders.PageCount,
                PageNumber = orders.PageNumber,
                TotalItems = orders.TotalItemCount,
                Data = orders,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Order.Delete)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await orderServices.DeleteOrder(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá phiếu đặt hàng thành công." },
                    StatusCode = Ok().StatusCode
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá phiếu đặt hàng." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Order.Create)]
        public async Task<IActionResult> InsertOrder(OrderModel orderModel)
        {
            var result = await orderServices.InsertOrder(orderModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = orderModel,
                    Messages = new string[] { "Thêm phiếu đặt hàng thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm phiếu đặt hàng thất bại." }
            });
        }

        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Order.Edit)]
        public async Task<IActionResult> UpdateOrderStatus(int id)
        {
            var result = await orderServices.UpdateOrderStatus(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Messages = new string[] { "Cập nhật trạng thái phiếu đặt hàng thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Cập nhật trạng thái phiếu đặt hàng thất bại." }
            });
        }
    }
}
