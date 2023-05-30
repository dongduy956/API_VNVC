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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodServices paymentMethodServices;
        public PaymentMethodController(IPaymentMethodServices paymentMethodServices)
        {
            this.paymentMethodServices = paymentMethodServices;
        }
        [Authorize(Roles = Permissions.PaymentMethod.View)]
        [HttpGet("[Action]")]
        public IActionResult GetAllPaymentMethods()
        {
            var paymentMethods = paymentMethodServices.GetPaymentMethods()
                               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                Data = paymentMethods,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }
        [Authorize(Roles = Permissions.PaymentMethod.View)]
        [HttpGet("[Action]")]
        public IActionResult GetPaymentMethods(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var paymentMethods = paymentMethodServices.GetPaymentMethods()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = paymentMethods.PageCount,
                PageNumber = paymentMethods.PageNumber,
                TotalItems = paymentMethods.TotalItemCount,
                Data = paymentMethods,
            });
        }
        [Authorize(Roles = Permissions.PaymentMethod.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchPaymentMethods(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var paymentMethods = paymentMethodServices.SearchPaymentMethods(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = paymentMethods.PageCount,
                PageNumber = paymentMethods.PageNumber,
                TotalItems = paymentMethods.TotalItemCount,
                Data = paymentMethods,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.PaymentMethod.Delete)]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var result = await paymentMethodServices.DeletePaymentMethod(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá phương thức thanh toán thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá phương thức thanh toán." }
            });
        }

        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.PaymentMethod.Create)]
        public async Task<IActionResult> InsertPaymentMethod(PaymentMethodModel paymentMethodModel)
        {
            var result = await paymentMethodServices.InsertPaymentMethod(paymentMethodModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = paymentMethodModel,
                    Messages = new string[] { "Thêm phương thức thanh toán thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm phương thức thanh toán thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.PaymentMethod.Create)]
        public async Task<IActionResult> InsertPaymentMethodsRange(IList<PaymentMethodModel> paymentMethodModels)
        {
            var result = await paymentMethodServices.InsertPaymentMethodsRange(paymentMethodModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = paymentMethodModels,
                    Messages = new string[] { "Thêm phương thức thanh toán thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm phương thức thanh toán thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.PaymentMethod.Edit)]
        public async Task<IActionResult> UpdatePaymentMethod(int id, PaymentMethodModel paymentMethodModel)
        {
            var result = await paymentMethodServices.UpdatePaymentMethod(id, paymentMethodModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa phương thức thanh toán thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa phương thức thanh toán thất bại." }
            });
        }
    }
}
