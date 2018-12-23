using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksu.Gdc.Api.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    HostUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 358, DateTimeKind.Unspecified).AddTicks(3210), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 358, DateTimeKind.Unspecified).AddTicks(3790), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 357, DateTimeKind.Unspecified).AddTicks(7950), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 357, DateTimeKind.Unspecified).AddTicks(8620), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 360, DateTimeKind.Unspecified).AddTicks(8790), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GameUsers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    OfficerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(maxLength: 100, nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 344, DateTimeKind.Unspecified).AddTicks(220), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 23, 0, 41, 50, 357, DateTimeKind.Unspecified).AddTicks(3060), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.OfficerId);
                    table.ForeignKey(
                        name: "FK_Officers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_GameId",
                table: "GameUsers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Officers_UserId",
                table: "Officers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUsers");

            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
