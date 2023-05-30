using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public static class FirebaseSettings
    {
        public static string ApplicationId { get; set; }
        public static string SenderId { get; set; }
        public static void FirebaseConfigurationSettings(IConfiguration configuration)
        {
            ApplicationId = configuration["FirebaseSettings:applicationId"] ?? "";
            SenderId = configuration["FirebaseSettings:senderId"] ?? "";
        }
    }
}
