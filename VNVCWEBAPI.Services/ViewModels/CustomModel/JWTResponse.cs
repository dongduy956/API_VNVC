using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class JWTResponse : JWTRequest
    {
        public string AccessToken_Type { get; set; }
        public object User { get; set; }
        public int ExpiredTime { get; set; }
        public int? PermissionId { get; set; }
        public string? PermissionName { get; set; }
    }
}
