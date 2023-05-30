using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ILoginSessionServices
    {
        IQueryable<LoginSessionModel> GetLoginSessions();
        LoginSession? GetLoginSessionByJWT(JWTRequest jWTRequest);
        IQueryable<LoginSessionModel> SearchLoginSessions(string q="");
        Task<LoginSessionModel> GetLoginSession(int id);
        Task<LoginSessionModel> GetLoginSession(JWTRequest jWTRequest);
        Task<bool> InsertLoginSession(LoginSessionModel loginSessionModel);        
        Task<bool> DeleteLoginSession(int id);
        Task<bool> DeleteLoginSession(JWTRequest jWTRequest);
        Task<bool> UpdateLoginSession(int id);
        IQueryable<LoginSessionModel> Where(Expression<Func<LoginSession, bool>> expression);

    }
}
