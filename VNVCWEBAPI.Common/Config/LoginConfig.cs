using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public static class LoginConfig
    {
        public static string DefaultPassword { get; set; }
        public static string ChatAPI { get; set; }

        public static void LoginConfigurationSettings(IConfiguration configuration)
        {
            DefaultPassword = configuration["LoginSettings:DefaultPassword"] ?? "";
            ChatAPI = configuration["LoginSettings:ChatAPI"] ?? "";
        }
    }
}
