using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Subject_entity_to_CourseComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "CourseComments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "CourseComments");
        }
    }
}
