using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShort.Migrations
{
    /// <inheritdoc />
    public partial class Firstmiggration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Visits = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ShortUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IpInfos",
                columns: table => new
                {
                    IpInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    ISP = table.Column<string>(type: "TEXT", nullable: false),
                    UserType = table.Column<string>(type: "TEXT", nullable: false),
                    Continent = table.Column<string>(type: "TEXT", nullable: false),
                    ShortUrlId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpInfos", x => x.IpInfoId);
                    table.ForeignKey(
                        name: "FK_IpInfos_Urls_IpInfoId",
                        column: x => x.IpInfoId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceFamily = table.Column<string>(type: "TEXT", nullable: false),
                    BrowserName = table.Column<string>(type: "TEXT", nullable: false),
                    BrowserVersion = table.Column<string>(type: "TEXT", nullable: false),
                    OperatingSystem = table.Column<string>(type: "TEXT", nullable: false),
                    OperatingSystemVirsion = table.Column<string>(type: "TEXT", nullable: false),
                    ShortUrlId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stat_Urls_ShortUrlId",
                        column: x => x.ShortUrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stat_ShortUrlId",
                table: "Stat",
                column: "ShortUrlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpInfos");

            migrationBuilder.DropTable(
                name: "Stat");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
