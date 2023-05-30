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
    public class StaffController : BaseController
    {
        private readonly IStaffServices staffServices;
        private readonly ILoginServices loginServices;
        public StaffController(IStaffServices staffServices, ILoginServices loginServices)
        {
            this.staffServices = staffServices;
            this.loginServices = loginServices;

        }
        [HttpGet("[Action]/{id}")]
        //[Authorize(Roles = Permissions.Staff.View)]
        public async Task<IActionResult> GetStaff(int id)
        {
            var login = loginServices.GetLogins().FirstOrDefault(x => x.StaffId == id);
            if (!isAccess(login == null ? 0 : login.Id, Permissions.Staff.View))
                return Forbid();

            var staff = await staffServices.GetStaff(id);
            if (staff != null)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = staff,
                });
            return Ok(new ResponseAPI
            {
                isSuccess = false,
                StatusCode = BadRequest().StatusCode
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Staff.View)]
        public IActionResult GetAllStaffs()
        {
            var staffs = staffServices.GetStaffs()
               .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = staffs,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Staff.View)]
        public IActionResult GetStaffs(int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var staffs = staffServices.GetStaffs()
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = staffs.PageCount,
                PageNumber = staffs.PageNumber,
                TotalItems = staffs.TotalItemCount,
                Data = staffs,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.Staff.View)]
        public IActionResult SearchStaffs(string q, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var staffs = staffServices.SearchStaffs(q)
                .OrderBy(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = staffs.PageCount,
                PageNumber = staffs.PageNumber,
                TotalItems = staffs.TotalItemCount,
                Data = staffs,
            });
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = Permissions.Staff.Delete)]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await staffServices.DeleteStaff(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá nhân viên thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá nhân viên." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Staff.Create)]
        public async Task<IActionResult> InsertStaff(StaffModel staffModel)
        {
            var result = await staffServices.InsertStaff(staffModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = staffModel,
                    Messages = new string[] { "Thêm nhân viên thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm nhân viên thất bại." }
            });
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = Permissions.Staff.Create)]
        public async Task<IActionResult> InsertStaffsRange(IList<StaffModel> staffModels)
        {
            var result = await staffServices.InsertStaffsRange(staffModels);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = staffModels,
                    Messages = new string[] { "Thêm nhân viên thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm nhân viên thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = Permissions.Staff.Edit)]
        public async Task<IActionResult> UpdateStaff(int id, StaffModel staffModel)
        {
            var result = await staffServices.UpdateStaff(id, staffModel);
            if (result)
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa nhân viên thành công." }
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa nhân viên thất bại." }
            });
        }
    }
}
