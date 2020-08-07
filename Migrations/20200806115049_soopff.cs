using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class soopff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoupFrequency",
                table: "UserPlScheduler",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SoupList",
                table: "UserPlScheduler",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoupFrequency",
                table: "UserPlScheduler");

            migrationBuilder.DropColumn(
                name: "SoupList",
                table: "UserPlScheduler");
        }
    }
}
