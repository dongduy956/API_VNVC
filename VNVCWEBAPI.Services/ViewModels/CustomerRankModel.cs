using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class CustomerRankModel : BaseModel
    {
        public string Name { get; set; }
        public int Point { get; set; }
    }
}
