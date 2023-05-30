using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.DATA.Models;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class LoginModel : BaseModel
    {
        public string Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool isLock { get; set; }
        public bool isValidate { get; set; }
        public int? CustomerId { get; set; }
        [StringLength(6)]
        public string? Code { get; set; }
        public int? StaffId { get; set; }
        public string? CustomerName { get; set; }
        public string? StaffName { get; set; }
        public string? Avatar { get; set; }
    }
}
