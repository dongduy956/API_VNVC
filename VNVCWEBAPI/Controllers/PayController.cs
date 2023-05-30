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
    public class PayController : ControllerBase
    {
        private readonly IPayServices payServices;
        public PayController(IPayServices payServices)
        {
            this.payServices = payServices;
        }
        [Authorize(Roles = Permissions.Pay.View)]
        [HttpGet("[Action]")]
        public IActionResult GetPay(int id)
        {
            var pay = payServices.GetPay(id);
            if (pay != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = pay,
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [Authorize(Roles = Permissions.Pay.View)]
        [HttpGet("[Action]/{injectionScheduleId}")]
        public IActionResult GetPayByInjectionScheduleId(int injectionScheduleId)
        {

            var pay = payServices.GetPayByInjectionScheduleId(injectionScheduleId);
            if (pay != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = pay,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Pay.View)]
        public IActionResult GetPays(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var pays = payServices.GetPays()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = pays.PageCount,
                PageNumber = pays.PageNumber,
                TotalItems = pays.TotalItemCount,
                Data = pays,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Pay.View)]
        public IActionResult SearchPays(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var pays = payServices.SearchPays(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = pays.PageCount,
                PageNumber = pays.PageNumber,
                TotalItems = pays.TotalItemCount,
                Data = pays,
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Pay.Create)]
        public async Task<IActionResult> InsertPay(PayModel payModel)
        {
            var result = await payServices.InsertPay(payModel);
            if (result)
            {
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = payModel,
                    Messages = new string[] { "Thêm thanh toán thành công." }
                });
            }
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm thanh toán thất bại." }
            });
        }
    }
}
