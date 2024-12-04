using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Account", "IsPasswordTemporary", "Password" },
                values: new object[] { new Guid("53793728-a273-4e8a-8e74-ed4fd1b67460"), "admin", false, new byte[] { 36, 50, 97, 36, 49, 49, 36, 54, 49, 104, 105, 119, 108, 81, 97, 68, 97, 86, 118, 122, 78, 65, 49, 78, 82, 47, 57, 107, 117, 113, 75, 81, 70, 106, 104, 75, 114, 114, 84, 120, 99, 75, 118, 65, 101, 78, 109, 121, 46, 117, 99, 118, 89, 68, 69, 119, 99, 113, 89, 105 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("53793728-a273-4e8a-8e74-ed4fd1b67460"));
        }
    }
}
