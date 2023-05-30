using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Constraint;
using VNVCWEBAPI.Common.Library;
using VNVCWEBAPI.DATA.Models;

namespace VNVCWEBAPI.REPO
{
    public class VNVCContext : DbContext
    {
        public VNVCContext(DbContextOptions<VNVCContext> options) : base(options)
        {

        }

        #region "DB SET"
        public DbSet<AdditionalCustomerInformation> AdditionalCustomerInformation { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ConditionPromotion> ConditionPromotions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRank> CustomerRanks { get; set; }
        public DbSet<CustomerRankDetails> CustomerRankDetails { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<EntrySlip> EntrySlips { get; set; }
        public DbSet<EntrySlipDetails> EntrySlipDetails { get; set; }
        public DbSet<InjectionIncident> InjectionIncidents { get; set; }
        public DbSet<InjectionSchedule> InjectionSchedules { get; set; }
        public DbSet<InjectionScheduleDetail> InjectionScheduleDetails { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<LoginSession> LoginSessions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<PayDetail> PayDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionDetails> PermissionDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<RegulationCustomer> RegulationCustomers { get; set; }
        public DbSet<RegulationInjection> RegulationInjections { get; set; }
        public DbSet<ScreeningExamination> ScreeningExaminations { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TypeOfVaccine> TypeOfVaccines { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccinePackage> VaccinePackages { get; set; }
        public DbSet<VaccinePackageDetails> VaccinePackageDetails { get; set; }
        public DbSet<VaccinePrice> VaccinePrices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        #endregion "DB SET"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Set Foreign Key
            RegisterMultipleForeginKey(modelBuilder);
            //Set unique
            setUniqueDb(modelBuilder);
            //Seed Data
            SeedData(modelBuilder);
        }

        #region "Config Function"
        //Load Unique function
        private void setUniqueDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasIndex(c => c.InsuranceCode).IsUnique();
            modelBuilder.Entity<CustomerType>().HasIndex(cp => cp.Name).IsUnique();
            modelBuilder.Entity<Permission>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<TypeOfVaccine>().HasIndex(p => p.Name).IsUnique();
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            //Seed Permission
            var SuperAdmin = new Permission
            {
                Id = 1,
                Name = DefaultRoles.SuperAdmin,
                Created = DateTime.Now
            };

            var Admin = new Permission
            {
                Id = 2,
                Name = DefaultRoles.Admin,
                Created = DateTime.Now
            };
            var User = new Permission
            {
                Id = 3,
                Name = DefaultRoles.User,
                Created = DateTime.Now
            };



            modelBuilder.Entity<Permission>().HasData(SuperAdmin, Admin, User);
            var NormalCustomer = new CustomerType
            {
                Age = 0,
                Created = DateTime.Now,
                Id = 1,
                isTrash = false,
                Name = "Khách hàng thông thường",
            };
            modelBuilder.Entity<CustomerType>().HasData(NormalCustomer);
            //Seed staff
            var staff = new Staff
            {
                Id = 1,
                Avatar = "",
                Province = "",
                Village = "",
                IdentityCard = "",
                Country = "Vietnam",
                PermissionId = 1,
                Sex = true,
                StaffName = "Super Admin",
                Created = DateTime.Now,
                DateOfBirth = DateTime.Now,
                District = "VNVC",
                Address = "VNVC",
            };
            modelBuilder.Entity<Staff>().HasData(staff);
            //Seed Account
            var login = new Login
            {
                Id = 1,
                Created = DateTime.Now,
                StaffId = 1,
                isValidate = true,
                Username = "admin",
                PasswordHash = StringLibrary.PasswordHash("admin")
            };
            modelBuilder.Entity<Login>().HasData(login);


        }

        private void RegisterMultipleForeginKey(ModelBuilder modelBuilder)
        {
            //Multiple Foreign key Staff
            modelBuilder.Entity<Staff>()
                .HasMany(st => st.InjectionSchedules)
                .WithOne(x => x.Staff)
                .HasForeignKey(f => f.StaffId);

            modelBuilder.Entity<Staff>()
                .HasMany(st => st.Nominators)
                .WithOne(x => x.Nominator)
                .HasForeignKey(f => f.NominatorId);

            modelBuilder.Entity<Staff>()
                .HasMany(st => st.Updaters)
                .WithOne(x => x.Updater)
                .HasForeignKey(f => f.UpdaterId);
        }
        #endregion
    }
}
