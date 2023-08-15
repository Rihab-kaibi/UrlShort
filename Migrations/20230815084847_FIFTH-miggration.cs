using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShort.Migrations
{
    /// <inheritdoc />
    public partial class FIFTHmiggration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_IpInfos_IpInfoId",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "continent",
                table: "IpInfos",
                newName: "Continent");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_IpInfos_IpInfoId",
                table: "Urls",
                column: "IpInfoId",
                principalTable: "IpInfos",
                principalColumn: "IpInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_IpInfos_IpInfoId",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Urls");

            migrationBuilder.RenameColumn(
                name: "Continent",
                table: "IpInfos",
                newName: "continent");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_IpInfos_IpInfoId",
                table: "Urls",
                column: "IpInfoId",
                principalTable: "IpInfos",
                principalColumn: "IpInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
