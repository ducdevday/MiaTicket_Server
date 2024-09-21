using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateVoucherEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherFixedAmount");

            migrationBuilder.DropTable(
                name: "VoucherPercentage");

            migrationBuilder.DropColumn(
                name: "IsPercentage",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Order",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Voucher",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Voucher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Voucher",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order",
                newName: "Discount");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Voucher",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<bool>(
                name: "IsPercentage",
                table: "Voucher",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VoucherFixedAmount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherFixedAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherFixedAmount_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherPercentage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherPercentage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherPercentage_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherFixedAmount_VoucherId",
                table: "VoucherFixedAmount",
                column: "VoucherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherPercentage_VoucherId",
                table: "VoucherPercentage",
                column: "VoucherId",
                unique: true);
        }
    }
}
