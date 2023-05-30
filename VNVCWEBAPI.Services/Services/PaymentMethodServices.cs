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
    public class PaymentMethodServices : IPaymentMethodServices
    {
        private readonly IRepository<PaymentMethod> repository;
        public PaymentMethodServices(IRepository<PaymentMethod> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeletePaymentMethod(int id)
        {
            var paymentMethod = await repository.GetAsync(id);
            return await repository.Delete(paymentMethod);
        }

        public async Task<PaymentMethodModel> GetPaymentMethod(int id)
        {
            var paymentMethod = await repository.GetAsync(id);
            return new PaymentMethodModel
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name,
                Created=paymentMethod.Created
            };
        }

        public IQueryable<PaymentMethodModel> GetPaymentMethods()
        {
            return repository
                    .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                    .Select(paymentMethod => new PaymentMethodModel
                    {
                        Id = paymentMethod.Id,
                        Name = paymentMethod.Name,
                        Created=paymentMethod.Created
                    });
        }

        public async Task<bool> InsertPaymentMethod(PaymentMethodModel paymentMethodModel)
        {
            var paymentMethod = new PaymentMethod
            {
                Name = paymentMethodModel.Name,
            };
            var result = await repository.InsertAsync(paymentMethod);
            paymentMethodModel.Id = paymentMethod.Id;
            paymentMethodModel.Created = paymentMethod.Created;
            return result;
        }

        public async Task<bool> InsertPaymentMethodsRange(IList<PaymentMethodModel> paymentMethodModels)
        {
            var paymentMethods = new List<PaymentMethod>();
            foreach (var paymentMethodModel in paymentMethodModels)
            {
                paymentMethods.Add(new PaymentMethod
                {
                    Name = paymentMethodModel.Name,
                    
                });
            }
            var result = await repository.InsertRangeAsync(paymentMethods);
            for (int i = 0; i < paymentMethods.Count; i++)
            {
                paymentMethodModels[i].Id = paymentMethods[i].Id;
                paymentMethodModels[i].Created = paymentMethods[i].Created;
            }
            return result;
        }

        public IQueryable<PaymentMethodModel> SearchPaymentMethods(string q = "")
        {
            q = q.Trim().ToLower();
            var results = repository
                .GetAll(Common.Enum.SelectEnum.Select.NONTRASH)
                .Where(x => x.Name.Trim().ToLower().Contains(q))
                .Select(model => new PaymentMethodModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Created=model.Created
                });
            return results;
        }

        public async Task<bool> UpdatePaymentMethod(int id, PaymentMethodModel paymentMethodModel)
        {
            var paymentMethod = await repository.GetAsync(id);

            paymentMethod.Name = paymentMethodModel.Name;
            return await repository.UpdateAsync(paymentMethod);
        }
    }
}
