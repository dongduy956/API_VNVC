using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public class MailConfig
    {
        public static string Mail { get; set; }
        public static string Pass { get; set; }

        public static void MailConfigurationSettings(IConfiguration configuration)
        {
            Mail = configuration["MailSettings:Mail"] ?? "";
            Pass = configuration["MailSettings:Pass"] ?? "";

        }
    }
}
