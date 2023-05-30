using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class RegulationInjectionModel : BaseModel
    {
        public int Distance { get; set; }
        public int Order { get; set; }
        public int RepeatInjection { get; set; }
        public int VaccineId { get; set; }
        public string? VaccineName { get; set; }
    }
}
