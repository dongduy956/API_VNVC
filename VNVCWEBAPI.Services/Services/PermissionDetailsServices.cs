using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class PermissionDetailsServices : IPermissionDetailsServices
    {
        private readonly IRepository<PermissionDetails> repository;
        public PermissionDetailsServices(IRepository<PermissionDetails> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeletePermissionDetailsAsync(int id)
        {
            var model = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(model);
        }

        public Task<bool> DeletePermissionDetailsByPermissionId(int permissionId)
        {
            var permission = repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Where(x => x.PermissionId == permissionId);
            return repository.DeleteFromTrashRange(permission);
        }

        public async Task<bool> DeletePermissionDetailsRangeAsync(int[] ids)
        {
            var lstPermissionDetails = new List<PermissionDetails>();
            foreach (var id in ids)
            {
                var permissionDetails = await repository.GetAsync(id);
                lstPermissionDetails.Add(permissionDetails);
            }
            return await repository.DeleteFromTrashRange(lstPermissionDetails);
        }

        public IQueryable<PermissionDetailsModel> GetPermissionDetails()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Select(x => new PermissionDetailsModel
                {
                    Created = x.Created,
                    Id = x.Id,
                    PermissionId = x.PermissionId,
                    PermissionType = x.PermissionType,
                    PermissionValue = x.PermissionValue
                });
        }

        public async Task<IList<PermissionDetailsModel>> GetPermissionDetails(int[] ids)
        {
            var lstPermissionDetails = new List<PermissionDetailsModel>();
            foreach (var id in ids)
            {
                var permissionDetails = await this.GetPermissionDetailsAsync(id); ;
                lstPermissionDetails.Add(permissionDetails);
            }
            return lstPermissionDetails;
        }

        public async Task<PermissionDetailsModel> GetPermissionDetailsAsync(int id)
        {
            var permissionDetails = await repository.GetAsync(id);
            var model = new PermissionDetailsModel
            {
                Created = permissionDetails.Created,
                Id = permissionDetails.Id,
                PermissionId = permissionDetails.PermissionId,
                PermissionType = permissionDetails.PermissionType,
                PermissionValue = permissionDetails.PermissionValue
            };
            return model;
        }

        public IQueryable<PermissionDetailsModel> GetPermissionDetailsByPermissionId(int permissionId)
        {
            return repository.GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Where(x => x.PermissionId == permissionId)
                .Select(x => new PermissionDetailsModel
                {
                    PermissionId = x.PermissionId,
                    Created = x.Created,
                    Id = x.Id,
                    PermissionType = x.PermissionType,
                    PermissionValue = x.PermissionValue
                });
        }

        public async Task<bool> InsertPermissionDetailsAsync(PermissionDetailsModel model)
        {
            var PermissionDetails = new PermissionDetails
            {
                PermissionValue = model.PermissionValue,
                PermissionType = model.PermissionType,
                PermissionId = model.PermissionId,
            };
            var result = await repository.InsertAsync(PermissionDetails);
            model.Id = PermissionDetails.Id;
            model.Created = PermissionDetails.Created;
            return result;
        }

        public async Task<bool> InsertPermissionDetailsRangesAsync(IList<PermissionDetailsModel> models)
        {
            var lstPermissionDetails = models
                .Select(x => new PermissionDetails
                {
                    PermissionId = x.PermissionId,
                    PermissionType = x.PermissionType,
                    PermissionValue = x.PermissionValue
                }).ToList();
            var result = await repository.InsertRangeAsync(lstPermissionDetails);
            for (int i = 0; i < models.Count; i++)
            {
                models[i].Id = lstPermissionDetails[i].Id;
                models[i].Created = lstPermissionDetails[i].Created;
            }
            return result;
        }

        public async Task<bool> UpdatePermissionDetailsAsync(int id, PermissionDetailsModel model)
        {
            if (model.Id != id)
                return false;
            var permissionDetails = await repository.GetAsync(id);
            permissionDetails.PermissionValue = model.PermissionValue;
            permissionDetails.PermissionType = model.PermissionType;
            permissionDetails.PermissionId = model.PermissionId;
            return await repository.UpdateAsync(permissionDetails);
        }
    }
}
