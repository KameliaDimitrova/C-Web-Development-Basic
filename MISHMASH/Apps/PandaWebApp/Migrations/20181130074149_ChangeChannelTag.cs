using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class ChangeChannelTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTag_Channels_TagId",
                table: "ChannelTag");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTag_Channels_ChannelId",
                table: "ChannelTag",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTag_Channels_ChannelId",
                table: "ChannelTag");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTag_Channels_TagId",
                table: "ChannelTag",
                column: "TagId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
