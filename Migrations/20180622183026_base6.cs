using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Belt.Migrations
{
    public partial class base6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creatorId = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    duration = table.Column<float>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.id);
                    table.ForeignKey(
                        name: "FK_activities_users_creatorId",
                        column: x => x.creatorId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    activityId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants", x => x.id);
                    table.ForeignKey(
                        name: "FK_participants_activities_activityId",
                        column: x => x.activityId,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_participants_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_creatorId",
                table: "activities",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_participants_activityId",
                table: "participants",
                column: "activityId");

            migrationBuilder.CreateIndex(
                name: "IX_participants_userId",
                table: "participants",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participants");

            migrationBuilder.DropTable(
                name: "activities");
        }
    }
}
