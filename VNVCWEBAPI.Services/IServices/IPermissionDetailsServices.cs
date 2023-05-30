using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Enum;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IPermissionDetailsServices
    {
        IQueryable<PermissionDetailsModel> GetPermissionDetails();
        Task<IList<PermissionDetailsModel>> GetPermissionDetails(int[] ids);
        IQueryable<PermissionDetailsModel> GetPermissionDetailsByPermissionId(int permissionId);
        Task<PermissionDetailsModel> GetPermissionDetailsAsync(int id);
        Task<bool> InsertPermissionDetailsAsync(PermissionDetailsModel model);
        Task<bool> InsertPermissionDetailsRangesAsync(IList<PermissionDetailsModel> models);
        Task<bool> UpdatePermissionDetailsAsync(int id, PermissionDetailsModel model);
        Task<bool> DeletePermissionDetailsAsync(int id);
        Task<bool> DeletePermissionDetailsRangeAsync(int[] ids);
        Task<bool> DeletePermissionDetailsByPermissionId(int permissionId);
    }
}
