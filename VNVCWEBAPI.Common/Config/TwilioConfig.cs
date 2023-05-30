using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public class TwilioConfig
    {
        public static string AccountSid { get; set; }
        public static string AuthToken { get; set; }
        public static string PhoneFrom { get; set; }


        public static void TwillioConfigurationSettings(IConfiguration configuration)
        {
            AccountSid = configuration["TwilioSettings:AccountSid"] ?? "";
            AuthToken = configuration["TwilioSettings:AuthToken"] ?? "";
            PhoneFrom = configuration["TwilioSettings:PhoneFrom"] ?? "";
        }
    }
}
