using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
    public class LoginController : BaseController
    {
        private readonly ILoginServices loginServices;
        public LoginController(ILoginServices loginServices)
        {
            this.loginServices = loginServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Login.View)]
        public IActionResult GetAllLogins()
        {
            var logins = loginServices.GetLogins()
                .OrderBy(x => x.Id);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = logins,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Login.View)]
        public IActionResult GetLogins(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var logins = loginServices.GetLogins()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = logins.PageCount,
                PageNumber = logins.PageNumber,
                TotalItems = logins.TotalItemCount,
                Data = logins,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Login.View)]
        public IActionResult SearchLogins(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var logins = loginServices.SearchLogins(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = logins.PageCount,
                PageNumber = logins.PageNumber,
                TotalItems = logins.TotalItemCount,
                Data = logins,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Login.Delete)]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var result = await loginServices.DeleteLogin(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá tài khoản thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá tài khoản." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Login.Create)]
        public async Task<IActionResult> InsertLogin(LoginModel loginModel)
        {
            var result = await loginServices.InsertLogin(loginModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = loginModel,
                    Messages = new string[] { "Thêm tài khoản thành công.Mật khẩu: " + LoginConfig.DefaultPassword }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm tài khoản thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Login.Edit)]
        public async Task<IActionResult> UpdateLoginLock(int id)
        {
            var result = await loginServices.UpdateLoginLock(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật trạng thái thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật trạng thái thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Login.Edit)]
        public async Task<IActionResult> UpdateLoginValidate(int id)
        {
            var result = await loginServices.UpdateLoginValidate(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Cập nhật xác thực thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Cập nhật xác thực thất bại." }
            });
        }
        [Authorize(Roles = Permissions.Login.Edit)]
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> ResetPassword(int id)
        {
            var result = await loginServices.ResetPassword(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Reset password thành công.Mật khẩu: " + LoginConfig.DefaultPassword }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi reset password." }
            });
        }
        [HttpPut("[Action]/{id}")]


        public async Task<IActionResult> ChangePassword(int id, ChangePasswordModel1 changePass)
        {
            //-1:sai pass
            //0: xác nhận mật khẩu không chính xác
            //1: oke
            //-2:có lỗi
            if (!isAccess(id, Permissions.Login.Edit))
                return Unauthorized();
            var result = await loginServices.ChangePassword(id, changePass);
            if (result == -1)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                    Data = result,
                    Messages = new string[] { "Sai mật khẩu." }
                });
            else if (result == 0)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                    Data = result,
                    Messages = new string[] { "Xác nhận mật khẩu không chính xác." }
                });
            if (result == -2)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                    Data = result,
                    Messages = new string[] { "Có lỗi xảy ra khi đổi mật khẩu." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                Data = result,
                isSuccess = true,
                Messages = new string[] { "Đổi mật khẩu thành công." }
            });
        }
    }
}
