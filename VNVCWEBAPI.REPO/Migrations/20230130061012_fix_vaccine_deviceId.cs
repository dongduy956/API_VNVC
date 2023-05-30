using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VNVCWEBAPI.REPO.Migrations
{
    /// <inheritdoc />
    public partial class fixvaccinedeviceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max",
                table: "Promotions");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Vaccines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "LoginSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CustomerTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4159));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4175));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4176));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4397), new DateTime(2023, 1, 30, 13, 10, 11, 486, DateTimeKind.Local).AddTicks(4397) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "LoginSessions");

            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
        }
    }
}
