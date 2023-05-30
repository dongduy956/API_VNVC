using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNVCWEBAPI.REPO.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfVaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfVaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccinePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectInjection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinePackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityCard = table.Column<string>(type: "varchar(25)", nullable: true),
                    InsuranceCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerTypeId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionDetails_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityCard = table.Column<string>(type: "varchar(25)", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(200)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Village = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", nullable: true),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiseasePrevention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InjectionSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SideEffects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Storage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageTemperatures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfVaccineId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccines_TypeOfVaccines_TypeOfVaccineId",
                        column: x => x.TypeOfVaccineId,
                        principalTable: "TypeOfVaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalCustomerInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightAtBirth = table.Column<double>(type: "float", nullable: false),
                    HeightAtBirth = table.Column<double>(type: "float", nullable: false),
                    FatherFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FatherPhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    MotherFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MotherPhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCustomerInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalCustomerInformation_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InjectionSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NominatorId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priorities = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InjectionSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InjectionSchedules_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InjectionSchedules_Staff_NominatorId",
                        column: x => x.NominatorId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InjectionSchedules_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InjectionSchedules_Staff_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isLock = table.Column<bool>(type: "bit", nullable: false),
                    isValidate = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "varchar(6)", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Logins_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoginSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginSessions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoginSessions_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Heartbeat = table.Column<double>(type: "float", nullable: false),
                    BloodPressure = table.Column<double>(type: "float", nullable: false),
                    isEligible = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningExaminations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreeningExaminations_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_VaccinePackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "VaccinePackages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cart_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConditionPromotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageVaccineId = table.Column<int>(type: "int", nullable: true),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    CustomerRankId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionPromotions_CustomerRanks_CustomerRankId",
                        column: x => x.CustomerRankId,
                        principalTable: "CustomerRanks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConditionPromotions_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConditionPromotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConditionPromotions_VaccinePackages_PackageVaccineId",
                        column: x => x.PackageVaccineId,
                        principalTable: "VaccinePackages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConditionPromotions_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegulationCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    CustomerTypeId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegulationCustomers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegulationCustomers_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegulationInjections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    RepeatInjection = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationInjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegulationInjections_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    InjectionScheduleId = table.Column<int>(type: "int", nullable: false),
                    Payer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    GuestMoney = table.Column<double>(type: "float", nullable: false),
                    ExcessMoney = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pays_InjectionSchedules_InjectionScheduleId",
                        column: x => x.InjectionScheduleId,
                        principalTable: "InjectionSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pays_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pays_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntrySlips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntrySlips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntrySlips_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntrySlips_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntrySlips_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InjectionIncidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InjectionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InjectionScheduleId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InjectionIncidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InjectionIncidents_InjectionSchedules_InjectionScheduleId",
                        column: x => x.InjectionScheduleId,
                        principalTable: "InjectionSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InjectionIncidents_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InjectionIncidents_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InjectionScheduleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Injections = table.Column<int>(type: "int", nullable: false),
                    InjectionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Injection = table.Column<bool>(type: "bit", nullable: false),
                    Pay = table.Column<bool>(type: "bit", nullable: false),
                    InjectionStaffId = table.Column<int>(type: "int", nullable: true),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    InjectionScheduleId = table.Column<int>(type: "int", nullable: false),
                    VaccinePackageid = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InjectionScheduleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InjectionScheduleDetails_InjectionSchedules_InjectionScheduleId",
                        column: x => x.InjectionScheduleId,
                        principalTable: "InjectionSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InjectionScheduleDetails_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InjectionScheduleDetails_Staff_InjectionStaffId",
                        column: x => x.InjectionStaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InjectionScheduleDetails_VaccinePackages_VaccinePackageid",
                        column: x => x.VaccinePackageid,
                        principalTable: "VaccinePackages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InjectionScheduleDetails_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ShipmentId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VaccinePackageDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderInjection = table.Column<int>(type: "int", nullable: false),
                    NumberOfInjections = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    VaccinePackageId = table.Column<int>(type: "int", nullable: false),
                    isGeneral = table.Column<bool>(type: "bit", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinePackageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinePackageDetails_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinePackageDetails_VaccinePackages_VaccinePackageId",
                        column: x => x.VaccinePackageId,
                        principalTable: "VaccinePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinePackageDetails_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VaccinePrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    EntryPrice = table.Column<double>(type: "float", nullable: false),
                    RetailPrice = table.Column<double>(type: "float", nullable: false),
                    PreOderPrice = table.Column<double>(type: "float", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinePrices_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinePrices_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerRankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PayId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRankDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerRankDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRankDetails_Pays_PayId",
                        column: x => x.PayId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    DiscountPackage = table.Column<double>(type: "float", nullable: true),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    VaccinePackageId = table.Column<int>(type: "int", nullable: true),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    PayId = table.Column<int>(type: "int", nullable: false),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayDetails_Pays_PayId",
                        column: x => x.PayId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayDetails_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayDetails_VaccinePackages_VaccinePackageId",
                        column: x => x.VaccinePackageId,
                        principalTable: "VaccinePackages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PayDetails_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntrySlipDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntrySlipId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    ShipmentID = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntrySlipDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntrySlipDetails_EntrySlips_EntrySlipId",
                        column: x => x.EntrySlipId,
                        principalTable: "EntrySlips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntrySlipDetails_Shipments_ShipmentID",
                        column: x => x.ShipmentID,
                        principalTable: "Shipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntrySlipDetails_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Created", "Name", "isTrash" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(715), "SuperAdmin", false },
                    { 2, new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(731), "Admin", false }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "Avatar", "Country", "Created", "DateOfBirth", "District", "Email", "IdentityCard", "PermissionId", "PhoneNumber", "Province", "Sex", "StaffName", "Village", "isTrash" },
                values: new object[] { 1, "VNVC", "", "Vietnam", new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(974), new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(976), "VNVC", null, "", 1, null, "", true, "Super Admin", "", false });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "Code", "Created", "CustomerId", "PasswordHash", "StaffId", "Username", "isLock", "isTrash", "isValidate" },
                values: new object[] { 1, null, new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(1007), null, "21232f297a57a5a743894a0e4a801fc3", 1, "admin", false, false, true });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalCustomerInformation_CustomerId",
                table: "AdditionalCustomerInformation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_PackageId",
                table: "Cart",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_VaccineId",
                table: "Cart",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionPromotions_CustomerRankId",
                table: "ConditionPromotions",
                column: "CustomerRankId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionPromotions_PackageVaccineId",
                table: "ConditionPromotions",
                column: "PackageVaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionPromotions_PaymentMethodId",
                table: "ConditionPromotions",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionPromotions_PromotionId",
                table: "ConditionPromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionPromotions_VaccineId",
                table: "ConditionPromotions",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRankDetails_CustomerId",
                table: "CustomerRankDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRankDetails_PayId",
                table: "CustomerRankDetails",
                column: "PayId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_InsuranceCode",
                table: "Customers",
                column: "InsuranceCode",
                unique: true,
                filter: "[InsuranceCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_Name",
                table: "CustomerTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntrySlipDetails_EntrySlipId",
                table: "EntrySlipDetails",
                column: "EntrySlipId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrySlipDetails_ShipmentID",
                table: "EntrySlipDetails",
                column: "ShipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EntrySlipDetails_VaccineId",
                table: "EntrySlipDetails",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrySlips_OrderId",
                table: "EntrySlips",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrySlips_StaffId",
                table: "EntrySlips",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrySlips_SupplierId",
                table: "EntrySlips",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionIncidents_InjectionScheduleId",
                table: "InjectionIncidents",
                column: "InjectionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionIncidents_ShipmentId",
                table: "InjectionIncidents",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionIncidents_VaccineId",
                table: "InjectionIncidents",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionScheduleDetails_InjectionScheduleId",
                table: "InjectionScheduleDetails",
                column: "InjectionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionScheduleDetails_InjectionStaffId",
                table: "InjectionScheduleDetails",
                column: "InjectionStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionScheduleDetails_ShipmentId",
                table: "InjectionScheduleDetails",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionScheduleDetails_VaccineId",
                table: "InjectionScheduleDetails",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionScheduleDetails_VaccinePackageid",
                table: "InjectionScheduleDetails",
                column: "VaccinePackageid");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionSchedules_CustomerId",
                table: "InjectionSchedules",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionSchedules_NominatorId",
                table: "InjectionSchedules",
                column: "NominatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionSchedules_StaffId",
                table: "InjectionSchedules",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_InjectionSchedules_UpdaterId",
                table: "InjectionSchedules",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerId",
                table: "Logins",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_StaffId",
                table: "Logins",
                column: "StaffId",
                unique: true,
                filter: "[StaffId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LoginSessions_CustomerId",
                table: "LoginSessions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginSessions_StaffId",
                table: "LoginSessions",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ShipmentId",
                table: "OrderDetails",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_VaccineId",
                table: "OrderDetails",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffId",
                table: "Orders",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PayDetails_PayId",
                table: "PayDetails",
                column: "PayId");

            migrationBuilder.CreateIndex(
                name: "IX_PayDetails_ShipmentId",
                table: "PayDetails",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayDetails_VaccineId",
                table: "PayDetails",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_PayDetails_VaccinePackageId",
                table: "PayDetails",
                column: "VaccinePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_InjectionScheduleId",
                table: "Pays",
                column: "InjectionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_PaymentMethodId",
                table: "Pays",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_StaffId",
                table: "Pays",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDetails_PermissionId",
                table: "PermissionDetails",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegulationCustomers_CustomerTypeId",
                table: "RegulationCustomers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationCustomers_VaccineId",
                table: "RegulationCustomers",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationInjections_VaccineId",
                table: "RegulationInjections",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningExaminations_CustomerId",
                table: "ScreeningExaminations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningExaminations_StaffId",
                table: "ScreeningExaminations",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SupplierId",
                table: "Shipments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_VaccineId",
                table: "Shipments",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PermissionId",
                table: "Staff",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfVaccines_Name",
                table: "TypeOfVaccines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePackageDetails_ShipmentId",
                table: "VaccinePackageDetails",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePackageDetails_VaccineId",
                table: "VaccinePackageDetails",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePackageDetails_VaccinePackageId",
                table: "VaccinePackageDetails",
                column: "VaccinePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePrices_ShipmentId",
                table: "VaccinePrices",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePrices_VaccineId",
                table: "VaccinePrices",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_TypeOfVaccineId",
                table: "Vaccines",
                column: "TypeOfVaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalCustomerInformation");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "ConditionPromotions");

            migrationBuilder.DropTable(
                name: "CustomerRankDetails");

            migrationBuilder.DropTable(
                name: "EntrySlipDetails");

            migrationBuilder.DropTable(
                name: "InjectionIncidents");

            migrationBuilder.DropTable(
                name: "InjectionScheduleDetails");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "LoginSessions");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PayDetails");

            migrationBuilder.DropTable(
                name: "PermissionDetails");

            migrationBuilder.DropTable(
                name: "RegulationCustomers");

            migrationBuilder.DropTable(
                name: "RegulationInjections");

            migrationBuilder.DropTable(
                name: "ScreeningExaminations");

            migrationBuilder.DropTable(
                name: "VaccinePackageDetails");

            migrationBuilder.DropTable(
                name: "VaccinePrices");

            migrationBuilder.DropTable(
                name: "CustomerRanks");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "EntrySlips");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "VaccinePackages");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "InjectionSchedules");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "TypeOfVaccines");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
