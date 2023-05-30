using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;
using X.PagedList;

namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionDetailsController : ControllerBase
    {
        private readonly IPermissionDetailsServices services;
        private readonly IActionDescriptorCollectionProvider collectionProvider;
        public PermissionDetailsController(IPermissionDetailsServices services, IActionDescriptorCollectionProvider collectionProvider)
        {
            this.services = services;
            this.collectionProvider = collectionProvider;
        }
        private async Task<List<PermissionPage>> GetAllControllerName(int permissionId)
        {
            //Get tất cả tên controller
            var datas = collectionProvider.ActionDescriptors.Items.Select(s => s.RouteValues["controller"]).Distinct().ToList();
            //Khởi tạo list perrmission
            var listPaged = new List<PermissionPage>();
            foreach (var data in datas)
            {
                //Khởi tạo mới tên quyền của trang
                var perpage = new PermissionPage
                {
                    //Gán tên name controller vào pageName
                    PageName = data
                };
                //khởi tạo các quyền dựa vào controller name
                var perdatas = Permissions.GeneratePermissionsList(data);
                var listPermissionDetails = services.GetPermissionDetailsByPermissionId(permissionId);
                foreach (var perdata in perdatas)
                {
                    var check = await listPermissionDetails.FirstOrDefaultAsync(x => x.PermissionValue.Equals(perdata));
                    perpage.PermissionPageDetails.Add(new PermissionPageDetails
                    {
                        Permission = perdata,
                        isSelect = check != null
                    });
                }
                listPaged.Add(perpage);
            }
            return listPaged;
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetControllerNames(int permissionId)
        {
            var listPaged = await GetAllControllerName(permissionId);
            return Ok(new ResponseAPI
            {
                Data = listPaged,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetControllerNamesPaging(int permissionId, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null)
                pageSize = PagingConfig.PageSize;

            var listPaged = await GetAllControllerName(permissionId);
            var models = await listPaged
            .OrderByDescending(x => x.PageName)
            .ToPagedListAsync(page.Value, pageSize.Value);

            return Ok(new ResponseAPIPaging
            {
                Data = models,
                isSuccess = true,
                PageCount = models.PageCount,
                PageSize = models.PageSize,
                PageNumber = models.PageNumber,
                StatusCode = Ok().StatusCode,
                TotalItems = models.TotalItemCount
            });

        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> SearchControllerNames(int permissionId, string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null)
                pageSize = PagingConfig.PageSize;
            var listQuery = q.Split(' ');
            var listPaged = await GetAllControllerName(permissionId);
            var models = await listPaged
             .Where(x => listQuery.FirstOrDefault(y => y.Trim().Equals(x.PageName)) != null)
            .OrderByDescending(x => x.PageName)
            .ToPagedListAsync(page.Value, pageSize.Value);

            return Ok(new ResponseAPIPaging
            {
                Data = models,
                isSuccess = true,
                PageCount = models.PageCount,
                PageSize = models.PageSize,
                PageNumber = models.PageNumber,
                StatusCode = Ok().StatusCode,
                TotalItems = models.TotalItemCount
            });

        }

        [HttpGet("[Action]/{id}")]
        [Authorize(Roles = Permissions.PermissionDetails.View)]
        public async Task<IActionResult> GetPermissionDetails(int id)
        {
            var model = await services.GetPermissionDetailsAsync(id);
            return Ok(new ResponseAPI
            {
                Data = model,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.PermissionDetails.View)]
        public async Task<IActionResult> GetPermissionDetailsByNameAndPermissionId(string permissionValue, int permissionId)
        {
            var model = await services
                .GetPermissionDetails()
                .Where(x => x.PermissionValue.Equals(permissionValue) && x.PermissionId == permissionId)
                .FirstOrDefaultAsync();

            return Ok(new ResponseAPI
            {
                Data = model,
                isSuccess = true,
                StatusCode = Ok().StatusCode
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.PermissionDetails.Create)]
        public async Task<IActionResult> InsertPermissionDetails(PermissionDetailsModel model)
        {
            if (model == null)
                return BadRequest();
            var result = await services.InsertPermissionDetailsAsync(model);
            return Ok(new ResponseAPI
            {
                Data = model,
                isSuccess = result,
                Messages = result ? new string[] { "Thêm thành công" } : new string[] { "Thêm thất bại" },
                StatusCode = result ? Ok().StatusCode : BadRequest().StatusCode
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.PermissionDetails.Delete)]
        public async Task<IActionResult> DeletePermissionDetails(int id)
        {
            var result = await services.DeletePermissionDetailsAsync(id);
            return Ok(new ResponseAPI
            {
                isSuccess = result,
                Messages = result ? new string[] { "Xóa thành công" } : new string[] { "Xóa thất bại" },
                StatusCode = result ? Ok().StatusCode : BadRequest().StatusCode
            });
        }

    }
}