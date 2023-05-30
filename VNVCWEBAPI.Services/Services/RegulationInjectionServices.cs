using Microsoft.EntityFrameworkCore;
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
    public class RegulationInjectionServices : IRegulationInjectionServices
    {
        private readonly IRepository<RegulationInjection> repository;
        public RegulationInjectionServices(IRepository<RegulationInjection> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteRegulationInjection(int id)
        {
            var regulationInjection = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(regulationInjection);
        }

        public async Task<bool> DeleteRegulationInjectionsRange(int[] ids)
        {
            var regulationInjections = new List<RegulationInjection>();
            foreach (var id in ids)
            {
                var regulationInjection = await repository.GetAsync(id);
                if (regulationInjection != null)
                {
                    regulationInjections.Add(regulationInjection);
                }
            }
            return await repository.DeleteFromTrashRange(regulationInjections);
        }

        public async Task<RegulationInjectionModel> GetRegulationInjection(int id)
        {
            var regulationInjection = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                 .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            return new RegulationInjectionModel
            {
                Id = regulationInjection.Id,
                Distance = regulationInjection.Distance,
                Order = regulationInjection.Order,
                RepeatInjection = regulationInjection.RepeatInjection,
                VaccineId = regulationInjection.VaccineId,
                VaccineName = regulationInjection.Vaccine.Name,
                Created = regulationInjection.Created
            };
        }

        public RegulationInjectionModel? GetRegulationInjectionByVaccineId(int vaccineId)
        {
            return GetRegulationInjections()
                    .FirstOrDefault(x => x.VaccineId == vaccineId);
        }

        public IQueryable<RegulationInjectionModel> GetRegulationInjections()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                  .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false)
                  .Select(regulationInjection => new RegulationInjectionModel
                  {
                      Id = regulationInjection.Id,
                      Distance = regulationInjection.Distance,
                      Order = regulationInjection.Order,
                      RepeatInjection = regulationInjection.RepeatInjection,
                      VaccineId = regulationInjection.VaccineId,
                      VaccineName = regulationInjection.Vaccine.Name,
                      Created = regulationInjection.Created
                  });
        }

        public async Task<bool> InsertRegulationInjection(RegulationInjectionModel regulationInjectionModel)
        {
            var regulationInjection = new RegulationInjection
            {
                Distance = regulationInjectionModel.Distance,
                RepeatInjection = regulationInjectionModel.RepeatInjection,
                VaccineId = regulationInjectionModel.VaccineId,
                Order = regulationInjectionModel.Order,
            };
            var result = await repository.InsertAsync(regulationInjection);
            regulationInjectionModel.Id = regulationInjection.Id;
            regulationInjectionModel.Created = regulationInjection.Created;
            return result;
        }

        public async Task<bool> InsertRegulationInjectionsRange(IList<RegulationInjectionModel> regulationInjectionModels)
        {
            var regulationInjections = new List<RegulationInjection>();
            foreach (var regulationInjectionModel in regulationInjectionModels)
            {
                regulationInjections.Add(new RegulationInjection
                {
                    Distance = regulationInjectionModel.Distance,
                    RepeatInjection = regulationInjectionModel.RepeatInjection,
                    VaccineId = regulationInjectionModel.VaccineId,
                    Order = regulationInjectionModel.Order,
                });
            }
            var result = await repository.InsertRangeAsync(regulationInjections);
            for (int i = 0; i < regulationInjections.Count; i++)
            {
                regulationInjectionModels[i].Id = regulationInjections[i].Id;
                regulationInjectionModels[i].Created = regulationInjections[i].Created;
            }
            return result;
        }

        public IQueryable<RegulationInjectionModel> SearchRegulationInjections(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(vc => vc.Vaccine)
                    .ThenInclude(x => x.TypeOfVaccine)
                .Where(x => x.Vaccine.isTrash == false && x.Vaccine.TypeOfVaccine.isTrash == false &&
                        (x.Distance.ToString().Equals(q) ||
                       x.Order.ToString().Equals(q) ||
                       (x.RepeatInjection == -1 ? "tiêm nhắc" : "không tiêm nhắc").Contains(q) ||
                        x.Vaccine.Name.ToLower().Contains(q)))
                .Select(regulationInjection => new RegulationInjectionModel
                {
                    Id = regulationInjection.Id,
                    Distance = regulationInjection.Distance,
                    Order = regulationInjection.Order,
                    RepeatInjection = regulationInjection.RepeatInjection,
                    VaccineId = regulationInjection.VaccineId,
                    VaccineName = regulationInjection.Vaccine.Name,
                    Created = regulationInjection.Created
                });
            return results;
        }

        public async Task<bool> UpdateRegulationInjection(int id, RegulationInjectionModel regulationInjectionModel)
        {
            var regulationInjection = await repository.GetAsync(id);
            regulationInjection.Distance = regulationInjectionModel.Distance;
            regulationInjection.RepeatInjection = regulationInjectionModel.RepeatInjection;
            regulationInjection.Order = regulationInjectionModel.Order;
            return await repository.UpdateAsync(regulationInjection);
        }
    }
}
