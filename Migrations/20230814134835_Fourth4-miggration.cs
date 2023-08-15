using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShort.Migrations
{
    /// <inheritdoc />
    public partial class Fourth4miggration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IpInfoId",
                table: "Urls",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IpInfos",
                columns: table => new
                {
                    IpInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    ISP = table.Column<string>(type: "TEXT", nullable: false),
                    UserType = table.Column<string>(type: "TEXT", nullable: false),
                    continent = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpInfos", x => x.IpInfoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urls_IpInfoId",
                table: "Urls",
                column: "IpInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_IpInfos_IpInfoId",
                table: "Urls",
                column: "IpInfoId",
                principalTable: "IpInfos",
                principalColumn: "IpInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_IpInfos_IpInfoId",
                table: "Urls");

            migrationBuilder.DropTable(
                name: "IpInfos");

            migrationBuilder.DropIndex(
                name: "IX_Urls_IpInfoId",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "IpInfoId",
                table: "Urls");
        }
    }
}
