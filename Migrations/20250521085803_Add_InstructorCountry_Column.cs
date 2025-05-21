using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleRESTApi.Migrations
{
    /// <inheritdoc />
    public partial class Add_InstructorCountry_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstructorCountry",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorCountry",
                table: "Instructors");
        }
    }
}
