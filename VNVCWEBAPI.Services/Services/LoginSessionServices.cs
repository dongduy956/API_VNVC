using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class LoginSessionServices : ILoginSessionServices
    {
        private readonly IRepository<LoginSession> repository;
        public LoginSessionServices(IRepository<LoginSession> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteLoginSession(int id)
        {
            var loginSession = await repository.GetAsync(id);
            if (loginSession == null)
                return false;
            return await repository.DeleteFromTrash(loginSession);
        }

        public async Task<bool> DeleteLoginSession(JWTRequest jWTRequest)
        {
            var loginSession = await repository
                .Where(x => x.AccessToken.Equals(jWTRequest.AccessToken)
                && x.RefreshToken.Equals(jWTRequest.RefreshToken))
                .FirstOrDefaultAsync();
            return await repository.DeleteFromTrash(loginSession);
        }

        public async Task<bool> DeleteLoginSessionsRange(int[] ids)
        {
            var loginSessions = new List<LoginSession>();
            foreach (var id in ids)
            {
                var loginSession = await repository.GetAsync(id);
                if (loginSession != null)
                {
                    loginSessions.Add(loginSession);
                }
            }
            return await repository.DeleteRange(loginSessions);
        }

        public async Task<LoginSessionModel> GetLoginSession(int id)
        {
            var loginSession = await repository.GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(ct => ct.Created)
                .Include(st => st.Staff)
                .FirstOrDefaultAsync(x => x.Id == id);
            return new LoginSessionModel
            {
                Id = loginSession.Id,
                LoginId=loginSession.LoginId,
                AccessToken = loginSession.AccessToken,
                CustomerId = loginSession.CustomerId,
                Expired = loginSession.Expired,
                IPAddress = loginSession.IPAddress,
                isRevoked = loginSession.isRevoked,
                RefreshToken = loginSession.RefreshToken,
                StaffId = loginSession.StaffId,
                TokenId = loginSession.TokenId,
                StaffName = loginSession.Staff.StaffName,
                CustomerName = loginSession.Customer.FirstName + ' ' + loginSession.Customer.LastName,
                Created = loginSession.Created,
                DeviceId=loginSession.DeviceId,
            };
        }

        public async Task<LoginSessionModel> GetLoginSession(JWTRequest jWTRequest)
        {
            var loginSessionModel = await repository
                .Where(x => x.AccessToken.Equals(jWTRequest.AccessToken)
                && x.RefreshToken.Equals(jWTRequest.RefreshToken))
                .Include(ct => ct.Created)
                .Include(st => st.Staff)
                .Select(loginSession => new LoginSessionModel
                {
                    Id = loginSession.Id,
                    AccessToken = loginSession.AccessToken,
                    LoginId = loginSession.LoginId,
                    CustomerId = loginSession.CustomerId,
                    Expired = loginSession.Expired,
                    IPAddress = loginSession.IPAddress,
                    isRevoked = loginSession.isRevoked,
                    RefreshToken = loginSession.RefreshToken,
                    StaffId = loginSession.StaffId,
                    TokenId = loginSession.TokenId,
                    StaffName = loginSession.Staff.StaffName,
                    CustomerName = loginSession.Customer.FirstName + ' ' + loginSession.Customer.LastName,
                    Created = loginSession.Created,
                    DeviceId=loginSession.DeviceId
                })
                .FirstOrDefaultAsync();

            return loginSessionModel;
        }

        public IQueryable<LoginSessionModel> GetLoginSessions()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(st => st.Staff)
                .Include(c => c.Customer)
                .Select(loginSession => new LoginSessionModel
                {
                    Id = loginSession.Id,
                    AccessToken = loginSession.AccessToken,
                    CustomerId = loginSession.CustomerId,
                    LoginId = loginSession.LoginId,
                    Expired = loginSession.Expired,
                    IPAddress = loginSession.IPAddress,
                    isRevoked = loginSession.isRevoked,
                    RefreshToken = loginSession.RefreshToken,
                    StaffId = loginSession.StaffId,
                    TokenId = loginSession.TokenId,
                    StaffName = loginSession.Staff.StaffName,
                    CustomerName = loginSession.Customer.FirstName + ' ' + loginSession.Customer.LastName,
                    Created = loginSession.Created,
                    DeviceId=loginSession.DeviceId
                });
        }

        public async Task<bool> InsertLoginSession(LoginSessionModel loginSessionModel)
        {
            var loginSession = new LoginSession
            {
                AccessToken = loginSessionModel.AccessToken,
                CustomerId = loginSessionModel.CustomerId,
                LoginId = loginSessionModel.LoginId,
                Expired = loginSessionModel.Expired,
                IPAddress = loginSessionModel.IPAddress,
                RefreshToken = loginSessionModel.RefreshToken,
                StaffId = loginSessionModel.StaffId,
                TokenId = loginSessionModel.TokenId,
                DeviceId=loginSessionModel.DeviceId

            };
            var result = await repository.InsertAsync(loginSession);
            loginSessionModel.Id = loginSession.Id;
            loginSessionModel.Created = loginSession.Created;
            return result;
        }

        public IQueryable<LoginSessionModel> SearchLoginSessions(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(ct => ct.Customer)
                .Include(st => st.Staff)
                .Where(x => x.AccessToken.ToLower().Contains(q) ||
                (x.Customer.FirstName + ' ' + x.Customer.LastName).ToLower().Contains(q) ||
                x.Expired.ToString().Contains(q) ||
                x.IPAddress.Equals(q) ||
                (x.isRevoked ? "đã thu hồi" : "chưa thu hồi").Contains(q) ||
                x.RefreshToken.ToLower().Contains(q) ||
                x.Staff.StaffName.ToLower().Contains(q) ||
                x.TokenId.ToLower().Equals(q))
                .Select(loginSession => new LoginSessionModel
                {
                    Id = loginSession.Id,
                    AccessToken = loginSession.AccessToken,
                    LoginId = loginSession.LoginId,
                    CustomerId = loginSession.CustomerId,
                    Expired = loginSession.Expired,
                    IPAddress = loginSession.IPAddress,
                    isRevoked = loginSession.isRevoked,
                    RefreshToken = loginSession.RefreshToken,
                    StaffId = loginSession.StaffId,
                    TokenId = loginSession.TokenId,
                    StaffName = loginSession.Staff.StaffName,
                    CustomerName = loginSession.Customer.FirstName + ' ' + loginSession.Customer.LastName,
                    Created = loginSession.Created,
                    DeviceId=loginSession.DeviceId
                });
            return results;
        }

        public async Task<bool> UpdateLoginSession(int id)
        {
            var loginSession = await repository.GetAsync(id);
            if (loginSession == null)
                return false;
            loginSession.isRevoked = true;
            return await repository.UpdateAsync(loginSession);
        }

        public IQueryable<LoginSessionModel> Where(Expression<Func<LoginSession, bool>> expression)
        {
            return repository.Where(expression).Select(x => new LoginSessionModel
            {
                AccessToken = x.AccessToken,
                CustomerId = x.CustomerId,
                Expired = x.Expired,
                LoginId = x.LoginId,
                IPAddress = x.IPAddress,
                RefreshToken = x.RefreshToken,
                StaffId = x.StaffId,
                TokenId = x.TokenId,
                Id = x.Id,
                Created = x.Created,
                isRevoked = x.isRevoked,
                DeviceId=x.DeviceId
            });
        }

        public LoginSession? GetLoginSessionByJWT(JWTRequest jWTRequest)
        {
            return repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                  .FirstOrDefault(x =>
                   x.AccessToken.Equals(jWTRequest.AccessToken)
                   && x.RefreshToken.Equals(jWTRequest.RefreshToken)
                   && DateTime.UtcNow < x.Expired && x.isRevoked == false);
        }
    }
}
