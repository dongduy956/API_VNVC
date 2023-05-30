using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IPaymentMethodServices
    {
        IQueryable<PaymentMethodModel> GetPaymentMethods();
        IQueryable<PaymentMethodModel> SearchPaymentMethods(string q="");
        Task<PaymentMethodModel> GetPaymentMethod(int id);
        Task<bool> InsertPaymentMethod(PaymentMethodModel paymentMethodModel);
        Task<bool> InsertPaymentMethodsRange(IList<PaymentMethodModel> paymentMethodModels);

        Task<bool> UpdatePaymentMethod(int id, PaymentMethodModel paymentMethodModel);
        Task<bool> DeletePaymentMethod(int id);
    }
}
