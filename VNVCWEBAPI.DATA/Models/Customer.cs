using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            AdditionalCustomerInformation = new HashSet<AdditionalCustomerInformation>();
            LoginSessions = new HashSet<LoginSession>();
            SreeningExaminations = new HashSet<ScreeningExamination>();
            InjectionSchedules = new HashSet<InjectionSchedule>();
            CustomerRankDetails = new HashSet<CustomerRankDetails>();
            //Customers = new HashSet<Customer>();
        }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public string? Email { get; set; }
        public bool Sex { get; set; }
        public string? Avatar { get; set; }
        [Column(TypeName = ("varchar(25)"))]
        public string? IdentityCard { get; set; }
        [Column(TypeName = ("varchar(100)"))]
        public string? InsuranceCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Province { get; set; }
        [StringLength(50)]
        public string District { get; set; }
        [StringLength(50)]
        public string Village { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [Column(TypeName = "varchar(11)")]
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        //public string? relationship { get; set; }

        //public int? CustomerId { get; set; }
        //[ForeignKey("CustomerId")]
        //public Customer? CustomerLink { get; set; }
        //public ICollection<Customer> Customers { get; set; }
        public int CustomerTypeId { get; set; }
        [ForeignKey("CustomerTypeId")]
        public CustomerType? CustomerType { get; set; }
        public ICollection<AdditionalCustomerInformation> AdditionalCustomerInformation { get; set; }
        public ICollection<LoginSession> LoginSessions { get; set; }
        public ICollection<ScreeningExamination> SreeningExaminations { get; set; }
        public ICollection<InjectionSchedule> InjectionSchedules { get; set; }
        public ICollection<CustomerRankDetails> CustomerRankDetails { get; set; }
        public Login Login { get; set; }
    }
}
