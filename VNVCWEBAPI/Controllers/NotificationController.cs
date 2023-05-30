using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Library;
using VNVCWEBAPI.Common.Models;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using X.PagedList;

namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : BaseController
    {
        private readonly INotificationServices notificationServices;
        public NotificationController(INotificationServices notificationServices)
        {
            this.notificationServices = notificationServices;
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> getNotifications(int? page, int? pageSize, int loginId)
        {
            if (!isAccess(loginId, Permissions.Notification.View))
            {
                return Forbid();
            }
            if (page == null)
                page = 1;
            if (pageSize == null)
                pageSize = PagingConfig.PageSize;
            var notifications = notificationServices.GetNotifications()
                .Where(x => x.LoginId == loginId || x.LoginId == null)
                .OrderByDescending(x => x.Id);
            var notificationsPage = await notifications.ToPagedListAsync(page.Value, pageSize.Value);
            return Ok(new ResponseAPIPaging
            {
                Data = notificationsPage,
                isSuccess = true,
                Messages = new string[] { "Get Success" },
                PageCount = notificationsPage.PageCount,
                PageNumber = notificationsPage.PageNumber,
                PageSize = notificationsPage.PageSize,
                StatusCode = Ok().StatusCode,
                TotalItems = notificationsPage.TotalItemCount
            });

        }

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> getNotification(int id)
        {
            var notification = await notificationServices.GetNotification(id);
            if (notification == null)
                return NotFound();
            if (!isAccess(notification.LoginId.Value, Permissions.Notification.View))
            {
                return Forbid();
            }
            return Ok(new ResponseAPI
            {
                Data = notification,
                isSuccess = true,
                StatusCode = Ok().StatusCode,
            });
        }

        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> deleteNotification(int id)
        {
            var notification = await notificationServices.GetNotification(id);
            if (notification == null)
                return NotFound();
            if (!isAccess(notification.LoginId.Value, Permissions.Notification.View))
            {
                return Forbid();
            }
            var result = await notificationServices.DeleteNotification(id);
            return Ok(new ResponseAPI
            {
                Data = result,
                isSuccess = result,
                StatusCode = result ? Ok().StatusCode : (int)HttpStatusCode.NotModified,
            });
        }

        [HttpPost("[Action]/{id}")]
        public async Task<IActionResult> insertNotification(NotificationModel model)
        {

            if (!isAccess(model.LoginId.Value, Permissions.Notification.View))
            {
                return Forbid();
            }
            var result = await notificationServices.InsertNotification(model);
            return Ok(new ResponseAPI
            {
                Data = model,
                isSuccess = result,
                StatusCode = result ? Ok().StatusCode : (int)HttpStatusCode.NotModified,
            });
        }

        [HttpPut("[Action]")]
        public async Task<IActionResult> updateSeenNotification(NotificationModel model)
        {

            if (!isAccess(model.LoginId.Value, Permissions.Notification.View))
            {
                return Forbid();
            }
            model.isSeen = true;
            var result = await notificationServices.UpdateNotification(model);
            return Ok(new ResponseAPI
            {
                Data = model,
                isSuccess = result,
                StatusCode = result ? Ok().StatusCode : (int)HttpStatusCode.NotModified,
            });
        }

        [HttpPost("[Action]"), Authorize(Roles = Permissions.Notification.Edit)]
        public async Task<IActionResult> pushNotification(string deviceId, string title, string body, bool isInsert = false)
        {
            var res = await notificationServices.PushNotification(deviceId, title, body, isInsert);
            return Ok(new ResponseAPI
            {
                Data = res,
                StatusCode = (int)HttpStatusCode.OK,
                isSuccess = true
            });
        }
    }
}
