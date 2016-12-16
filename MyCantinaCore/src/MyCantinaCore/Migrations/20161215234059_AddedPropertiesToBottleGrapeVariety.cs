using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCantinaCore.Migrations
{
    public partial class AddedPropertiesToBottleGrapeVariety : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrapeVarietyColour",
                table: "BottleGrapeVarieties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrapeVarietyName",
                table: "BottleGrapeVarieties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrapeVarietyColour",
                table: "BottleGrapeVarieties");

            migrationBuilder.DropColumn(
                name: "GrapeVarietyName",
                table: "BottleGrapeVarieties");
        }
    }
}
