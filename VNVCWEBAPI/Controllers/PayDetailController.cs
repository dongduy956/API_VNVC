using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.Services;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayDetailController : ControllerBase
    {
        private readonly IPayDetailServices payDetailServices;
        public PayDetailController(IPayDetailServices payDetailServices)
        {
            this.payDetailServices = payDetailServices;
        }

        [HttpGet("[Action]/{payId}")]
        [Authorize(Roles = Permissions.PayDetail.View)]
        public IActionResult GetPayDetails(int payId)
        {

            var payDetails = payDetailServices.GetPayDetails(payId)
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = payDetails,
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.PayDetail.Create)]
        public async Task<IActionResult> InsertPayDetail(PayDetailModel payDetailModel)
        {
            var result = await payDetailServices.InsertPayDetail(payDetailModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = payDetailModel,
                    Messages = new string[] { "Thêm chi tiết thanh toán thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết thanh toán thất bại." }
            });
        }
        [Authorize(Roles = Permissions.PayDetail.Create)]
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertPayDetailsRange(IList<PayDetailModel> payDetailModels)
        {
            var result = await payDetailServices.InsertPayDetailsRange(payDetailModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = payDetailModels,
                    Messages = new string[] { "Thêm chi tiết thanh toán thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chi tiết thanh toán thất bại." }
            });
        }
    }
}
