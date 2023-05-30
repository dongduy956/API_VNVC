using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class VaccinePackageServices : IVaccinePackageServices
    {
        private readonly IRepository<VaccinePackage> repository;
        public VaccinePackageServices(IRepository<VaccinePackage> repository)
        {
            this.repository = repository;
        }
        public async Task<bool> DeleteVaccinePackage(int id)
        {
            var vaccinePagkage = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(vaccinePagkage);
        }

        public IQueryable<VaccinePackageModel> GetVaccinePackages()
        {
            return repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Select(vaccinePackage => new VaccinePackageModel
                {
                    Id = vaccinePackage.Id,
                    Name = vaccinePackage.Name,
                    ObjectInjection = vaccinePackage.ObjectInjection,
                    Created = vaccinePackage.Created
                });
        }

        public async Task<VaccinePackageModel> GetVaccinePackage(int id)
        {
            var vaccinePackage = await repository.GetAsync(id);
            return new VaccinePackageModel
            {
                Name = vaccinePackage.Name,
                ObjectInjection = vaccinePackage.ObjectInjection,
                Created = vaccinePackage.Created,
                Id = vaccinePackage.Id
            };
        }

        public async Task<bool> InsertVaccinePackage(VaccinePackageModel vaccinePackageModel)
        {
            var vaccinePackage = new VaccinePackage
            {
                Name = vaccinePackageModel.Name,
                ObjectInjection = vaccinePackageModel.ObjectInjection,
            };
            var result = await repository.InsertAsync(vaccinePackage);
            vaccinePackageModel.Id = vaccinePackage.Id;
            vaccinePackageModel.Created = vaccinePackage.Created;
            return result;
        }

        public async Task<bool> UpdateVaccinePackage(int id, VaccinePackageModel vaccinePackageModel)
        {
            var vaccinePackage = await repository.GetAsync(id);
            vaccinePackage.ObjectInjection = vaccinePackageModel.ObjectInjection;
            return await repository.UpdateAsync(vaccinePackage);
        }

        public IQueryable<VaccinePackageModel> SearchVaccinePackages(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x =>  x.Name.ToLower().Contains(q) ||
                            x.ObjectInjection.ToLower().Contains(q))
                .Select(model => new VaccinePackageModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    ObjectInjection = model.ObjectInjection,
                    Created = model.Created

                });
            return results;
        }

        public async Task<bool> InsertVaccinePackagesRange(IList<VaccinePackageModel> vaccinePackageModels)
        {
            var vaccinePackages = new List<VaccinePackage>();
            foreach (var vaccinePackageModel in vaccinePackageModels)
            {
                vaccinePackages.Add(new VaccinePackage
                {
                    Name = vaccinePackageModel.Name,
                    ObjectInjection = vaccinePackageModel.ObjectInjection,
                });
            }
            var result = await repository.InsertRangeAsync(vaccinePackages);
            for (int i = 0; i < vaccinePackages.Count; i++)
            {
                vaccinePackageModels[i].Id = vaccinePackages[i].Id;
                vaccinePackageModels[i].Created = vaccinePackages[i].Created;
            }
            return result;
        }
    }
}
