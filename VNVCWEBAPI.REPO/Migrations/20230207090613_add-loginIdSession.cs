using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VNVCWEBAPI.REPO.Migrations
{
    /// <inheritdoc />
    public partial class addloginIdSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "LoginSessions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CustomerTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3632));

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3394));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3408));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3679), new DateTime(2023, 2, 7, 16, 6, 12, 365, DateTimeKind.Local).AddTicks(3680) });

            migrationBuilder.CreateIndex(
                name: "IX_LoginSessions_LoginId",
                table: "LoginSessions",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginSessions_Logins_LoginId",
                table: "LoginSessions",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginSessions_Logins_LoginId",
                table: "LoginSessions");

            migrationBuilder.DropIndex(
                name: "IX_LoginSessions_LoginId",
                table: "LoginSessions");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "LoginSessions");

            migrationBuilder.UpdateData(
                table: "CustomerTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(6691));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(6715));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(6717));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "DateOfBirth" },
                values: new object[] { new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(7086), new DateTime(2023, 1, 30, 22, 51, 54, 123, DateTimeKind.Local).AddTicks(7088) });
        }
    }
}
