using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class VaccineModel:BaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string DiseasePrevention { get; set; }
        public string InjectionSite { get; set; }
        public string SideEffects { get; set; }
        public int Amount { get; set; }
        public string Storage { get; set; }
        public string StorageTemperatures { get; set; }
        public int TypeOfVaccineId { get; set; }
        public string? TypeOfVaccineName { get; set; }
        public int QuantityRemain { get; set; }
        public string? Content { get; set; }
        public ICollection<VaccinePriceModel>? VaccinePrices { get; set; }
    }
}
