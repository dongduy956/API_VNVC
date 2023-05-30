using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IStaffServices
    {
        IQueryable<StaffModel> GetStaffs();
        IQueryable<StaffModel> SearchStaffs(string q="");
        Task<StaffModel> GetStaff(int id);
        Task<bool> InsertStaff(StaffModel staffModel);
        Task<bool> InsertStaffsRange(IList<StaffModel> staffModels);
        Task<bool> UpdateStaff(int id, StaffModel staffModel);
        Task<bool> DeleteStaff(int id);
    }
}
