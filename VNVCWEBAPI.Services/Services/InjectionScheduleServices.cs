using Microsoft.EntityFrameworkCore;
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
    public class InjectionScheduleServices : IInjectionScheduleServices
    {
        private readonly IRepository<InjectionSchedule> repository;
        public InjectionScheduleServices(IRepository<InjectionSchedule> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteInjectionScheduleAsync(int id)
        {
            var injectionSchedule = await repository.GetAsync(id);
            return await repository.Delete(injectionSchedule);
        }  

        public InjectionScheduleModel? GetInjectionScheduleAsync(int id)
        {
            return GetInjectionSchedules().FirstOrDefault(x => x.Id == id);

        }

        public IQueryable<InjectionScheduleModel> GetInjectionSchedules()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(cm => cm.Customer)
                .Include(n => n.Nominator)
                .Include(st => st.Staff)
                .Include(ud => ud.Updater)
                .Include(id => id.InjectionScheduleDetails)
                .Select(injectionSchedule => new InjectionScheduleModel
                {
                    Id = injectionSchedule.Id,
                    CustomerId = injectionSchedule.CustomerId,
                    CustomerName = injectionSchedule.Customer.FirstName + ' ' + injectionSchedule.Customer.LastName,
                    Created = injectionSchedule.Created,
                    Date = injectionSchedule.Date,
                    NominatorId = injectionSchedule.NominatorId,
                    NominatorName = injectionSchedule.Nominator.StaffName,
                    Note = injectionSchedule.Note,
                    Priorities = injectionSchedule.Priorities,
                    StaffId = injectionSchedule.StaffId,
                    StaffName = injectionSchedule.Staff.StaffName,
                    UpdaterId = injectionSchedule.UpdaterId,
                    UpdaterName = injectionSchedule.Updater.StaffName,
                    UpdateTime = injectionSchedule.UpdateTime,
                    CheckPay = injectionSchedule.InjectionScheduleDetails.FirstOrDefault(x => x.Pay == false) != null,
                });
        }       

        public async Task<bool> InsertInjectionScheduleAsync(InjectionScheduleModel model)
        {
            var injectionSchedule = new InjectionSchedule
            {
                CustomerId = model.CustomerId,
                Date = model.Date,
                NominatorId = model.NominatorId,
                Note = model.Note,
                Priorities = model.Priorities,
                StaffId = model.StaffId,
                UpdaterId = model.UpdaterId,
                UpdateTime = model.UpdateTime,
            };
            var result = await repository.InsertAsync(injectionSchedule);
            model.Id = injectionSchedule.Id;
            model.Created = injectionSchedule.Created;
            return result;
        }

        public IQueryable<InjectionScheduleModel> SearchInjectionSchedules(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(cm => cm.Customer)
                .Include(n => n.Nominator)
                .Include(st => st.Staff)
                .Include(ud => ud.Updater)
                .Include(id => id.InjectionScheduleDetails)
                .Where(x => x.Created.ToString().Contains(q) ||
                            (x.Customer.FirstName + ' ' + x.Customer.LastName).ToLower().Contains(q) ||
                            x.Date.ToString().Contains(q) ||
                            x.Nominator.StaffName.ToLower().Contains(q) ||
                            x.Note.ToLower().Contains(q) ||
                            (x.Priorities == 0 ? "đặt trước" : "trực tiếp").ToLower().Contains(q) ||
                            x.Staff.StaffName.ToLower().Contains(q) ||
                            x.Updater.StaffName.ToLower().Contains(q) ||
                            x.UpdateTime.ToString().Contains(q))
                .Select(injectionSchedule => new InjectionScheduleModel
                {
                    Id = injectionSchedule.Id,
                    CustomerId = injectionSchedule.CustomerId,
                    CustomerName = injectionSchedule.Customer.FirstName + ' ' + injectionSchedule.Customer.LastName,
                    Created = injectionSchedule.Created,
                    Date = injectionSchedule.Date,
                    NominatorId = injectionSchedule.NominatorId,
                    NominatorName = injectionSchedule.Nominator.StaffName,
                    Note = injectionSchedule.Note,
                    Priorities = injectionSchedule.Priorities,
                    StaffId = injectionSchedule.StaffId,
                    StaffName = injectionSchedule.Staff.StaffName,
                    UpdaterId = injectionSchedule.UpdaterId,
                    UpdaterName = injectionSchedule.Updater.StaffName,
                    UpdateTime = injectionSchedule.UpdateTime,
                    CheckPay = injectionSchedule.InjectionScheduleDetails.FirstOrDefault(x => x.Pay == false) != null
                });
            return results;
        }

        public async Task<bool> UpdateInjectionScheduleAsync(int id, InjectionScheduleModel model)
        {
            var injectionSchedule = await repository.GetAsync(id);
            if (injectionSchedule == null)
                return false;
            injectionSchedule.Note = model.Note;
            injectionSchedule.UpdaterId = model.UpdaterId;
            injectionSchedule.UpdateTime = model.UpdateTime;
            injectionSchedule.NominatorId= model.NominatorId;
            return await repository.UpdateAsync(injectionSchedule);
        }
    }
}
