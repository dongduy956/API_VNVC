using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;
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
    public class CartController : BaseController
    {
        private readonly ICartServices cartServices;
        public CartController(ICartServices cartServices)
        {
            this.cartServices = cartServices;
        }
        [HttpGet("[Action]/{id}")]
        public IActionResult GetCart(int id)
        {
            var cart = cartServices.GetCart(id);
            if (cart != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = cart,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [HttpGet("[Action]")]
        public IActionResult GetCarts(int? page, int? pageSize)
        {
            var StringUserId = User.Claims
    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            int.TryParse(StringUserId, out int userId);
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var cart = cartServices.GetCarts()
                .Where(x => x.LoginId == userId)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = cart.PageCount,
                PageNumber = cart.PageNumber,
                TotalItems = cart.TotalItemCount,
                Data = cart,
            });
        }
        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var StringUserId = User.Claims
    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            int.TryParse(StringUserId, out int userId);
            var result = await cartServices.DeleteCartAsync(id, userId);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá giỏ hàng thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá giỏ hàng." }
            });
        }
        [HttpDelete("[Action]")]
        public async Task<IActionResult> DeleteCartRange(int[] ids)
        {
            var StringUserId = User.Claims
    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            int.TryParse(StringUserId, out int userId);
            var result = await cartServices.DeleteCartRangeAsync(ids, userId);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá giỏ hàng thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá giỏ hàng." }
            });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertCart(CartModel model)
        {
            var StringUserId = User.Claims
    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            int.TryParse(StringUserId, out int userId);
            model.LoginId = userId;
            var checkCart = cartServices.GetCarts()
                .FirstOrDefault(x => x.VaccineId == model.VaccineId
                &&x.LoginId==model.LoginId);
            if (checkCart != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = (int)HttpStatusCode.Found,
                    isSuccess = false,
                    Messages = new string[] { "Đã có vaccine này trong giỏ hàng" }
                });
            var result = await cartServices.InsertCart(model);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = model,
                    Messages = new string[] { "Thêm giỏ hàng thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm giỏ hàng thất bại." }
            });
        }
    }
}
