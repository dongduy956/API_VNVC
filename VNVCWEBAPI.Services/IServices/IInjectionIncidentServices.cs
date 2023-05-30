using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Services.ViewModels;

namespace VNVCWEBAPI.Services.IServices
{
    public interface IInjectionIncidentServices
    {
        IQueryable<InjectionIncidentModel> GetInjectionIncidents();
        Task<InjectionIncidentModel> GetInjectionIncidentAsync(int id);
        IQueryable<InjectionIncidentModel> SearchInjectionIncidents(string q="");
        Task<bool> InsertInjectionIncidentAsync(InjectionIncidentModel model);
      
    }
}
