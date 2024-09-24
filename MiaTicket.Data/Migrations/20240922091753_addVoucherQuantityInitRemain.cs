using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class addVoucherQuantityInitRemain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Voucher",
                newName: "RemainQuantity");

            migrationBuilder.AddColumn<int>(
                name: "InitQuantity",
                table: "Voucher",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitQuantity",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "RemainQuantity",
                table: "Voucher",
                newName: "Quantity");
        }
    }
}
