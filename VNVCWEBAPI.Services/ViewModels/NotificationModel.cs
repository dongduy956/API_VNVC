using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class NotificationModel : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public bool isSeen { get; set; }
        public int? LoginId { get; set; }
    }
}
