using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class AddFluentApiCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTag_Channels_ChannelId",
                table: "ChannelTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Channels_ChannelId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChanel_Channels_ChannelId",
                table: "UserChanel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChanel",
                table: "UserChanel");

            migrationBuilder.DropIndex(
                name: "IX_UserChanel_ChannelId",
                table: "UserChanel");

            migrationBuilder.DropIndex(
                name: "IX_UserChanel_UserId",
                table: "UserChanel");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ChannelId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelTag",
                table: "ChannelTag");

            migrationBuilder.DropIndex(
                name: "IX_ChannelTag_ChannelId",
                table: "ChannelTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserChanel");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "UserChanel");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChannelTag");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserChanel_ChanelId_UserId",
                table: "UserChanel",
                columns: new[] { "ChanelId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChanel",
                table: "UserChanel",
                columns: new[] { "UserId", "ChanelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelTag",
                table: "ChannelTag",
                columns: new[] { "ChannelId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTag_Channels_TagId",
                table: "ChannelTag",
                column: "TagId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChanel_Channels_ChanelId",
                table: "UserChanel",
                column: "ChanelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTag_Channels_TagId",
                table: "ChannelTag");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChanel_Channels_ChanelId",
                table: "UserChanel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserChanel_ChanelId_UserId",
                table: "UserChanel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChanel",
                table: "UserChanel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelTag",
                table: "ChannelTag");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserChanel",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "UserChanel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChannelTag",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChanel",
                table: "UserChanel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelTag",
                table: "ChannelTag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserChanel_ChannelId",
                table: "UserChanel",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChanel_UserId",
                table: "UserChanel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ChannelId",
                table: "Tags",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelTag_ChannelId",
                table: "ChannelTag",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTag_Channels_ChannelId",
                table: "ChannelTag",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Channels_ChannelId",
                table: "Tags",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChanel_Channels_ChannelId",
                table: "UserChanel",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
