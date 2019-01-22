using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class fr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChanel_Channels_ChanelId",
                table: "UserChanel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserChanel_ChanelId_UserId",
                table: "UserChanel");

            migrationBuilder.RenameColumn(
                name: "ChanelId",
                table: "UserChanel",
                newName: "ChannelId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserChanel_ChannelId_UserId",
                table: "UserChanel",
                columns: new[] { "ChannelId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserChanel_Channels_ChannelId",
                table: "UserChanel",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChanel_Channels_ChannelId",
                table: "UserChanel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserChanel_ChannelId_UserId",
                table: "UserChanel");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "UserChanel",
                newName: "ChanelId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserChanel_ChanelId_UserId",
                table: "UserChanel",
                columns: new[] { "ChanelId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserChanel_Channels_ChanelId",
                table: "UserChanel",
                column: "ChanelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
