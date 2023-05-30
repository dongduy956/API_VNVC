using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class PermissionDetailsModel : BaseModel
    {
        public string PermissionType { get; set; }
        public string PermissionValue { get; set; }
        public int PermissionId { get; set; }
    }
}
