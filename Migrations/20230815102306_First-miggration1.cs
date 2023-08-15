using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShort.Migrations
{
    /// <inheritdoc />
    public partial class Firstmiggration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpInfos_Urls_IpInfoId",
                table: "IpInfos");

            migrationBuilder.AlterColumn<int>(
                name: "IpInfoId",
                table: "IpInfos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_IpInfos_ShortUrlId",
                table: "IpInfos",
                column: "ShortUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_IpInfos_Urls_ShortUrlId",
                table: "IpInfos",
                column: "ShortUrlId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpInfos_Urls_ShortUrlId",
                table: "IpInfos");

            migrationBuilder.DropIndex(
                name: "IX_IpInfos_ShortUrlId",
                table: "IpInfos");

            migrationBuilder.AlterColumn<int>(
                name: "IpInfoId",
                table: "IpInfos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_IpInfos_Urls_IpInfoId",
                table: "IpInfos",
                column: "IpInfoId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
