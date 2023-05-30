using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class CustomerRankDetailsModel : BaseModel
    {
        public int Point { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int PayId { get; set; }
    }
}
