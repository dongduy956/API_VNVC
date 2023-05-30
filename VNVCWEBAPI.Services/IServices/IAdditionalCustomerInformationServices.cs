using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.REPO.Repository;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IAdditionalCustomerInformationServices
    {
        IQueryable<AdditionalCustomerInformationModel> GetAdditionalCustomerInformations();
        IList<AdditionalCustomerInformationModel> GetAdditionalCustomerInformationByIds(int[] ids);
        IQueryable<AdditionalCustomerInformationModel> SearchAdditionalCustomerInformations(string q="");
        Task<AdditionalCustomerInformationModel> GetAdditionalCustomerInformationAsync(int id);
        Task<bool> InsertAdditionalCustomerInformationAsync(AdditionalCustomerInformationModel model);
        Task<bool> InsertAdditionalCustomerInformationsRange(IList<AdditionalCustomerInformationModel> additionalCustomerInformationModels);
        Task<bool> UpdateAdditionalCustomerInformationsRange(IList<AdditionalCustomerInformationModel> additionalCustomerInformationModels);
        Task<bool> UpdateAdditionalCustomerInformationAsync(int id,AdditionalCustomerInformationModel model);
        Task<bool> DeleteAdditionalCustomerInformationAsync(int customerId);
    }
}
