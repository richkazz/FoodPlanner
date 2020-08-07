using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class soopffs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "showSF",
                table: "UserPlScheduler",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "showSF",
                table: "UserPlScheduler");
        }
    }
}
