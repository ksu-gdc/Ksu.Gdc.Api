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
                    HostUrl = table.Column<string>(maxLength: 2000, nullable: true),
                    IsFeatured = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 534, DateTimeKind.Unspecified).AddTicks(4590), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 534, DateTimeKind.Unspecified).AddTicks(4990), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Data = table.Column<byte[]>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 513, DateTimeKind.Unspecified).AddTicks(770), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
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
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 527, DateTimeKind.Unspecified).AddTicks(1190), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 527, DateTimeKind.Unspecified).AddTicks(1590), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GameImages",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 536, DateTimeKind.Unspecified).AddTicks(130), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImages", x => new { x.GameId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_GameImages_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 536, DateTimeKind.Unspecified).AddTicks(6600), new TimeSpan(0, -6, 0, 0, 0)))
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
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 526, DateTimeKind.Unspecified).AddTicks(7070), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 526, DateTimeKind.Unspecified).AddTicks(7630), new TimeSpan(0, -6, 0, 0, 0)))
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

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2019, 2, 4, 0, 51, 43, 534, DateTimeKind.Unspecified).AddTicks(1070), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => new { x.UserId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_UserImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameImages_ImageId",
                table: "GameImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_GameId",
                table: "GameUsers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Officers_UserId",
                table: "Officers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_ImageId",
                table: "UserImages",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameImages");

            migrationBuilder.DropTable(
                name: "GameUsers");

            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
