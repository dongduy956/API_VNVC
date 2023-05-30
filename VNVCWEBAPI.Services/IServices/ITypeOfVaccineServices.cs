using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface ITypeOfVaccineServices
    {
        IQueryable<TypeOfVaccineModel> GetTypeOfVaccines();
        IQueryable<TypeOfVaccineModel> SearchTypeOfVaccines(string q="");
        Task<TypeOfVaccineModel> GetTypeOfVaccine(int id);
        Task<bool> InsertTypeOfVaccine(TypeOfVaccineModel typeOfVaccineModel);
        Task<bool> InsertTypeOfVaccinesRange(IList<TypeOfVaccineModel> typeOfVaccineModels);
        Task<bool> UpdateTypeOfVaccine(int id, TypeOfVaccineModel typeOfVaccineModel);
        Task<bool> DeleteTypeOfVaccine(int id);
    }
}
