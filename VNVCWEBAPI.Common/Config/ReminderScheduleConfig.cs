using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Config
{
    public class ReminderScheduleConfig
    {

        public static int Day { get; set; }

        public static void ReminderScheduleConfigurationSettings(IConfiguration configuration)
        {
            int.TryParse(configuration["ReminderScheduleSettings:Day"] ?? "0", out int day);
            Day = day;
        }
    }
}
