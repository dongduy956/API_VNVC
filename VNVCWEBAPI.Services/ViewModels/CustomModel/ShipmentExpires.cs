﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class ShipmentExpires
    {
        public string Shipment { get; set; }
        public string Vaccine { get; set; }
        public DateTime Date { get; set; }
    }
}
