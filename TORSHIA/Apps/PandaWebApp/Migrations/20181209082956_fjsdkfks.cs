using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class fjsdkfks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSector_Tasks_TaskId",
                table: "TaskSector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskSector",
                table: "TaskSector");

            migrationBuilder.RenameTable(
                name: "TaskSector",
                newName: "TasksSectors");

            migrationBuilder.RenameIndex(
                name: "IX_TaskSector_TaskId",
                table: "TasksSectors",
                newName: "IX_TasksSectors_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksSectors",
                table: "TasksSectors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TasksSectors_Tasks_TaskId",
                table: "TasksSectors",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TasksSectors_Tasks_TaskId",
                table: "TasksSectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksSectors",
                table: "TasksSectors");

            migrationBuilder.RenameTable(
                name: "TasksSectors",
                newName: "TaskSector");

            migrationBuilder.RenameIndex(
                name: "IX_TasksSectors_TaskId",
                table: "TaskSector",
                newName: "IX_TaskSector_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskSector",
                table: "TaskSector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSector_Tasks_TaskId",
                table: "TaskSector",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
