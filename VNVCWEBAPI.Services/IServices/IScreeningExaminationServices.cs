using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IScreeningExaminationServices
    {
        IQueryable<ScreeningExaminationModel> GetScreeningExaminations();
        IQueryable<ScreeningExaminationModel> SearchScreeningExaminations(string q="");
        Task<ScreeningExaminationModel?> GetScreeningExamination(int id);
        Task<bool> InsertScreeningExamination(ScreeningExaminationModel screeningExaminationModel);
        Task<bool> UpdateScreeningExamination(int id, ScreeningExaminationModel screeningExaminationModel);
        Task<bool> DeleteScreeningExamination(int id);
        Task<bool> DeleteScreeningExaminationsRange(int[] ids);
    }
}
