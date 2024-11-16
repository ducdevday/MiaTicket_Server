using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class addcreateatupdateattoentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Voucher",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Voucher",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VerificationCode",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "VerificationCode",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ShowTime",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ShowTime",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderTicket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderTicket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EventOrganizer",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EventOrganizer",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Banner",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Banner",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BankAccount",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BankAccount",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Admin",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admin",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateTable(
                name: "EventCheckIn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    CheckedInAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCheckIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCheckIn_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCheckIn_User_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCheckIn_OrderId",
                table: "EventCheckIn",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventCheckIn_OrganizerId",
                table: "EventCheckIn",
                column: "OrganizerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCheckIn");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VerificationCode");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "VerificationCode");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ShowTime");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ShowTime");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderTicket");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OrderTicket");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EventOrganizer");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EventOrganizer");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Banner");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Banner");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
