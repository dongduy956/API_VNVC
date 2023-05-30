using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class StaffModel:BaseModel
    {
        public string StaffName { get; set; }
        public string? Email { get; set; }
        public bool Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdentityCard { get; set; }
        public string Avatar { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
    }
}
