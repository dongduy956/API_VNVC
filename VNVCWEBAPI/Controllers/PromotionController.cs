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
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionServices promotionServices;
        public PromotionController(IPromotionServices promotionServices)
        {
            this.promotionServices = promotionServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Promotion.View)]
        public IActionResult GetAllPromotions()
        {
            var promotions = promotionServices.GetPromotions()
                                                .Where(x=>x.EndDate.CompareTo(DateTime.Now)>=0)
                                            .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                Data = promotions,
                isSuccess = true,
            });
        }
        [Authorize(Roles = Permissions.Promotion.View)]
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetPromotion(int id)
        {
            var promotion = await promotionServices.GetPromotion(id);
            if (promotion != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    Data = promotion,
                    isSuccess = true,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Promotion.View)]
        public IActionResult GetPromotions(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var promotions = promotionServices.GetPromotions()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = promotions.PageCount,
                PageNumber = promotions.PageNumber,
                TotalItems = promotions.TotalItemCount,
                Data = promotions,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Promotion.View)]
        public IActionResult SearchPromotions(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var promotions = promotionServices.SearchPromotions(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = promotions.PageCount,
                PageNumber = promotions.PageNumber,
                TotalItems = promotions.TotalItemCount,
                Data = promotions,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Promotion.Delete)]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var result = await promotionServices.DeletePromotion(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá khuyến mãi thành công." },
                    StatusCode = Ok().StatusCode
                });

            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá khuyến mãi." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Promotion.Create)]
        public async Task<IActionResult> InsertPromotion(PromotionModel promotionModel)
        {
            var result = await promotionServices.InsertPromotion(promotionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = promotionModel,
                    Messages = new string[] { "Thêm khuyến mãi thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm khuyến mãi thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Promotion.Edit)]
        public async Task<IActionResult> UpdatePromotion(int id, PromotionModel promotionModel)
        {
            var result = await promotionServices.UpdatePromotion(id, promotionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa khuyến mãi thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa khuyến mãi thất bại." }
            });
        }
        [HttpPut("[Action]")]
        [Authorize(Roles = Permissions.Promotion.Edit)]
        public async Task<IActionResult> UpdateCountPromotionsRange(int[] ids)
        {
            var result = await promotionServices.UpdateCountPromotionsRange(ids);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa khuyến mãi thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa khuyến mãi thất bại." }
            });
        }
    }
}
