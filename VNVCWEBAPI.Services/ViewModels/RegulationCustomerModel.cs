using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class RegulationCustomerModel : BaseModel
    {
        public int Amount { get; set; }
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public int? CustomerTypeId { get; set; }
        public string? CustomerTypeName { get; set; }
    }
}
