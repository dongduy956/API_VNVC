using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public static class JWTConfig
    {
        public static string SecretKey { get; set; }
        public static int AccessTokenTime { get; set; }
        public static int RefreshTokenTime { get; set; }
        public static void JWTConfigurationSettings(IConfiguration configuration)
        {
            SecretKey = configuration["JWTSettings:SecretKey"] ?? "";
            AccessTokenTime = Convert.ToInt32(configuration["JWTSettings:AccesstokenTime"] ?? "1");
            RefreshTokenTime = Convert.ToInt32(configuration["JWTSettings:RefreshTokenTime"] ?? "1");
        }
    }
}
