using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface INotificationServices
    {
        Task<NotificationModel> GetNotification(int notificationId);
        IQueryable<NotificationModel> GetNotifications();
        IQueryable<NotificationModel> GetNotifications(int loginId);
        Task<bool> DeleteNotification(int notificationId);
        Task<bool> DeleteNotificationFromLoginId(int loginId);
        Task<bool> InsertNotification(NotificationModel notification);
        Task<bool> UpdateNotification(NotificationModel notification);
        Task<bool> PushNotification(string deviceId, string title, string body, bool isInsert = false, string image = "");
    }
}
