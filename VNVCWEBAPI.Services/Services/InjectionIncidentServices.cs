using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.Services
{
    public class InjectionIncidentServices : IInjectionIncidentServices
    {
        private readonly IRepository<InjectionIncident> repository;
        public InjectionIncidentServices(IRepository<InjectionIncident> repository)
        {
            this.repository = repository;
        }



        public async Task<InjectionIncidentModel> GetInjectionIncidentAsync(int id)
        {
            var InjectionIncident = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(vc => vc.Vaccine)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(x => x.Id == id);
            var model = new InjectionIncidentModel
            {
                Id = InjectionIncident.Id,
                Content = InjectionIncident.Content,
                InjectionScheduleId = InjectionIncident.InjectionScheduleId,
                InjectionTime = InjectionIncident.InjectionTime,
                VaccineId = InjectionIncident.VaccineId,
                VaccineName = InjectionIncident.Vaccine.Name,
                Created = InjectionIncident.Created,
                ShipmentCode = InjectionIncident.Shipment.ShipmentCode,
                ShipmentId = InjectionIncident.ShipmentId

            };
            return model;
        }

        public IQueryable<InjectionIncidentModel> GetInjectionIncidents()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(vc => vc.Vaccine)
                 .Include(s => s.Shipment)
                .Select(InjectionIncident => new InjectionIncidentModel
                {
                    Id = InjectionIncident.Id,
                    Content = InjectionIncident.Content,
                    InjectionScheduleId = InjectionIncident.InjectionScheduleId,
                    InjectionTime = InjectionIncident.InjectionTime,
                    VaccineId = InjectionIncident.VaccineId,
                    VaccineName = InjectionIncident.Vaccine.Name,
                    Created = InjectionIncident.Created,
                    ShipmentCode = InjectionIncident.Shipment.ShipmentCode,
                    ShipmentId = InjectionIncident.ShipmentId
                });
        }

        public async Task<bool> InsertInjectionIncidentAsync(InjectionIncidentModel model)
        {
            var injectionIncident = new InjectionIncident
            {
                Content = model.Content,
                InjectionScheduleId = model.InjectionScheduleId,
                InjectionTime = model.InjectionTime,
                VaccineId = model.VaccineId,
                ShipmentId = model.ShipmentId,
            };
            var result = await repository.InsertAsync(injectionIncident);
            model.Created = injectionIncident.Created;
            model.Id = injectionIncident.Id;
            return result;
        }

        public IQueryable<InjectionIncidentModel> SearchInjectionIncidents(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                 .Include(vc => vc.Vaccine)
                 .Include(s => s.Shipment)
                .Where(x => x.Content.ToLower().Contains(q) ||
                x.InjectionScheduleId.ToString().Equals(q) ||
                x.InjectionTime.ToString().Contains(q) ||
                x.Vaccine.Name.ToLower().Contains(q) ||
                x.Shipment.ShipmentCode.ToLower().Equals(q))
                .Select(InjectionIncident => new InjectionIncidentModel
                {
                    Id = InjectionIncident.Id,
                    Content = InjectionIncident.Content,
                    InjectionScheduleId = InjectionIncident.InjectionScheduleId,
                    InjectionTime = InjectionIncident.InjectionTime,
                    VaccineId = InjectionIncident.VaccineId,
                    VaccineName = InjectionIncident.Vaccine.Name,
                    Created = InjectionIncident.Created,
                    ShipmentId = InjectionIncident.ShipmentId,
                    ShipmentCode = InjectionIncident.Shipment.ShipmentCode
                });
            return results;
        }


    }
}
