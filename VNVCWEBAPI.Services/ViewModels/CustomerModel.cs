using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Services.ViewModels
{
    public class CustomerModel:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public bool Sex { get; set; }
        public string IdentityCard { get; set; }
        public string? Avatar { get; set; }
        public string? InsuranceCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }
        public int CustomerTypeId { get; set; }
        public string? CustomerTypeName { get; set; }
    }
}
