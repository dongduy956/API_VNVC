using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using VNVCWEBAPI.Common.Config;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.REPO.IRepository;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.IServices;
using VNVCWEBAPI.Services.ViewModels;
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class InjectionScheduleDetailServices : IInjectionScheduleDetailServices
    {
        private readonly IRepository<InjectionScheduleDetail> repository;
        private readonly IInjectionScheduleServices injectionScheduleServices;
        private readonly IInjectionIncidentServices injectionIncidentServices;
        private readonly IRegulationInjectionServices regulationInjectionServices;
        private readonly ICustomerServices customerServices;
        private readonly IVaccineServices vaccineServices;
        private readonly IMailServices mailServices;
        private readonly ITwilioRestClient twilioRestClient;
        public InjectionScheduleDetailServices(IRepository<InjectionScheduleDetail> repository, IInjectionScheduleServices injectionScheduleServices, IInjectionIncidentServices injectionIncidentServices, IRegulationInjectionServices regulationInjectionServices, ICustomerServices customerServices, IVaccineServices vaccineServices, IMailServices mailServices, ITwilioRestClient twilioRestClient)
        {
            this.repository = repository;
            this.injectionScheduleServices = injectionScheduleServices;
            this.injectionIncidentServices = injectionIncidentServices;
            this.regulationInjectionServices = regulationInjectionServices;
            this.customerServices = customerServices;
            this.vaccineServices = vaccineServices;
            this.mailServices = mailServices;
            this.twilioRestClient = twilioRestClient;
        }

        public async Task<bool> DeleteInjectionScheduleDetail(int id)
        {
            var injectionScheduleDetail = await repository.GetAsync(id);
            return await repository.DeleteFromTrash(injectionScheduleDetail);
        }

        public async Task<bool> DeleteInjectionScheduleDetailsByInjectionScheduleId(int injectionScheduleId)
        {
            var injectionScheduleDetails = repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                                                     .Where(x => x.InjectionScheduleId == injectionScheduleId);
            return await repository.DeleteFromTrashRange(injectionScheduleDetails);
        }

        public async Task<bool> DeleteInjectionScheduleDetailsRange(int[] ids)
        {
            var injectionScheduleDetails = new List<InjectionScheduleDetail>();
            foreach (var id in ids)
            {
                var injectionScheduleDetail = await repository.GetAsync(id);
                if (injectionScheduleDetail != null)
                {
                    injectionScheduleDetails.Add(injectionScheduleDetail);
                }
            }
            return await repository.DeleteFromTrashRange(injectionScheduleDetails);
        }

        public async Task<InjectionScheduleDetailModel> GetInjectionScheduleDetail(int id)
        {
            var models = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(vc => vc.Vaccine)
                .Include(vcp => vcp.VaccinePackage)
                .Include(ist => ist.InjectionStaff)
                .Select(injectionScheduleDetail => new InjectionScheduleDetailModel
                {
                    Id = injectionScheduleDetail.Id,
                    Address = injectionScheduleDetail.Address,
                    Amount = injectionScheduleDetail.Amount,
                    Injection = injectionScheduleDetail.Injection,
                    Injections = injectionScheduleDetail.Injections,
                    InjectionScheduleId = injectionScheduleDetail.InjectionScheduleId,
                    InjectionStaffId = injectionScheduleDetail.InjectionStaffId,
                    InjectionStaffName = injectionScheduleDetail.InjectionStaff.StaffName,
                    InjectionTime = injectionScheduleDetail.InjectionTime,
                    Pay = injectionScheduleDetail.Pay,
                    VaccineId = injectionScheduleDetail.VaccineId,
                    VaccineName = injectionScheduleDetail.Vaccine.Name,
                    VaccinePackageId = injectionScheduleDetail.VaccinePackageid,
                    VaccinePackageName = injectionScheduleDetail.VaccinePackage.Name,
                    Created = injectionScheduleDetail.Created,
                    ShipmentId = injectionScheduleDetail.ShipmentId,
                    ShipmentCode = injectionScheduleDetail.Shipment.ShipmentCode,
                    CheckReport = injectionIncidentServices
                                    .GetInjectionIncidents()
                                    .FirstOrDefault(x => x.InjectionScheduleId == injectionScheduleDetail.InjectionScheduleId
                                                    && x.VaccineId == injectionScheduleDetail.VaccineId
                                                    && x.ShipmentId == injectionScheduleDetail.ShipmentId
                                                    && x.InjectionTime.CompareTo(injectionScheduleDetail.InjectionTime ?? DateTime.Now) == 0
                                                    ) != null

                }).FirstOrDefaultAsync();
            return models;
        }

        public IQueryable<InjectionScheduleDetailModel> GetInjectionScheduleDetails(int injectionScheduleId)
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Where(x => x.InjectionScheduleId == injectionScheduleId)
                    .Include(vc => vc.Vaccine)
                    .Include(vcp => vcp.VaccinePackage)
                    .Include(ist => ist.InjectionStaff)
                    .Include(s => s.Shipment)
                    .Select(injectionScheduleDetail => new InjectionScheduleDetailModel
                    {
                        Id = injectionScheduleDetail.Id,
                        Address = injectionScheduleDetail.Address,
                        Amount = injectionScheduleDetail.Amount,
                        Injection = injectionScheduleDetail.Injection,
                        Injections = injectionScheduleDetail.Injections,
                        InjectionScheduleId = injectionScheduleDetail.InjectionScheduleId,
                        InjectionStaffId = injectionScheduleDetail.InjectionStaffId,
                        InjectionStaffName = injectionScheduleDetail.InjectionStaff.StaffName,
                        InjectionTime = injectionScheduleDetail.InjectionTime,
                        Pay = injectionScheduleDetail.Pay,
                        VaccineId = injectionScheduleDetail.VaccineId,
                        VaccineName = injectionScheduleDetail.Vaccine.Name,
                        VaccinePackageId = injectionScheduleDetail.VaccinePackageid,
                        VaccinePackageName = injectionScheduleDetail.VaccinePackage.Name,
                        Created = injectionScheduleDetail.Created,
                        ShipmentCode = injectionScheduleDetail.Shipment.ShipmentCode,
                        ShipmentId = injectionScheduleDetail.ShipmentId,
                        CheckReport = injectionIncidentServices
                                    .GetInjectionIncidents()
                                    .FirstOrDefault(x => x.InjectionScheduleId == injectionScheduleDetail.InjectionScheduleId
                                                    && x.VaccineId == injectionScheduleDetail.VaccineId
                                                    && x.ShipmentId == injectionScheduleDetail.ShipmentId
                                                    && x.InjectionTime.CompareTo(injectionScheduleDetail.InjectionTime ?? DateTime.Now) == 0) != null
                    });
        }

        public IQueryable<InjectionScheduleDetailModel> GetInjectionScheduleDetailsByCustomerId(int customerId)
        {
            return repository
            .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                     .Include(vc => vc.Vaccine)
                     .Include(vcp => vcp.VaccinePackage)
                     .Include(ist => ist.InjectionStaff)
                     .Include(s => s.Shipment)
                     .Include(ish => ish.InjectionSchedule)
                     .Where(x => x.InjectionSchedule.CustomerId == customerId)
                     .Select(injectionScheduleDetail => new InjectionScheduleDetailModel
                     {
                         Id = injectionScheduleDetail.Id,
                         Address = injectionScheduleDetail.Address,
                         Amount = injectionScheduleDetail.Amount,
                         Injection = injectionScheduleDetail.Injection,
                         Injections = injectionScheduleDetail.Injections,
                         InjectionScheduleId = injectionScheduleDetail.InjectionScheduleId,
                         InjectionStaffId = injectionScheduleDetail.InjectionStaffId,
                         InjectionStaffName = injectionScheduleDetail.InjectionStaff.StaffName,
                         InjectionTime = injectionScheduleDetail.InjectionTime,
                         Pay = injectionScheduleDetail.Pay,
                         VaccineId = injectionScheduleDetail.VaccineId,
                         VaccineName = injectionScheduleDetail.Vaccine.Name,
                         VaccinePackageId = injectionScheduleDetail.VaccinePackageid,
                         VaccinePackageName = injectionScheduleDetail.VaccinePackage.Name,
                         Created = injectionScheduleDetail.Created,
                         ShipmentCode = injectionScheduleDetail.Shipment.ShipmentCode,
                         ShipmentId = injectionScheduleDetail.ShipmentId,
                         ScheduleTime = injectionScheduleDetail.InjectionSchedule.Date,
                         CheckReport = injectionIncidentServices
                                     .GetInjectionIncidents()
                                     .FirstOrDefault(x => x.InjectionScheduleId == injectionScheduleDetail.InjectionScheduleId
                                                     && x.VaccineId == injectionScheduleDetail.VaccineId

                                                     && x.ShipmentId == injectionScheduleDetail.ShipmentId
                                                     && x.InjectionTime.CompareTo(injectionScheduleDetail.InjectionTime ?? DateTime.Now) == 0) != null
                     });
        }

        public async Task<bool> InsertInjectionScheduleDetail(InjectionScheduleDetailModel model)
        {
            var injectionScheduleDetail = new InjectionScheduleDetail
            {
                Address = model.Address,
                Amount = model.Amount,
                Injection = model.Injection,
                Injections = model.Injections,
                InjectionScheduleId = model.InjectionScheduleId,
                InjectionStaffId = model.InjectionStaffId,
                InjectionTime = model.InjectionTime,
                Pay = model.Pay,
                VaccineId = model.VaccineId,
                VaccinePackageid = model.VaccinePackageId,
                ShipmentId = model.ShipmentId
            };
            var result = await repository.InsertAsync(injectionScheduleDetail);
            model.Id = injectionScheduleDetail.Id;
            model.Created = injectionScheduleDetail.Created;
            return result;

        }

        public async Task<bool> InsertInjectionScheduleDetailsRange(IList<InjectionScheduleDetailModel> injectionScheduleDetailModels)
        {
            var injectionScheduleDetails = new List<InjectionScheduleDetail>();
            foreach (var injectionScheduleDetailModel in injectionScheduleDetailModels)
            {
                injectionScheduleDetails.Add(new InjectionScheduleDetail
                {
                    Address = injectionScheduleDetailModel.Address,
                    Amount = injectionScheduleDetailModel.Amount,
                    Injection = false,
                    Injections = injectionScheduleDetailModel.Injections,
                    InjectionScheduleId = injectionScheduleDetailModel.InjectionScheduleId,
                    InjectionStaffId = injectionScheduleDetailModel.InjectionStaffId,
                    Pay = injectionScheduleDetailModel.Pay,
                    VaccineId = injectionScheduleDetailModel.VaccineId,
                    VaccinePackageid = injectionScheduleDetailModel.VaccinePackageId,
                    ShipmentId = injectionScheduleDetailModel.ShipmentId
                });
            }
            var result = await repository.InsertRangeAsync(injectionScheduleDetails);
            for (int i = 0; i < injectionScheduleDetails.Count; i++)
            {
                injectionScheduleDetailModels[0].Id = injectionScheduleDetails[0].Id;
                injectionScheduleDetailModels[0].Created = injectionScheduleDetails[0].Created;
            }
            return result;
        }
        public void ReminderSchedule(Mail mail, SmsMessage sms)
        {
            mailServices.SendMail(mail);
            string phoneTo = sms.To[0] == '+' ? sms.To : "+84" + sms.To.Substring(1);
            MessageResource.Create(
                to: new PhoneNumber(phoneTo),
                from: new PhoneNumber(sms.From),
                body: sms.Message,
                client: twilioRestClient);
        }       

        public async Task<bool> UpdateInjectionInjectionScheduleDetail(int id)
        {

            var injectionScheduleDetail = await repository.GetAsync(id);
            var dateUpdate = DateTime.Now;
            injectionScheduleDetail.InjectionTime = dateUpdate;
            injectionScheduleDetail.Injection = true;
            var result = await repository.UpdateAsync(injectionScheduleDetail);
            if (result)
            {
                var temps = GetInjectionScheduleDetails(injectionScheduleDetail.InjectionScheduleId)
                          .Where(x => x.ShipmentId == injectionScheduleDetail.ShipmentId &&
                                    x.VaccineId == injectionScheduleDetail.VaccineId);
                if (temps.Count() != 0)
                {
                    var repeatInjection = temps.FirstOrDefault(x => injectionScheduleDetail.VaccinePackageid != null && x.VaccinePackageId == injectionScheduleDetail.VaccinePackageid && x.Injection == false);
                    var temp = repeatInjection != null ? repeatInjection : temps.First();
                    var regulationInjection = regulationInjectionServices.GetRegulationInjectionByVaccineId(temp.VaccineId ?? 0);
                    var injectionSchedule = injectionScheduleServices.GetInjectionScheduleAsync(injectionScheduleDetail.InjectionScheduleId);
                    var customer = await customerServices.GetCustomer(injectionSchedule.CustomerId ?? 0);
                    var vaccine = await vaccineServices.GetVaccine(injectionScheduleDetail.VaccineId ?? 0);
                    var dateContinue = dateUpdate.AddDays(regulationInjection.Distance);
                    string body, title;
                    body = title = string.Empty;
                    int day = 0;
                    if (temp.Injection == false)
                    {
                        body = $"Ngày {dateContinue.Day}/{dateContinue.Month}/{dateContinue.Year} bạn đến trung tâm tiêm mũi tiếp theo của vaccine {vaccine.Name}.";
                        title = $"Thông báo tiêm mũi tiếp theo vaccine {vaccine.Name}";
                        day = regulationInjection.Distance <= ReminderScheduleConfig.Day ? 0 : regulationInjection.Distance - ReminderScheduleConfig.Day;
                    }
                    else
                    if (regulationInjection.RepeatInjection == -1)
                    {
                        DateTime dateRepeat = dateUpdate.AddYears(1);
                        body = $"Ngày {dateRepeat.Day}/{dateRepeat.Month}/{dateRepeat.Year} bạn đến trung tâm tiêm nhắc lại của vaccine {vaccine.Name}.";
                        title = $"Thông báo tiêm nhắc lại vaccine {vaccine.Name}";
                        day = 365 - ReminderScheduleConfig.Day;
                    }
                    if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(title))
                        BackgroundJob.Schedule(() => ReminderSchedule(new Mail
                        {
                            Body = body,
                            Title = title,
                            MailTo = customer.Email
                        }, new SmsMessage
                        {
                            From = TwilioConfig.PhoneFrom,
                            To = customer.PhoneNumber,
                            Message = body
                        }), TimeSpan.FromDays(day));
                }
            }
            return result;
        }

        public async Task<bool> UpdatePayInjectionScheduleDetails(int[] ids)
        {
            var listInjectionScheduleDetail = new List<InjectionScheduleDetail>();
            foreach (var id in ids)
            {
                var injectionScheduleDetail = await repository.GetAsync(id);
                injectionScheduleDetail.Pay = true;
                listInjectionScheduleDetail.Add(injectionScheduleDetail);
            }
            return await repository.UpdateRangeAsync(listInjectionScheduleDetail);
        }

        public async Task<bool> UpdateAddressInjectionStaffInjectionScheduleDetail(int id, string address,int injectionStaffId)
        {
            var model = await repository.GetAsync(id);
            if (model == null)
                return false;
            model.Address = address;
            model.InjectionStaffId=injectionStaffId;
            return await repository.UpdateAsync(model);
        }
    }
}
