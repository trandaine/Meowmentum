using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerPaymentRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "CustomerPaymentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "CustomerPaymentRecords");
        }
    }
}
