using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Login : BaseEntity
    {
        public Login()
        {
            Carts = new HashSet<Cart>();
        }
        [Column(TypeName ="varchar(50)")]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool isLock { get; set; }
        public bool isValidate { get; set; }
        public int? CustomerId { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? Code { get; set; }
        public int? StaffId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
