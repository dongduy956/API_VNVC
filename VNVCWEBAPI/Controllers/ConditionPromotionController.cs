using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

    public class ConditionPromotionController : BaseController
    {
        private readonly IConditionPromotionServices conditionPromotionServices;
        public ConditionPromotionController(IConditionPromotionServices conditionPromotionServices)
        {
            this.conditionPromotionServices = conditionPromotionServices;
        }
        [Authorize(Roles = Permissions.ConditionPromotion.View)]
        [HttpGet("[Action]")]
        public IActionResult GetConditionPromotions(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var conditionPromotions = conditionPromotionServices.GetConditionPromotions()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = conditionPromotions.PageCount,
                PageNumber = conditionPromotions.PageNumber,
                TotalItems = conditionPromotions.TotalItemCount,
                Data = conditionPromotions,
            });
        }
        [Authorize(Roles = Permissions.ConditionPromotion.View)]
        [HttpGet("[Action]/{vaccineId}")]
        public IActionResult GetConditionPromotionByVaccineId(int vaccineId)
        {

            var conditionPromotion = conditionPromotionServices.GetConditionPromotionByVaccineId(vaccineId);
            if (conditionPromotion != null)
                return Ok(new ResponseAPIPaging
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = conditionPromotion,
                });
            return Ok(new ResponseAPIPaging
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [Authorize(Roles = Permissions.ConditionPromotion.View)]
        [HttpGet("[Action]/{vaccinePackageId}")]
        public IActionResult GetConditionPromotionByVaccinePackageId(int vaccinePackageId)
        {

            var conditionPromotion = conditionPromotionServices.GetConditionPromotionByVaccinePackageId(vaccinePackageId);
            if (conditionPromotion != null)
                return Ok(new ResponseAPIPaging
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = conditionPromotion,
                });
            return Ok(new ResponseAPIPaging
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }

        [Authorize(Roles = Permissions.ConditionPromotion.View)]
        [HttpGet("[Action]/{paymentMethodId}")]
        public IActionResult GetConditionPromotionByPaymentMethodId(int paymentMethodId)
        {

            var conditionPromotion = conditionPromotionServices.GetConditionPromotionByPaymentMethodId(paymentMethodId);
            if (conditionPromotion != null)
                return Ok(new ResponseAPIPaging
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = conditionPromotion,
                });
            return Ok(new ResponseAPIPaging
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [Authorize(Roles = Permissions.ConditionPromotion.View)]
        [HttpGet("[Action]/{customerRankId}")]
        public IActionResult GetConditionPromotionByCustomerRankId(int customerRankId)
        {

            var conditionPromotion = conditionPromotionServices.GetConditionPromotionByCustomerRankId(customerRankId);
            if (conditionPromotion != null)
                return Ok(new ResponseAPIPaging
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = conditionPromotion,
                });
            return Ok(new ResponseAPIPaging
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }

        [Authorize(Roles = Permissions.ConditionPromotion.View)]
        [HttpGet("[Action]")]
        public IActionResult SearchConditionPromotions(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var conditionPromotions = conditionPromotionServices.SearchConditionPromotions(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = conditionPromotions.PageCount,
                PageNumber = conditionPromotions.PageNumber,
                TotalItems = conditionPromotions.TotalItemCount,
                Data = conditionPromotions,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.ConditionPromotion.Delete)]
        public async Task<IActionResult> DeleteConditionPromotion(int id)
        {
            var result = await conditionPromotionServices.DeleteConditionPromotionAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá điều kiện khuyến mãi thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá điều kiện khuyến mãi." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.ConditionPromotion.Create)]
        public async Task<IActionResult> InsertConditionPromotion(ConditionPromotionModel conditionPromotionModel)
        {
            var result = await conditionPromotionServices.InsertConditionPromotionAsync(conditionPromotionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = conditionPromotionModel,
                    Messages = new string[] { "Thêm điều kiện khuyến mãi thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm điều kiện khuyến mãi thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.ConditionPromotion.Edit)]
        public async Task<IActionResult> UpdateConditionPromotion(int id, ConditionPromotionModel conditionPromotionModel)
        {
            var result = await conditionPromotionServices.UpdateConditionPromotionAsync(id, conditionPromotionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa điều kiện khuyến mãi thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa điều kiện khuyến mãi thất bại." }
            });
        }
    }
}
