using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeVnPayInformationEntityTransactionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "VnPayInformation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionDate",
                table: "VnPayInformation",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
