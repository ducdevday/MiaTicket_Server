using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateEventEntityStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Ticket_MaximumPurchase_MinValue",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "UserStatus",
                table: "User",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ticket_MaximumPurchase_MinValue",
                table: "Ticket",
                sql: "MinimumPurchase <= Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Ticket_MaximumPurchase_MinValue",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "User",
                newName: "UserStatus");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ticket_MaximumPurchase_MinValue",
                table: "Ticket",
                sql: "MaximumPurchase >= MinimumPurchase");
        }
    }
}
