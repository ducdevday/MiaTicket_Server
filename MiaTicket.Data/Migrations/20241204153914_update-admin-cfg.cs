using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateadmincfg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("53793728-a273-4e8a-8e74-ed4fd1b67460"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsPasswordTemporary",
                table: "Admin",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Account", "Password" },
                values: new object[] { new Guid("1fe519fd-f9d8-4e95-a4fa-a1a2615df2c0"), "admin", new byte[] { 36, 50, 97, 36, 49, 49, 36, 66, 80, 111, 57, 87, 66, 121, 73, 113, 112, 67, 111, 68, 50, 77, 88, 101, 79, 79, 120, 53, 101, 90, 46, 74, 120, 104, 98, 115, 100, 77, 56, 116, 112, 108, 76, 101, 51, 89, 80, 76, 70, 49, 78, 76, 120, 57, 90, 84, 100, 76, 104, 54 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("1fe519fd-f9d8-4e95-a4fa-a1a2615df2c0"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsPasswordTemporary",
                table: "Admin",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Account", "IsPasswordTemporary", "Password" },
                values: new object[] { new Guid("53793728-a273-4e8a-8e74-ed4fd1b67460"), "admin", false, new byte[] { 36, 50, 97, 36, 49, 49, 36, 54, 49, 104, 105, 119, 108, 81, 97, 68, 97, 86, 118, 122, 78, 65, 49, 78, 82, 47, 57, 107, 117, 113, 75, 81, 70, 106, 104, 75, 114, 114, 84, 120, 99, 75, 118, 65, 101, 78, 109, 121, 46, 117, 99, 118, 89, 68, 69, 119, 99, 113, 89, 105 } });
        }
    }
}
