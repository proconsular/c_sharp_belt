using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Belt.Migrations
{
    public partial class base7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "time",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "time",
                table: "activities");
        }
    }
}
