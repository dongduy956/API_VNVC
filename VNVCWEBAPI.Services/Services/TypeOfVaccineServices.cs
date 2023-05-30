using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class TypeOfVaccineServices : ITypeOfVaccineServices
    {
        private readonly IRepository<TypeOfVaccine> repository;
        public TypeOfVaccineServices(IRepository<TypeOfVaccine> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteTypeOfVaccine(int id)
        {
            var typeOfVaccine = await repository.GetAsync(id);
            return await repository.Delete(typeOfVaccine);
        }

        public async Task<TypeOfVaccineModel> GetTypeOfVaccine(int id)
        {
            var typeOfVaccine = await repository.GetAsync(id);
            return new TypeOfVaccineModel
            {
                Id = typeOfVaccine.Id,
                Name = typeOfVaccine.Name,
                Created = typeOfVaccine.Created
            };
        }

        public IQueryable<TypeOfVaccineModel> GetTypeOfVaccines()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Select(typeOfVaccine => new TypeOfVaccineModel
                {
                    Id = typeOfVaccine.Id,
                    Name = typeOfVaccine.Name,
                    Created = typeOfVaccine.Created

                });
        }

        public async Task<bool> InsertTypeOfVaccine(TypeOfVaccineModel typeOfVaccineModel)
        {
            var typeOfVaccine = new TypeOfVaccine
            {
                Name = typeOfVaccineModel.Name,
            };
            var result = await repository.InsertAsync(typeOfVaccine);
            typeOfVaccineModel.Id = typeOfVaccine.Id;
            typeOfVaccineModel.Created = typeOfVaccine.Created;
            return result;
        }

        public async Task<bool> InsertTypeOfVaccinesRange(IList<TypeOfVaccineModel> typeOfVaccineModels)
        {
            var typeOfVaccines = new List<TypeOfVaccine>();
            foreach (var typeOfVaccineModel in typeOfVaccineModels)
            {
               typeOfVaccines.Add(new TypeOfVaccine 
               { 
                   Name=typeOfVaccineModel.Name
               });
            }
            var result = await repository.InsertRangeAsync(typeOfVaccines);
            for (int i = 0; i < typeOfVaccines.Count; i++)
            {
                typeOfVaccineModels[i].Id = typeOfVaccines[i].Id;
                typeOfVaccineModels[i].Created = typeOfVaccines[i].Created;
            }
            return result;
        }

        public IQueryable<TypeOfVaccineModel> SearchTypeOfVaccines(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.Name.Trim().ToLower().Contains(q))
                .Select(model => new TypeOfVaccineModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Created=model.Created
                });
            return results;
        }

        public async Task<bool> UpdateTypeOfVaccine(int id, TypeOfVaccineModel typeOfVaccineModel)
        {
            var typeOfVaccine = await repository.GetAsync(id);

            typeOfVaccine.Name = typeOfVaccineModel.Name;
            return await repository.UpdateAsync(typeOfVaccine);
        }
    }
}
