using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderTicketEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "OrderTicket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTicket_TicketId",
                table: "OrderTicket",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTicket_Ticket_TicketId",
                table: "OrderTicket",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTicket_Ticket_TicketId",
                table: "OrderTicket");

            migrationBuilder.DropIndex(
                name: "IX_OrderTicket_TicketId",
                table: "OrderTicket");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "OrderTicket");
        }
    }
}
