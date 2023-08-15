using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShort.Migrations
{
    /// <inheritdoc />
    public partial class Thirdmiggration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortUrlId = table.Column<int>(type: "INTEGER", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceFamily = table.Column<string>(type: "TEXT", nullable: false),
                    BrowserName = table.Column<string>(type: "TEXT", nullable: false),
                    BrowserVersion = table.Column<string>(type: "TEXT", nullable: false),
                    OperatingSystem = table.Column<string>(type: "TEXT", nullable: false),
                    OperatingSystemVirsion = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "Stat");
        }
    }
}
