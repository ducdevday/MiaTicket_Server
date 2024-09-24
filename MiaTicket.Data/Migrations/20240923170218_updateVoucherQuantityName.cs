using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateVoucherQuantityName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinQuanityPerOrder",
                table: "Voucher",
                newName: "MinQuantityPerOrder");

            migrationBuilder.RenameColumn(
                name: "MaxQuanityPerOrder",
                table: "Voucher",
                newName: "MaxQuantityPerOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Code",
                table: "Voucher",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Voucher_Code",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "MinQuantityPerOrder",
                table: "Voucher",
                newName: "MinQuanityPerOrder");

            migrationBuilder.RenameColumn(
                name: "MaxQuantityPerOrder",
                table: "Voucher",
                newName: "MaxQuanityPerOrder");
        }
    }
}
