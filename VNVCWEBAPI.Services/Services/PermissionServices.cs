using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class PermissionServices : IPermissionServices
    {
        private readonly IRepository<Permission> repository;
        public PermissionServices(IRepository<Permission> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeletePermission(int id)
        {
            var permission = await repository.GetAsync(id);
            return await repository.Delete(permission);
        }

        public async Task<PermissionModel> GetPermission(int id)
        {
            var permission = await repository.GetAsync(id);
            return new PermissionModel
            {
                Id=permission.Id,
                Name = permission.Name,
                Created=permission.Created
            };
        }

        public IQueryable<PermissionModel> GetPermissions()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Select(permission => new PermissionModel
                    {
                        Id= permission.Id,
                        Name = permission.Name,
                        Created = permission.Created

                    });
        }

        public async Task<bool> InsertPermission(PermissionModel permissionModel)
        {
            var permission = new Permission
            {
                Name = permissionModel.Name
            };
            return await repository.InsertAsync(permission);
        }

        public async Task<bool> InsertPermissionsRange(IList<PermissionModel> permissionModels)
        {
            var permissions = new List<Permission>();
            foreach (var permissionModel in permissionModels)
            {
                permissions.Add(new Permission
                {
                    Name=permissionModel.Name
                });
            }
            var result = await repository.InsertRangeAsync(permissions);
            for (int i = 0; i < permissions.Count; i++)
            {
                permissionModels[i].Id = permissions[i].Id;
                permissionModels[i].Created = permissions[i].Created;
            }
            return result;
        }

        public IQueryable<PermissionModel> SearchPermissions(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.Name.Trim().ToLower().Contains(q))
                .Select(model => new PermissionModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Created = model.Created

                });
            return results;
        }

        public async Task<bool> UpdatePermission(int id, PermissionModel permissionModel)
        {
            var permission = await repository.GetAsync(id);

            permission.Name = permissionModel.Name;
            return await repository.UpdateAsync(permission);
        }
    }
}
