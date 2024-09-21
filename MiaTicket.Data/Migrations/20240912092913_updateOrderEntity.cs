using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Event_EventId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_EventId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderTicketStatus",
                table: "OrderTicket");

            migrationBuilder.DropColumn(
                name: "AddressDistinct",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressNo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressProvince",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressWard",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "QrUrl",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "QrCode",
                table: "Order",
                type: "uniqueidentifier",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressDetail",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ShowTimeId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_EventId",
                table: "Order",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Event_EventId",
                table: "Order",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShowTime_EventId",
                table: "Order",
                column: "EventId",
                principalTable: "ShowTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Event_EventId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShowTime_EventId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_EventId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AddressDetail",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShowTimeId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderTicketStatus",
                table: "OrderTicket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "QrCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Order",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "AddressDistinct",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressNo",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressProvince",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressWard",
                table: "Order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrUrl",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_EventId",
                table: "Order",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Event_EventId",
                table: "Order",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
