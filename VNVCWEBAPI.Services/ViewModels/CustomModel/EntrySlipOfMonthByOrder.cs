﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class EntrySlipOfMonthByOrder
    {
        public int OrderId { get; set; }
        public double TotalOrder { get; set; }
        public double TotalEntrySlip { get; set; }
    }
}
