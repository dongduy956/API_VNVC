﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Banner : BaseEntity
    {
        public string Image { get; set; }
        public string? Title { get; set; }
        public string? href { get; set; }
    }
}
