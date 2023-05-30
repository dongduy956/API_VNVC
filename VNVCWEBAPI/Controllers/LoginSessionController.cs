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
    public class LoginSessionController : ControllerBase
    {
        private readonly ILoginSessionServices loginSessionServices;
        public LoginSessionController(ILoginSessionServices loginSessionServices)
        {
            this.loginSessionServices = loginSessionServices;
        }

        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.LoginSession.View)]
        public IActionResult GetLoginSessions(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var loginSessions = loginSessionServices.GetLoginSessions()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = loginSessions.PageCount,
                PageNumber = loginSessions.PageNumber,
                TotalItems = loginSessions.TotalItemCount,
                Data = loginSessions,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.LoginSession.View)]
        public IActionResult SearchLoginSessions(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var loginSessions = loginSessionServices.SearchLoginSessions(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = loginSessions.PageCount,
                PageNumber = loginSessions.PageNumber,
                TotalItems = loginSessions.TotalItemCount,
                Data = loginSessions,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.LoginSession.Delete)]
        public async Task<IActionResult> DeleteLoginSession(int id)
        {
            var result = await loginSessionServices.DeleteLoginSession(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá phiên đăng nhập thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá phiên đăng nhập." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.LoginSession.Create)]
        public async Task<IActionResult> InsertLoginSession(LoginSessionModel loginSessionModel)
        {
            var result = await loginSessionServices.InsertLoginSession(loginSessionModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = loginSessionModel,
                    Messages = new string[] { "Thêm phiên đăng nhập thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm phiên đăng nhập thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.LoginSession.Edit)]
        public async Task<IActionResult> UpdateLoginSession(int id)
        {
            var result = await loginSessionServices.UpdateLoginSession(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật trạng thái thu hồi thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật trạng thái thu hồi thành công." }
            });
        }
    }
}
