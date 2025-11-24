using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCustomerDetails",
                table: "CourseCustomerDetails");

            migrationBuilder.DropIndex(
                name: "IX_CourseCustomerDetails_CustomerId",
                table: "CourseCustomerDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseCustomerDetails");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CourseCustomerDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCustomerDetails",
                table: "CourseCustomerDetails",
                columns: new[] { "CustomerId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CurrencyId",
                table: "Courses",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Currencies_CurrencyId",
                table: "Courses",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Currencies_CurrencyId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CurrencyId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCustomerDetails",
                table: "CourseCustomerDetails");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Courses");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "CourseCustomerDetails",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseCustomerDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCustomerDetails",
                table: "CourseCustomerDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCustomerDetails_CustomerId",
                table: "CourseCustomerDetails",
                column: "CustomerId");
        }
    }
}
