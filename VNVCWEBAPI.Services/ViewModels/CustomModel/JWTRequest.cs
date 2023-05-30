using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class JWTRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
