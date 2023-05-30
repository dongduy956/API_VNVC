using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class CartModel : BaseModel
    {
        public int? VaccineId { get; set; }
        public int? PackageId { get; set; }
        public int? LoginId { get; set; }
        public VaccineModel? Vaccine { get; set; }
        public VaccinePackageModel? vaccinePackageModel { get; set; }
    }
}
