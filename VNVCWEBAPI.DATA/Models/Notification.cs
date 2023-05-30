using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public bool isSeen { get; set; }
        public int? LoginId { get; set; }
        [ForeignKey("LoginId")]
        public Login? Login { get; set; }
    }
}
