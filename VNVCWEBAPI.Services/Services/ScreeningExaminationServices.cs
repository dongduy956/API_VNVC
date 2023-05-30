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
    public class ScreeningExaminationServices : IScreeningExaminationServices
    {
        private readonly IRepository<ScreeningExamination> repository;
        public ScreeningExaminationServices(IRepository<ScreeningExamination> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteScreeningExamination(int id)
        {
            var screeningExamination = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(screeningExamination);
        }

        public async Task<bool> DeleteScreeningExaminationsRange(int[] ids)
        {
            var screeningExaminations = new List<ScreeningExamination>();
            foreach (var id in ids)
            {
                var screeningExamination = await repository.GetAsync(id);
                if (screeningExamination != null)
                {
                    screeningExaminations.Add(screeningExamination);
                }

            }
            return await repository.DeleteFromTrashRange(screeningExaminations);
        }

        public async Task<ScreeningExaminationModel?> GetScreeningExamination(int id)
        {
            var screeningExamination = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .Include(ct => ct.Customer)
                    .ThenInclude(x => x.CustomerType)
                .FirstOrDefaultAsync(x => x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false && x.Id == id);
            if (screeningExamination != null)
                return new ScreeningExaminationModel
                {
                    Id = screeningExamination.Id,
                    StaffName = screeningExamination.Staff.StaffName,
                    CustomerName = screeningExamination.Customer.FirstName + ' ' + screeningExamination.Customer.LastName,
                    BloodPressure = screeningExamination.BloodPressure,
                    CustomerId = screeningExamination.CustomerId,
                    Diagnostic = screeningExamination.Diagnostic,
                    Heartbeat = screeningExamination.Heartbeat,
                    Height = screeningExamination.Height,
                    Note = screeningExamination.Note,
                    StaffId = screeningExamination.StaffId,
                    Temperature = screeningExamination.Temperature,
                    Weight = screeningExamination.Weight,
                    isEligible = screeningExamination.isEligible,
                    Created = screeningExamination.Created,
                    IsHide = screeningExamination.Staff.isTrash == true || screeningExamination.Staff.Permission.isTrash == true
                };
            return null;
        }

        public IQueryable<ScreeningExaminationModel> GetScreeningExaminations()
        {
            return repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .Include(ct => ct.Customer)
                    .ThenInclude(x => x.CustomerType)
                .Where(x => x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false)
                .Select(screeningExamination => new ScreeningExaminationModel
                {
                    Id = screeningExamination.Id,
                    StaffName = screeningExamination.Staff.StaffName,
                    CustomerName = screeningExamination.Customer.FirstName + ' ' + screeningExamination.Customer.LastName,
                    BloodPressure = screeningExamination.BloodPressure,
                    CustomerId = screeningExamination.CustomerId,
                    Diagnostic = screeningExamination.Diagnostic,
                    Heartbeat = screeningExamination.Heartbeat,
                    Height = screeningExamination.Height,
                    Note = screeningExamination.Note,
                    StaffId = screeningExamination.StaffId,
                    Temperature = screeningExamination.Temperature,
                    isEligible = screeningExamination.isEligible,
                    Weight = screeningExamination.Weight,
                    Created = screeningExamination.Created,
                    IsHide = screeningExamination.Staff.isTrash == true || screeningExamination.Staff.Permission.isTrash == true
                });
        }

        public async Task<bool> InsertScreeningExamination(ScreeningExaminationModel screeningExaminationModel)
        {
            var screeningExamination = new ScreeningExamination
            {
                BloodPressure = screeningExaminationModel.BloodPressure,
                CustomerId = screeningExaminationModel.CustomerId,
                Diagnostic = screeningExaminationModel.Diagnostic,
                Heartbeat = screeningExaminationModel.Heartbeat,
                Height = screeningExaminationModel.Height,
                Note = screeningExaminationModel.Note,
                StaffId = screeningExaminationModel.StaffId,
                Temperature = screeningExaminationModel.Temperature,
                Weight = screeningExaminationModel.Weight,
                isEligible = screeningExaminationModel.isEligible,

            };
            var result = await repository.InsertAsync(screeningExamination);
            screeningExaminationModel.Id = screeningExamination.Id;
            screeningExaminationModel.Created = screeningExamination.Created;
            return result;
        }

        public IQueryable<ScreeningExaminationModel> SearchScreeningExaminations(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(st => st.Staff)
                    .ThenInclude(x => x.Permission)
                .Include(ct => ct.Customer)
                    .ThenInclude(x => x.CustomerType)
                .Where(x => x.Customer.isTrash == false && x.Customer.CustomerType.isTrash == false && (x.Weight.ToString().Equals(q) ||
                            x.Temperature.ToString().Equals(q) ||
                            (x.Customer.FirstName + ' ' + x.Customer.LastName).ToLower().Contains(q) ||
                            x.BloodPressure.ToString().Equals(q) ||
                            x.Diagnostic.ToLower().Contains(q) ||
                            x.Heartbeat.ToString().Equals(q) ||
                            x.Height.ToString().Equals(q) ||
                            x.Note.ToLower().Contains(q) ||
                            x.Staff.StaffName.ToLower().Contains(q)))
                .Select(screeningExamination => new ScreeningExaminationModel
                {
                    Id = screeningExamination.Id,
                    StaffName = screeningExamination.Staff.StaffName,
                    CustomerName = screeningExamination.Customer.FirstName + ' ' + screeningExamination.Customer.LastName,
                    BloodPressure = screeningExamination.BloodPressure,
                    CustomerId = screeningExamination.CustomerId,
                    Diagnostic = screeningExamination.Diagnostic,
                    Heartbeat = screeningExamination.Heartbeat,
                    Height = screeningExamination.Height,
                    Note = screeningExamination.Note,
                    StaffId = screeningExamination.StaffId,
                    Temperature = screeningExamination.Temperature,
                    Weight = screeningExamination.Weight,
                    isEligible = screeningExamination.isEligible,
                    Created = screeningExamination.Created,
                    IsHide = screeningExamination.Staff.isTrash == true || screeningExamination.Staff.Permission.isTrash == true
                });
            return results;
        }

        public async Task<bool> UpdateScreeningExamination(int id, ScreeningExaminationModel screeningExaminationModel)
        {
            var screeningExamination = await repository.GetAsync(id);
            screeningExamination.BloodPressure = screeningExaminationModel.BloodPressure;
            screeningExamination.Diagnostic = screeningExaminationModel.Diagnostic;
            screeningExamination.Heartbeat = screeningExaminationModel.Heartbeat;
            screeningExamination.Height = screeningExaminationModel.Height;
            screeningExamination.Note = screeningExaminationModel.Note;
            screeningExamination.StaffId = screeningExaminationModel.StaffId;
            screeningExamination.Temperature = screeningExaminationModel.Temperature;
            screeningExamination.isEligible = screeningExaminationModel.isEligible;
            screeningExamination.Weight = screeningExaminationModel.Weight;
            return await repository.UpdateAsync(screeningExamination);
        }
    }
}
