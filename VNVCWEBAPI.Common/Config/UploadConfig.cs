using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public static class UploadConfig
    {
        public static string Images { get; set; }
        public static void UploadConfigurationSettings(IConfiguration configuration)
        {
            Images = configuration["UploadSettings:Image"] ?? "";
        }
    }
}
