﻿using System;
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
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 427, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 427, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)))
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
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 427, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 427, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    OfficerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(maxLength: 100, nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 414, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0))),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)))
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
                name: "UserGames",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2018, 12, 19, 15, 36, 14, 436, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGames", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UserGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Officers_UserId",
                table: "Officers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_GameId",
                table: "UserGames",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "UserGames");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}