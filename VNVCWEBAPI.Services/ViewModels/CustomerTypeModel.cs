using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class CustomerTypeModel:BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
