using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VNVCWEBAPI.REPO.Migrations
{
    /// <inheritdoc />
    public partial class fixCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Age", "Created", "Name", "isTrash" },
                values: new object[] { 1, 0, new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4745), "Khách hàng thông thường", false });

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

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Created", "Name", "isTrash" },
                values: new object[] { 3, new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4577), "User", false });

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4782), new DateTime(2023, 1, 21, 15, 35, 41, 974, DateTimeKind.Local).AddTicks(4783) });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_LoginId",
                table: "Cart",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Logins_LoginId",
                table: "Cart",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Logins_LoginId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_LoginId",
                table: "Cart");

            migrationBuilder.DeleteData(
                table: "CustomerTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Cart");

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(1007));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(974), new DateTime(2023, 1, 15, 11, 55, 32, 409, DateTimeKind.Local).AddTicks(976) });
        }
    }
}
