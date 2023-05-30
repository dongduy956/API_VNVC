using Azure;
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
using VNVCWEBAPI.Services.ViewModels.CustomModel;

namespace VNVCWEBAPI.Services.Services
{
    public class PayServices : IPayServices
    {
        private readonly IRepository<Pay> repository;
        public PayServices(IRepository<Pay> repository)
        {
            this.repository = repository;
        }


        public async Task<PayModel> GetPay(int id)
        {
            var pay = await repository
                .GetAll(Common.Enum.SelectEnum.Select.ALL)
                .Include(pm => pm.PaymentMethod)
                .Include(st => st.Staff)
                .Include(pd => pd.PayDetails)
                .Include(x => x.InjectionSchedule)
                    .ThenInclude(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return new PayModel
            {
                Id = pay.Id,
                ExcessMoney = pay.ExcessMoney,
                GuestMoney = pay.GuestMoney,
                InjectionScheduleId = pay.InjectionScheduleId,
                Payer = pay.Payer,
                PaymentMethodId = pay.PaymentMethodId,
                StaffId = pay.StaffId,
                PaymentMethodName = pay.PaymentMethod.Name,
                StaffName = pay.Staff.StaffName,
                Discount = pay.Discount,
                Total = pay.Total,
                Created = pay.Created,
                CustomerName = pay.InjectionSchedule.Customer.FirstName + ' ' + pay.InjectionSchedule.Customer.LastName
            };
        }
        public void get()
        {
            //repository.GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
            //    .Include(x=>x.InjectionSchedule)
            //    .OrderByDescending(x=>x.)
        }
        public PayModel? GetPayByInjectionScheduleId(int injectionScheduleId)
        {
            return GetPays()
                    .FirstOrDefault(x => x.InjectionScheduleId == injectionScheduleId);
        }

        public IQueryable<PayModel> GetPays()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Include(pm => pm.PaymentMethod)
                    .Include(st => st.Staff)
                    .Include(pd => pd.PayDetails)
                    .Include(x => x.InjectionSchedule)
                        .ThenInclude(x => x.Customer)
                    .Select(pay => new PayModel
                    {
                        Id = pay.Id,
                        ExcessMoney = pay.ExcessMoney,
                        GuestMoney = pay.GuestMoney,
                        InjectionScheduleId = pay.InjectionScheduleId,
                        Payer = pay.Payer,
                        PaymentMethodId = pay.PaymentMethodId,
                        StaffId = pay.StaffId,
                        PaymentMethodName = pay.PaymentMethod.Name,
                        StaffName = pay.Staff.StaffName,
                        Discount = pay.Discount,
                        Total = pay.Total,
                        Created = pay.Created,
                        CustomerName = pay.InjectionSchedule.Customer.FirstName + ' ' + pay.InjectionSchedule.Customer.LastName
                    });
        }

        public async Task<bool> InsertPay(PayModel payModel)
        {
            var pay = new Pay
            {
                ExcessMoney = payModel.ExcessMoney,
                GuestMoney = payModel.GuestMoney,
                InjectionScheduleId = payModel.InjectionScheduleId,
                Payer = payModel.Payer,
                PaymentMethodId = payModel.PaymentMethodId,
                StaffId = payModel.StaffId,
                Discount = payModel.Discount,

            };
            var result = await repository.InsertAsync(pay);
            payModel.Id = pay.Id;
            payModel.Created = pay.Created;
            return result;
        }

        public IQueryable<PayModel> SearchPays(string q = "")
        {
            q = q.Trim().ToLower();

            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Include(pm => pm.PaymentMethod)
                .Include(st => st.Staff)
                 .Include(pd => pd.PayDetails)
                 .Include(x => x.InjectionSchedule)
                    .ThenInclude(x => x.Customer)
                .Where(x => x.ExcessMoney.ToString().Equals(q) ||
                    x.GuestMoney.ToString().Equals(q) ||
                    x.InjectionScheduleId.Equals(q) ||
                    x.Payer.ToLower().Contains(q) ||
                    x.PaymentMethod.Name.ToLower().Contains(q) ||
                    x.Staff.StaffName.ToLower().Contains(q))
                .Select(pay => new PayModel
                {
                    Id = pay.Id,
                    ExcessMoney = pay.ExcessMoney,
                    GuestMoney = pay.GuestMoney,
                    InjectionScheduleId = pay.InjectionScheduleId,
                    Payer = pay.Payer,
                    PaymentMethodId = pay.PaymentMethodId,
                    StaffId = pay.StaffId,
                    PaymentMethodName = pay.PaymentMethod.Name,
                    StaffName = pay.Staff.StaffName,
                    Discount = pay.Discount,
                    Total = pay.Total,
                    Created = pay.Created,
                    CustomerName = pay.InjectionSchedule.Customer.FirstName + ' ' + pay.InjectionSchedule.Customer.LastName


                });
            return results;
        }
    }
}
