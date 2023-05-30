using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class StaffServices : IStaffServices
    {
        private readonly IRepository<Staff> repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string myHostUrl = "";

        public StaffServices(IRepository<Staff> repository, IHttpContextAccessor _httpContextAccessor)
        {
            this.repository = repository;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public async Task<bool> DeleteStaff(int id)
        {
            var staff = await repository.GetAsync(id);
            return await repository.Delete(staff);
        }

        public async Task<StaffModel> GetStaff(int id)
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            var staff = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(p => p.Permission)
                .Where(x => x.Permission.isTrash == false)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (staff == null)
                return null;
            return new StaffModel
            {
                Id = staff.Id,
                PhoneNumber = staff.PhoneNumber,
                Email = staff.Email,
                Address = staff.Address,
                Avatar = string.IsNullOrEmpty(staff.Avatar) ? "" : $"{myHostUrl}{staff.Avatar}",
                Country = staff.Country,
                DateOfBirth = staff.DateOfBirth,
                District = staff.District,
                IdentityCard = staff.IdentityCard,
                PermissionId = staff.PermissionId,
                Province = staff.Province,
                Sex = staff.Sex,
                StaffName = staff.StaffName,
                Village = staff.Village,
                PermissionName = staff.Permission.Name,
                Created = staff.Created
            };
        }

        public IQueryable<StaffModel> GetStaffs()
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(p => p.Permission)
                .Where(x => x.Permission.isTrash == false)
                    .Select(staff => new StaffModel
                    {
                        Id = staff.Id,
                        PhoneNumber = staff.PhoneNumber,
                        Email = staff.Email,
                        Address = staff.Address,
                        Avatar = string.IsNullOrEmpty(staff.Avatar) ? "" : $"{myHostUrl}{staff.Avatar}",
                        Country = staff.Country,
                        DateOfBirth = staff.DateOfBirth,
                        District = staff.District,
                        IdentityCard = staff.IdentityCard,
                        PermissionId = staff.PermissionId,
                        Province = staff.Province,
                        Sex = staff.Sex,
                        StaffName = staff.StaffName,
                        Village = staff.Village,
                        PermissionName = staff.Permission.Name,
                        Created = staff.Created

                    });
        }

        public async Task<bool> InsertStaff(StaffModel staffModel)
        {
            var staff = new Staff
            {
                StaffName = staffModel.StaffName,
                Address = staffModel.Address,
                PermissionId = staffModel.PermissionId,
                Avatar = staffModel.Avatar,
                Country = staffModel.Country,
                DateOfBirth = staffModel.DateOfBirth,
                District = staffModel.District,
                Email = staffModel.Email,
                IdentityCard = staffModel.IdentityCard,
                PhoneNumber = staffModel.PhoneNumber,
                Province = staffModel.Province,
                Village = staffModel.Village,
                Sex = staffModel.Sex,

            };
            var result = await repository.InsertAsync(staff);
            staffModel.Id = staff.Id;
            staffModel.Created = staff.Created;
            return result;
        }

        public async Task<bool> InsertStaffsRange(IList<StaffModel> staffModels)
        {
            var staffs = new List<Staff>();
            foreach (var staffModel in staffModels)
            {
                staffs.Add(new Staff
                {
                    StaffName = staffModel.StaffName,
                    Address = staffModel.Address,
                    PermissionId = staffModel.PermissionId,
                    Avatar = staffModel.Avatar,
                    Country = staffModel.Country,
                    DateOfBirth = staffModel.DateOfBirth,
                    District = staffModel.District,
                    Email = staffModel.Email,
                    IdentityCard = staffModel.IdentityCard,
                    PhoneNumber = staffModel.PhoneNumber,
                    Province = staffModel.Province,
                    Village = staffModel.Village,
                    Sex = staffModel.Sex,
                });
            }
            var result = await repository.InsertRangeAsync(staffs);
            for (int i = 0; i < staffs.Count; i++)
            {
                staffModels[i].Id = staffs[i].Id;
                staffModels[i].Created = staffs[i].Created;
            }
            return result;
        }

        public IQueryable<StaffModel> SearchStaffs(string q = "")
        {
            myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(p => p.Permission)
                .Where(x => x.Permission.isTrash == false &&
                          (x.StaffName.Trim().ToLower().Contains(q) ||
                          x.DateOfBirth.ToString().Equals(q) ||
                          x.Address.ToLower().Contains(q) ||
                          x.Permission.Name.ToLower().Contains(q) ||
                          x.Country.ToLower().Contains(q) ||
                          x.District.ToLower().Contains(q) ||
                          x.Email.ToLower().Equals(q) ||
                          x.IdentityCard.ToLower().Contains(q) ||
                          x.PhoneNumber.ToLower().Equals(q) ||
                          x.Province.ToLower().Contains(q) ||
                          x.Village.ToLower().Contains(q) ||
                          (x.Sex ? "Nam" : "Nữ").ToLower().Equals(q)))
                .Select(staff => new StaffModel
                {
                    Id = staff.Id,
                    PhoneNumber = staff.PhoneNumber,
                    Email = staff.Email,
                    Address = staff.Address,
                    Avatar = string.IsNullOrEmpty(staff.Avatar) ? "" : $"{myHostUrl}{staff.Avatar}",
                    Country = staff.Country,
                    DateOfBirth = staff.DateOfBirth,
                    District = staff.District,
                    IdentityCard = staff.IdentityCard,
                    PermissionId = staff.PermissionId,
                    Province = staff.Province,
                    Sex = staff.Sex,
                    StaffName = staff.StaffName,
                    Village = staff.Village,
                    PermissionName = staff.Permission.Name,
                    Created = staff.Created

                });
            return results;
        }

        public async Task<bool> UpdateStaff(int id, StaffModel staffModel)
        {
            var staff = await repository.GetAsync(id);

            staff.StaffName = staffModel.StaffName;
            staff.Address = staffModel.Address;
            staff.PermissionId = staffModel.PermissionId;
            staff.Avatar = staffModel.Avatar;
            staff.Country = staffModel.Country;
            staff.DateOfBirth = staffModel.DateOfBirth;
            staff.District = staffModel.District;
            staff.Email = staffModel.Email;
            staff.IdentityCard = staffModel.IdentityCard;
            staff.PhoneNumber = staffModel.PhoneNumber;
            staff.Province = staffModel.Province;
            staff.Village = staffModel.Village;
            staff.Sex = staffModel.Sex;

            return await repository.UpdateAsync(staff);
        }
    }
}
