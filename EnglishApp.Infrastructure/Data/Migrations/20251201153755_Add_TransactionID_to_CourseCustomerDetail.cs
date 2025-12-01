using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TransactionID_to_CourseCustomerDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "CourseCustomerDetails",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "CourseCustomerDetails");
        }
    }
}
