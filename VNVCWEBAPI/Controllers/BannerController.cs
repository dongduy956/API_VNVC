using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : BaseController
    {
        private readonly IBannerServices bannerServices;
        public BannerController(IBannerServices bannerServices)
        {
            this.bannerServices = bannerServices;
        }
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetBanner(int id)
        {
            var banner = await bannerServices.GetBannerAsync(id);
            if (banner != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = banner,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
            });
        }
        [HttpGet("[Action]")]
        public IActionResult GetBanners(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var banners = bannerServices.GetBanners()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = banners.PageCount,
                PageNumber = banners.PageNumber,
                TotalItems = banners.TotalItemCount,
                Data = banners,
            });
        }
        [HttpDelete("[Action]/{id}"),Authorize(Roles =Permissions.Banner.Delete)]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var result = await bannerServices.DeleteBannerAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá banner thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá banner." }
            });
        }
        [HttpDelete("[Action]")]
        [HttpDelete("[Action]/{id}"), Authorize(Roles = Permissions.Banner.Delete)]

        public async Task<IActionResult> DeleteBannerRange(int[] ids)
        {
            var result = await bannerServices.DeleteBannerRangeAsync(ids);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá banner thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá banner." }
            });
        }
        [HttpPost("[Action]")]
        [HttpDelete("[Action]/{id}"), Authorize(Roles = Permissions.Banner.Create)]

        public async Task<IActionResult> InsertBanner(BannerModel bannerModel)
        {
            var result = await bannerServices.InsertBanner(bannerModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = bannerModel,
                    Messages = new string[] { "Thêm banner thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                isSuccess = false,
                Messages = new string[] { "Thêm banner thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [HttpDelete("[Action]/{id}"), Authorize(Roles = Permissions.Banner.Edit)]

        public async Task<IActionResult> UpdateBanner(int id, BannerModel bannerModel)
        {
            var result = await bannerServices.UpdateBannerAsync(id, bannerModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa banner thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa banner thất bại." }
            });
        }
    }
}
