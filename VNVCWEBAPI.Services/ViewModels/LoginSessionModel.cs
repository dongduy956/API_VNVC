using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class LoginSessionModel : BaseModel
    {
        public string TokenId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string? IPAddress { get; set; }
        public DateTime Expired { get; set; }
        public bool isRevoked { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? StaffId { get; set; }
        public int? LoginId { get; set; }
        public string? StaffName { get; set; }
        public string? DeviceId { get; set; }
        public bool isExpired
        {
            get
            {
                return DateTime.UtcNow > Expired;
            }
        }
    }
}
