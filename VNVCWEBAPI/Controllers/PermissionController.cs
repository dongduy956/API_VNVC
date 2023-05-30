using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionServices permissionServices;
        public PermissionController(IPermissionServices permissionServices)
        {
            this.permissionServices = permissionServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Permission.View)]
        public IActionResult GetPermissions(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var permissions = permissionServices.GetPermissions()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = permissions.PageCount,
                PageNumber = permissions.PageNumber,
                TotalItems = permissions.TotalItemCount,
                Data = permissions,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Permission.View)]
        public IActionResult GetAllPermissions()
        {
            var permissions = permissionServices.GetPermissions()
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = permissions,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Permission.View)]
        public IActionResult SearchPermissions(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var permissions = permissionServices.SearchPermissions(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = permissions.PageCount,
                PageNumber = permissions.PageNumber,
                TotalItems = permissions.TotalItemCount,
                Data = permissions,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Permission.Delete)]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var result = await permissionServices.DeletePermission(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá chức vụ thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá chức vụ." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Permission.Create)]
        public async Task<IActionResult> InsertPermission(PermissionModel permissionModel)
        {
            var result = await permissionServices.InsertPermission(permissionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = permissionModel,
                    Messages = new string[] { "Thêm chức vụ thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chức vụ thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Permission.Create)]
        public async Task<IActionResult> InsertPermissionsRange(IList<PermissionModel> permissionModels)
        {
            var result = await permissionServices.InsertPermissionsRange(permissionModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = permissionModels,
                    Messages = new string[] { "Thêm chức vụ thành công." }
                });
            return BadRequest(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm chức vụ thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Permission.Edit)]
        public async Task<IActionResult> UpdatePermission(int id, PermissionModel permissionModel)
        {
            var result = await permissionServices.UpdatePermission(id, permissionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa chức vụ thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa chức vụ thất bại." }
            });
        }
    }
}
