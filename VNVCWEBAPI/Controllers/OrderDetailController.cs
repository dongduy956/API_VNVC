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
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailServices orderDetailServices;
        public OrderDetailController(IOrderDetailServices orderDetailServices)
        {
            this.orderDetailServices = orderDetailServices;
        }
        [Authorize(Roles = Permissions.OrderDetail.View)]
        [HttpGet("[Action]/{orderId}")]
        public IActionResult GetOrderDetailsByOrderId(int orderId)
        {

            var orders = orderDetailServices.GetOrderDetailsByOrderId(orderId)
                .OrderBy(x => x.Id);

            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = orders,
            });
        }
        [Authorize(Roles = Permissions.OrderDetail.View)]
        [HttpGet("[Action]")]
        public IActionResult GetOrderDetailsByOrderIdVaccineIdShipmentId(int orderId,int vaccineId,int shipmentId)
        {
            var order = orderDetailServices.GetOrderDetailsByOrderIdVaccineIdShipmentId(orderId, vaccineId, shipmentId);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = order,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.OrderDetail.Delete)]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var result = await orderDetailServices.DeleteOrderDetail(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chi tiết đặt hàng thành công." },
                    StatusCode = Ok().StatusCode
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Xoá chi tiết đặt hàng thất bại." }
            });
        }
        [HttpDelete("[Action]/{orderId}")]
        [Authorize(Roles = Permissions.OrderDetail.Delete)]
        public async Task<IActionResult> DeleteOrderDetailsByOrderId(int orderId)
        {
            var result = await orderDetailServices.DeleteOrderDetailsByOrderId(orderId);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.OrderDetail.Create)]
        public async Task<IActionResult> InsertOrderDetail(OrderDetailModel orderDetailModel)
        {
            var result = await orderDetailServices.InsertOrderDetail(orderDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = orderDetailModel,
                    Messages = new string[] { "Thêm chi tiết đặt hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết đặt hàng thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.OrderDetail.Create)]
        public async Task<IActionResult> InsertOrderDetailsRange(IList<OrderDetailModel> orderDetailModels)
        {
            var result = await orderDetailServices.InsertOrderDetailsRange(orderDetailModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = orderDetailModels,
                    Messages = new string[] { "Thêm chi tiết đặt hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết đặt hàng thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.OrderDetail.Edit)]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailModel orderDetailModel)
        {
            var result = await orderDetailServices.UpdateOrderDetail(id, orderDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa chi tiết đặt hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa chi tiết đặt hàng thất bại." }
            });
        }

    }
}
