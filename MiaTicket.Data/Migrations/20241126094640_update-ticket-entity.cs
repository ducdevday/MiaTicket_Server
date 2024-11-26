using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateticketentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitQuantity",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitQuantity",
                table: "Ticket");
        }
    }
}
