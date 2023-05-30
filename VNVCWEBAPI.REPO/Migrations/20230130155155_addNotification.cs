using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VNVCWEBAPI.REPO.Migrations
{
    /// <inheritdoc />
    public partial class addNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isSeen = table.Column<bool>(type: "bit", nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: true),
                    isTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LoginId",
                table: "Notifications",
                column: "LoginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

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
    }
}
