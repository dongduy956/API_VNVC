using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class BannerModel : BaseModel
    {
        public string Image { get; set; }
        public string? Title { get; set; }
        public string? Href { get; set; }
    }
}
