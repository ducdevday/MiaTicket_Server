using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderEntityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShowTime_EventId",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShowTimeId",
                table: "Order",
                column: "ShowTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShowTime_ShowTimeId",
                table: "Order",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShowTime_ShowTimeId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShowTimeId",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShowTime_EventId",
                table: "Order",
                column: "EventId",
                principalTable: "ShowTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
