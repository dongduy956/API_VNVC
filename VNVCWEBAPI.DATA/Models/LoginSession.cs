using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class LoginSession : BaseEntity
    {
        public string TokenId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string? IPAddress { get; set; }
        public DateTime Expired { get; set; }
        public string? DeviceId { get; set; }
        public bool isRevoked { get; set; }
        [NotMapped]
        public bool isExpired
        {
            get
            {
                return DateTime.UtcNow > Expired;
            }
        }
       
        public int? LoginId { get; set; }
        [ForeignKey("LoginId")]
        public Login? Login { get; set; }
        public int? CustomerId { get; set; }
        public int? StaffId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}
