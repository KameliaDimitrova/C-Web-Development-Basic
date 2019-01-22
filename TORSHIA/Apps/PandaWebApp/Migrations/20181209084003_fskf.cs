using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class fskf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskSectors",
                table: "TasksSectors");

            migrationBuilder.AddColumn<int>(
                name: "Sector",
                table: "TasksSectors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sector",
                table: "TasksSectors");

            migrationBuilder.AddColumn<int>(
                name: "TaskSectors",
                table: "TasksSectors",
                nullable: false,
                defaultValue: 0);
        }
    }
}
