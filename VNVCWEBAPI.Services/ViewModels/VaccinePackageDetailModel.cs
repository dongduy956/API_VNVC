using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class VaccinePackageDetailModel : BaseModel
    {
        public int OrderInjection { get; set; }
        public int NumberOfInjections { get; set; }
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public int VaccinePackageId { get; set; }
        public string? VaccinePackageName { get; set; }
        public int ShipmentId { get; set; }
        public string? ShipmentCode { get; set; }
        public bool isGeneral { get; set; }
    }
}
