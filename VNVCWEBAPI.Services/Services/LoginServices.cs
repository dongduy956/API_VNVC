using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.Common.Library;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IRepository<Login> repository;
        public LoginServices(IRepository<Login> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteLogin(int id)
        {
            var login = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(login);
        }

        public async Task<bool> DeleteLoginsRange(int[] ids)
        {
            var logins = new List<Login>();
            foreach (var id in ids)
            {
                var login = await repository.GetAsync(id);
                if (login != null)
                {
                    logins.Add(login);
                }
            }
            return await repository.DeleteFromTrashRange(logins);
        }

        public async Task<LoginModel> GetLogin(int id)
        {
            var model = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(ct => ct.Customer)
                    .ThenInclude(x => x.CustomerType)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => ((x.Customer != null && x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false) || (x.Staff != null && x.Staff.isTrash == false && x.Staff.Permission.isTrash == false)) && x.Id == id);

            var loginModel = new LoginModel
            {
                Id = model.Id,
                StaffId = model.StaffId,
                StaffName = model.Staff == null ? null : model.Staff.StaffName.IsNullOrEmpty() ? null : model.Staff.StaffName,
                CustomerId = model.CustomerId,
                CustomerName = model.Customer?.FirstName + " " + model.Customer?.LastName,
                isLock = model.isLock,
                isValidate = model.isValidate,
                Username = model.Username,
                Created = model.Created,
                Code = model.Code
            };
            if (model.Staff == null)
            {
                if (model.Customer.Avatar.IsNullOrEmpty())
                {
                    loginModel.Avatar = null;
                }
                else
                {
                    loginModel.Avatar = model.Customer.Avatar;
                }
            }
            else
            {
                if (model.Staff.Avatar.IsNullOrEmpty())
                {
                    loginModel.Avatar = null;
                }
                else
                {
                    loginModel.Avatar = model.Staff.Avatar;
                }
            }
            return loginModel;
        }

        public async Task<LoginModel> GetLogin(string username)
        {
            var model = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(ct => ct.Customer)
                    .ThenInclude(x => x.CustomerType)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => ((x.Customer != null && x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false) || (x.Staff != null && x.Staff.isTrash == false && x.Staff.Permission.isTrash == false)) && x.Username == username);

            var loginModel = new LoginModel
            {
                Id = model.Id,
                StaffId = model.StaffId,
                StaffName = model.Staff == null ? null : model.Staff.StaffName.IsNullOrEmpty() ? null : model.Staff.StaffName,
                CustomerId = model.CustomerId,
                CustomerName = model.Customer?.FirstName + " " + model.Customer?.LastName,
                isLock = model.isLock,
                isValidate = model.isValidate,
                Username = model.Username,
                Created = model.Created,
                Code=model.Code
            };
            if (model.Staff == null)
            {
                if (model.Customer.Avatar.IsNullOrEmpty())
                {
                    loginModel.Avatar = null;
                }
                else
                {
                    loginModel.Avatar = model.Customer.Avatar;
                }
            }
            else
            {
                if (model.Staff.Avatar.IsNullOrEmpty())
                {
                    loginModel.Avatar = null;
                }
                else
                {
                    loginModel.Avatar = model.Staff.Avatar;
                }
            }
            return loginModel;
        }

        public async Task<LoginModel> GetLogin(LoginRequest loginRequest)
        {
            loginRequest.Password = StringLibrary.PasswordHash(loginRequest.Password);
            if (loginRequest.Username.StartsWith("KH"))
            {
                loginRequest.Username = loginRequest.Username.Substring(2);
                var model = await repository
                .Where(x => x.Username.Equals(loginRequest.Username)
                && x.PasswordHash.Equals(loginRequest.Password)
                && x.CustomerId != null)
                .Include(cm => cm.Customer)
                    .ThenInclude(x => x.CustomerType)
                .FirstOrDefaultAsync(x => x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false);

                if (model == null)
                    return null;

                return new LoginModel()
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    CustomerName = model.Customer == null ? null : (model.Customer.FirstName + " " + model.Customer.LastName),
                    isLock = model.isLock,
                    isValidate = model.isValidate,
                    Username = model.Username,
                    Created = model.Created,
                    Avatar = model.Customer?.Avatar,
                    Code = model.Code
                };
            }
            else
            {
                var model = await repository
                .Where(x => x.Username.Equals(loginRequest.Username)
                            && x.PasswordHash.Equals(loginRequest.Password)
                            && x.StaffId != null)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => x.Staff.isTrash == false && x.Staff.Permission.isTrash == false);

                if (model == null)
                    return null;

                return new LoginModel()
                {
                    Id = model.Id,
                    StaffId = model.StaffId,
                    StaffName = model.Staff == null ? null : model.Staff.StaffName,
                    isLock = model.isLock,
                    isValidate = model.isValidate,
                    Username = model.Username,
                    Created = model.Created,
                    PasswordHash = model.PasswordHash,
                    Avatar = model.Staff?.Avatar,
                    Code = model.Code
                };
            }

        }

        public IQueryable<LoginModel> GetLogins()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Include(ct => ct.Customer)
                        .ThenInclude(x => x.CustomerType)
                    .Include(st => st.Staff)
                        .ThenInclude(x => x.Permission)
                    .Where(x => (x.Customer != null && x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false) || (x.Staff != null && x.Staff.isTrash == false && x.Staff.Permission.isTrash == false))
                    .Select(model => new LoginModel
                    {
                        Id = model.Id,
                        StaffId = model.StaffId,
                        StaffName = model.Staff.StaffName,
                        CustomerId = model.CustomerId,
                        CustomerName = model.Customer.FirstName + " " + model.Customer.LastName,
                        isLock = model.isLock,
                        isValidate = model.isValidate,
                        Username = model.Username,
                        Created = model.Created
                    });
        }

        public async Task<bool> InsertLogin(LoginModel loginModel)
        {
            if (loginModel.PasswordHash.IsNullOrEmpty())
                loginModel.PasswordHash = StringLibrary.PasswordHash(LoginConfig.DefaultPassword);
            var login = new Login
            {
                Username = loginModel.Username,
                StaffId = loginModel.StaffId,
                CustomerId = loginModel.CustomerId,
                isLock = false,
                isValidate = false,
                PasswordHash = loginModel.PasswordHash,
                Code = loginModel.Code,
            };
            var result = await repository.InsertAsync(login);
            loginModel.Id = login.Id;
            loginModel.Created = login.Created;
            return result;
        }

        public async Task<int> ChangePassword(int id, ChangePasswordModel changePass)
        {
            //-1:sai pass
            //1: oke
            //0:có lỗi
            var login = await repository.Where(x => x.Id == id && x.PasswordHash.Equals(changePass.Password))
                                   .FirstOrDefaultAsync();
            if (login != null)
            {
                login.PasswordHash = changePass.NewPassword;
                var result = await repository.UpdateAsync(login);
                if (result)
                    return 1;
                return 0;
            }
            return -1;
        }
        public async Task<int> ChangePassword(int id, ChangePasswordModel1 changePass)
        {
            //-1:sai pass
            //0: xác nhận mật khẩu không chính xác
            //1: oke
            //-2:có lỗi            
            changePass.OldPass = StringLibrary.PasswordHash(changePass.OldPass);
            var login = repository.Where(x => x.Id == id && x.PasswordHash.Equals(changePass.OldPass))
                                   .FirstOrDefault();
            if (login != null)
            {
                if (changePass.NewPass.Equals(changePass.PrePass))
                {
                    login.PasswordHash = StringLibrary.PasswordHash(changePass.NewPass);
                    var result = await repository.UpdateAsync(login);
                    if (result)
                        return 1;
                    return -2;
                }
                return 0;
            }
            return -1;
        }

        public IQueryable<LoginModel> SearchLogins(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(ct => ct.Customer)
                    .ThenInclude(x => x.CustomerType)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .Where(x => (x.Customer != null && x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false) || (x.Staff != null && x.Staff.isTrash == false && x.Staff.Permission.isTrash == false))
                .Where(x =>
                (x.isLock ? "khoá" : "hoạt động").Contains(q) ||
                (x.isValidate ? "đã xác thực" : "chưa xác thực").Contains(q) ||
                x.Username.ToLower().Contains(q) ||
                x.Customer.FirstName.ToLower().Contains(q) ||
                x.Customer.LastName.ToLower().Contains(q) ||
                x.Staff.StaffName.ToLower().Contains(q))
                .Select(model => new LoginModel
                {
                    Id = model.Id,
                    StaffId = model.StaffId,
                    StaffName = model.Staff.StaffName,
                    CustomerId = model.CustomerId,
                    CustomerName = model.Customer.FirstName + " " + model.Customer.LastName,
                    isLock = model.isLock,
                    isValidate = model.isValidate,
                    Username = model.Username,
                    Created = model.Created
                });
            return results;

        }

        public async Task<bool> UpdateLoginValidate(int id)
        {
            var login = await repository.GetAsync(id);
            if (login == null)
                return false;
            login.isValidate = !login.isValidate;
            login.Code = null;
            return await repository.UpdateAsync(login);
        }
        public async Task<bool> UpdateLoginValidate(string username)
        {
            var login = await repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (login == null)
                return false;
            login.isValidate = !login.isValidate;
            login.Code = null;
            return await repository.UpdateAsync(login);
        }
        public async Task<bool> UpdateLoginLock(int id)
        {
            var login = await repository.GetAsync(id);
            if (login == null)
                return false;
            login.isLock = !login.isLock;
            return await repository.UpdateAsync(login);
        }

        public async Task<bool> ResetPassword(int id)
        {
            var login = await repository.GetAsync(id);
            if (login == null)
                return false;
            string password = StringLibrary.PasswordHash(LoginConfig.DefaultPassword);
            login.PasswordHash = password;
            return await repository.UpdateAsync(login);
        }

        public async Task<bool> CheckExitsUserbyUsername(string username)
        {
            var user = await repository.Where(x => x.Username.Equals(username)).FirstOrDefaultAsync();
            if (user != null)
                return true;
            return false;
        }

        public async Task<string> RenewVerifyCode(int id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
                return "-1";
            user.Code = StringLibrary.GenerateCode();
            var result = await repository.UpdateAsync(user);
            if (result)
                return "-1";
            return user.Code;
        }

        public async Task<bool> UpdateLogin(LoginModel model)
        {
            var login = new Login
            {
                PasswordHash = model.PasswordHash,
                Code = model.Code,
                Created = model.Created,
                CustomerId = model.CustomerId,
                Id = model.Id,
                isLock = model.isLock,
                isValidate = model.isValidate,
                StaffId = model.StaffId,
                Username = model.Username,
            };
            await repository.UpdateAsync(login);
            return true;

        }
        public async Task<string> RenewVerifyCode(string username)
        {
            var user = await repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH).FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (user == null)
                return "-1";
            user.Code = StringLibrary.GenerateCode();
            var result = await repository.UpdateAsync(user);
            if (!result)
                return "-1";
            return user.Code;
        }
    }
}
