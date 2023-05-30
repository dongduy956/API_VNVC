using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ILoginServices
    {
        IQueryable<LoginModel> GetLogins();
        IQueryable<LoginModel> SearchLogins(string q="");
        Task<int> ChangePassword(int id,ChangePasswordModel changePass);
        Task<int> ChangePassword(int id, ChangePasswordModel1 changePass);
        Task<bool> ResetPassword(int id);
        Task<LoginModel> GetLogin(int id);
        Task<LoginModel> GetLogin(string username);
        Task<LoginModel> GetLogin(LoginRequest loginRequest);
        Task<bool> CheckExitsUserbyUsername(string username);
        Task<bool> InsertLogin(LoginModel LoginModel);
        Task<bool> UpdateLoginLock(int id);
        Task<bool> UpdateLoginValidate(int id);
        Task<bool> UpdateLoginValidate(string username);
        Task<string> RenewVerifyCode(int id);
        Task<string> RenewVerifyCode(string username);
        Task<bool> DeleteLogin(int id);
        Task<bool> UpdateLogin(LoginModel model);
        Task<bool> DeleteLoginsRange(int[] ids);
    }
}
