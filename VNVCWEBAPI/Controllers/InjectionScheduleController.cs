using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Library;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;
namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InjectionScheduleController : BaseController
    {
        private readonly IInjectionScheduleServices injectionScheduleServices;
        private readonly ILoginServices loginServices;
        private readonly INotificationServices notificationServices;
        public InjectionScheduleController(IInjectionScheduleServices injectionScheduleServices
            , ILoginServices loginServices,
            INotificationServices notificationServices)
        {
            this.injectionScheduleServices = injectionScheduleServices;
            this.loginServices = loginServices;
            this.notificationServices = notificationServices;
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.InjectionSchedule.View)]
        public IActionResult GetAllInjectionSchedules()
        {
            var injectionSchedules = injectionScheduleServices.GetInjectionSchedules()
                    .OrderBy(x => x.Id);
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                Data = injectionSchedules,
            });
        }
        [HttpGet("[Action]")]
        // [Authorize(Roles = Permissions.InjectionSchedule.View)]
        public async Task<IActionResult> GetInjectionSchedules(int? customerId, int? page, int? pageSize)
        {
            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            if (customerId.HasValue)
            {
                var StringUserId = User.Claims
                .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
                var loginUser = await loginServices.GetLogin(int.Parse(StringUserId));
                if (!(loginUser.CustomerId == customerId || isAccess(Permissions.InjectionSchedule.View)))
                {
                    return Forbid();
                }
                var injectionSchedules = injectionScheduleServices.GetInjectionSchedules()
                    .Where(x => x.CustomerId == customerId.Value)
               .OrderByDescending(x => x.Id)
               .ToPagedList(page.Value, pageSize.Value);
                return Ok(new ResponseAPIPaging
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    PageSize = pageSize.Value,
                    PageCount = injectionSchedules.PageCount,
                    PageNumber = injectionSchedules.PageNumber,
                    TotalItems = injectionSchedules.TotalItemCount,
                    Data = injectionSchedules,
                });
            }
            else
            {
                if (!isAccess(Permissions.InjectionSchedule.View))
                {
                    return Forbid();
                }
                var injectionSchedules = injectionScheduleServices.GetInjectionSchedules()
                    .OrderByDescending(x => x.Id)
                    .ToPagedList(page.Value, pageSize.Value);
                return Ok(new ResponseAPIPaging
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    PageSize = pageSize.Value,
                    PageCount = injectionSchedules.PageCount,
                    PageNumber = injectionSchedules.PageNumber,
                    TotalItems = injectionSchedules.TotalItemCount,
                    Data = injectionSchedules,
                });
            }
        }
        //[Authorize(Roles = Permissions.InjectionSchedule.View)]
        [HttpGet("[Action]/{id}")]
        public IActionResult GetInjectionSchedule(int id)
        {
            if (!isAccess(id, Permissions.InjectionSchedule.View))
            {
                return Unauthorized();
            }
            var injectionSchedule = injectionScheduleServices.GetInjectionScheduleAsync(id);
            if (injectionSchedule == null)
                return Ok(new ResponseAPI
                {
                    StatusCode = BadRequest().StatusCode,
                    isSuccess = false,
                });
            return Ok(new ResponseAPI
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,

                Data = injectionSchedule,
            });
        }
        [HttpGet("[Action]")]
        [Authorize(Roles = Permissions.InjectionSchedule.View)]

        public IActionResult SearchInjectionSchedules(string q, int? page, int? pageSize)
        {

            if (page == null)
                page = 1;
            if (pageSize == null) pageSize = PagingConfig.PageSize;
            var injectionSchedules = injectionScheduleServices.SearchInjectionSchedules(q)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                StatusCode = Ok().StatusCode,
                isSuccess = true,
                PageSize = pageSize.Value,
                PageCount = injectionSchedules.PageCount,
                PageNumber = injectionSchedules.PageNumber,
                TotalItems = injectionSchedules.TotalItemCount,
                Data = injectionSchedules,
            });
        }
        [Authorize(Roles = Permissions.InjectionSchedule.Delete)]
        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> DeleteInjectionSchedule(int id)
        {
            var result = await injectionScheduleServices.DeleteInjectionScheduleAsync(id);
            if (result)
                return Ok(new ResponseAPI
                {
                    Data = result,
                    isSuccess = true,
                    Messages = new string[] { "Xoá lịch tiêm thành công." },
                    StatusCode = Ok().StatusCode
                });
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Có lỗi xảy ra khi xoá lịch tiêm." }
            });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> InsertInjectionSchedule(int? loginId, InjectionScheduleModel? injectionScheduleModel)
        {
            if (!isAccess(loginId ?? 0, Permissions.InjectionSchedule.Create))
            {
                return Unauthorized();
            }
            var result = await injectionScheduleServices.InsertInjectionScheduleAsync(injectionScheduleModel);
            if (result)
            {
                //Send notification
                try
                {
                    await notificationServices.PushNotification($"/topics/device-{loginId}", "Đặt đơn hàng thành công", $"Bạn vừa đặt hàng thành công và lúc {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}, mã đơn hàng của bạn là #{injectionScheduleModel.Id}", true);

                }
                catch (Exception ex) { }
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = injectionScheduleModel,
                    Messages = new string[] { "Thêm lịch tiêm thành công." }
                });
            }
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Thêm lịch tiêm thất bại." }
            });
        }
        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> UpdateInjectionSchedule(int id, InjectionScheduleModel injectionScheduleModel)
        {
            var result = await injectionScheduleServices.UpdateInjectionScheduleAsync(id, injectionScheduleModel);
            if (result)
            {
                //Send notification
                try
                {
                    var loginId = loginServices.GetLogins().FirstOrDefault(x => x.CustomerId == injectionScheduleModel.CustomerId);
                    if (loginId != null)
                    {
                        await notificationServices.PushNotification($"/topics/device-{loginId.Id}", "Cập nhật lịch tiêm", $"Lịch tiêm của bạn vừa cập nhật vào lúc {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}, vui lòng kiểm tra lại lịch tiêm", true);
                    }
                }
                catch (Exception ex) { }
                return Ok(new ResponseAPI
                {
                    StatusCode = Ok().StatusCode,
                    isSuccess = true,
                    Data = result,
                    Messages = new string[] { "Sửa lịch tiêm thành công." }
                });
            }
            return Ok(new ResponseAPI
            {
                StatusCode = BadRequest().StatusCode,
                Data = result,
                isSuccess = false,
                Messages = new string[] { "Sửa lịch tiêm thất bại." }
            });
        }
    }
}
