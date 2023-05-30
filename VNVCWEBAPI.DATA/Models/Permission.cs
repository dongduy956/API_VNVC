using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Permission : BaseEntity
    {
        public Permission()
        {
            Staff = new HashSet<Staff>();
        }
        public string Name { get; set; }
        public ICollection<PermissionDetails> PermissionDetails { get; set; }
        public ICollection<Staff> Staff { get; set; }
    }
}
