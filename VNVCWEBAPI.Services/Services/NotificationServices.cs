using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Library;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly IRepository<Notification> repository;
        private readonly ILoginSessionServices loginSessionServices;
        public NotificationServices(IRepository<Notification> repository, ILoginSessionServices loginSessionServices)
        {
            this.repository = repository;
            this.loginSessionServices = loginSessionServices;
        }

        public async Task<bool> DeleteNotification(int notificationId)
        {
            var notification = await repository.GetAsync(notificationId);
            return await repository.DeleteFromTrash(notification);
        }

        public async Task<bool> DeleteNotificationFromLoginId(int loginId)
        {
            var notifications = repository.GetAll(Common.Enum.SelectEnum.Select.ALL).Where(x => x.LoginId == loginId).ToList();
            return await repository.DeleteRange(notifications);
        }

        public async Task<NotificationModel> GetNotification(int notificationId)
        {
            var notification = await repository.GetAsync(notificationId);
            return new NotificationModel
            {
                Content = notification.Content,
                Created = notification.Created,
                Id = notification.Id,
                Image = notification.Image,
                isSeen = notification.isSeen,
                LoginId = notification.LoginId,
                Title = notification.Title
            };
        }

        public IQueryable<NotificationModel> GetNotifications()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Select(notification => new NotificationModel
                {
                    Content = notification.Content,
                    Created = notification.Created,
                    Id = notification.Id,
                    Image = notification.Image,
                    isSeen = notification.isSeen,
                    LoginId = notification.LoginId,
                    Title = notification.Title
                });
        }

        public IQueryable<NotificationModel> GetNotifications(int loginId)
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.LoginId == loginId)
                .Select(notification => new NotificationModel
                {
                    Content = notification.Content,
                    Created = notification.Created,
                    Id = notification.Id,
                    Image = notification.Image,
                    isSeen = notification.isSeen,
                    LoginId = notification.LoginId,
                    Title = notification.Title
                });
        }

        public async Task<bool> InsertNotification(NotificationModel notificationModel)
        {
            var notification = new Notification
            {
                Created = notificationModel.Created,
                LoginId = notificationModel.LoginId,
                Content = notificationModel.Content,
                Image = notificationModel.Image,
                isSeen = notificationModel.isSeen,
                isTrash = false,
                Title = notificationModel.Title
            };
            var result = await repository.InsertAsync(notification);
            notificationModel.Id = notification.Id;
            notificationModel.Created = notification.Created;
            return result;
        }

        public async Task<bool> PushNotification(string deviceId, string title, string body, bool isInsert = false, string image = "")
        {
            try
            {
                var firbase = new Firebase();
                var res = await firbase.PushNotification(deviceId, title, body, "");
                if (isInsert)
                {
                    if (deviceId.Contains("topics"))
                    {
                        var arr = deviceId.Split("-");
                        if (arr.Length > 0)
                        {
                            int loginId = int.Parse(Regex.Match(deviceId, @"\d+").Value);
                            await this.InsertNotification(new NotificationModel
                            {
                                Content = body,
                                Created = DateTime.Now,
                                isSeen = false,
                                Title = title,
                                LoginId = loginId,
                            });
                        }
                        else
                        {
                            await this.InsertNotification(new NotificationModel
                            {
                                Content = body,
                                Created = DateTime.Now,
                                isSeen = false,
                                Title = title,
                            });
                        }
                        return true;
                    }
                    else
                    {
                        var loginSession = loginSessionServices.GetLoginSessions().Where(x => x.DeviceId.Equals(deviceId)).FirstOrDefault();
                        if (loginSession != null)
                        {
                            await this.InsertNotification(new NotificationModel
                            {
                                Content = body,
                                Created = DateTime.Now,
                                isSeen = false,
                                Title = title,
                                LoginId = loginSession.LoginId,
                            });
                            return true;
                        }
                        return false;
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateNotification(NotificationModel notificationModel)
        {
            var notification = await repository.GetAsync(notificationModel.Id);
            if (notification == null)
                return false;
            notificationModel.Content = notification.Content;
            notification.isSeen = notification.isSeen;
            notification.Title = notificationModel.Title;
            notification.Image = notificationModel.Image;
            notification.LoginId = notificationModel.LoginId;
            return await repository.UpdateAsync(notification);
        }
    }
}
