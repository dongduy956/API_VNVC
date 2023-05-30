using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VNVCWEBAPI.REPO.Migrations
{
    /// <inheritdoc />
    public partial class addDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Logins_LoginId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_VaccinePackages_PackageId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Vaccines_VaccineId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_EntrySlips_Supplier_SupplierId",
                table: "EntrySlips");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Supplier_SupplierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Supplier_SupplierId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_VaccineId",
                table: "Carts",
                newName: "IX_Carts_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_PackageId",
                table: "Carts",
                newName: "IX_Carts_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_LoginId",
                table: "Carts",
                newName: "IX_Carts_LoginId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    href = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CustomerTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 27, 3, 15, 35, 292, DateTimeKind.Local).AddTicks(64));

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 27, 3, 15, 35, 292, DateTimeKind.Local).AddTicks(137));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 27, 3, 15, 35, 291, DateTimeKind.Local).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 27, 3, 15, 35, 291, DateTimeKind.Local).AddTicks(9744));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 27, 3, 15, 35, 291, DateTimeKind.Local).AddTicks(9746));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 1, 27, 3, 15, 35, 292, DateTimeKind.Local).AddTicks(105), new DateTime(2023, 1, 27, 3, 15, 35, 292, DateTimeKind.Local).AddTicks(106) });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Logins_LoginId",
                table: "Carts",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_VaccinePackages_PackageId",
                table: "Carts",
                column: "PackageId",
                principalTable: "VaccinePackages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Vaccines_VaccineId",
                table: "Carts",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrySlips_Suppliers_SupplierId",
                table: "EntrySlips",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Suppliers_SupplierId",
                table: "Shipments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Logins_LoginId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_VaccinePackages_PackageId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Vaccines_VaccineId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_EntrySlips_Suppliers_SupplierId",
                table: "EntrySlips");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Suppliers_SupplierId",
                table: "Shipments");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_VaccineId",
                table: "Cart",
                newName: "IX_Cart_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_PackageId",
                table: "Cart",
                newName: "IX_Cart_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_LoginId",
                table: "Cart",
                newName: "IX_Cart_LoginId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CustomerTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4745));

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4806));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4561));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4575));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4577));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4782), new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4783) });

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Logins_LoginId",
                table: "Cart",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_VaccinePackages_PackageId",
                table: "Cart",
                column: "PackageId",
                principalTable: "VaccinePackages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Vaccines_VaccineId",
                table: "Cart",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrySlips_Supplier_SupplierId",
                table: "EntrySlips",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Supplier_SupplierId",
                table: "Orders",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Supplier_SupplierId",
                table: "Shipments",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
