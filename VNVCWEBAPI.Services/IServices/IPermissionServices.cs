using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IPermissionServices
    {
        IQueryable<PermissionModel> GetPermissions();
        IQueryable<PermissionModel> SearchPermissions(string q="");
        Task<PermissionModel> GetPermission(int id);
        Task<bool> InsertPermission(PermissionModel permissionModel);
        Task<bool> InsertPermissionsRange(IList<PermissionModel> permissionModels);
        Task<bool> UpdatePermission(int id, PermissionModel permissionModel);
        Task<bool> DeletePermission(int id);
    }
}
