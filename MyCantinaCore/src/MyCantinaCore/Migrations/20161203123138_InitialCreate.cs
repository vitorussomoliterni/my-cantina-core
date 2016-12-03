using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyCantinaCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bottles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AverageRating = table.Column<double>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Producer = table.Column<string>(nullable: false),
                    Region = table.Column<string>(nullable: true),
                    WineType = table.Column<string>(nullable: false),
                    Year = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bottles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrapeVarieties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Colour = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrapeVarieties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerBottles",
                columns: table => new
                {
                    BottleId = table.Column<int>(nullable: false),
                    ConsumerId = table.Column<int>(nullable: false),
                    DateAcquired = table.Column<DateTime>(nullable: false),
                    DateOpened = table.Column<DateTime>(nullable: true),
                    Owned = table.Column<bool>(type: "bit", nullable: false),
                    PricePaid = table.Column<double>(type: "money", nullable: true),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerBottles", x => new { x.BottleId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ConsumerBottles_Bottles_BottleId",
                        column: x => x.BottleId,
                        principalTable: "Bottles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumerBottles_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    BottleId = table.Column<int>(nullable: false),
                    ConsumerId = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.BottleId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_Reviews_Bottles_BottleId",
                        column: x => x.BottleId,
                        principalTable: "Bottles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BottleGrapeVarieties",
                columns: table => new
                {
                    BottleId = table.Column<int>(nullable: false),
                    GrapeVarietyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BottleGrapeVarieties", x => new { x.BottleId, x.GrapeVarietyId });
                    table.ForeignKey(
                        name: "FK_BottleGrapeVarieties_Bottles_BottleId",
                        column: x => x.BottleId,
                        principalTable: "Bottles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BottleGrapeVarieties_GrapeVarieties_GrapeVarietyId",
                        column: x => x.GrapeVarietyId,
                        principalTable: "GrapeVarieties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BottleGrapeVarieties_GrapeVarietyId",
                table: "BottleGrapeVarieties",
                column: "GrapeVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerBottles_ConsumerId",
                table: "ConsumerBottles",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ConsumerId",
                table: "Reviews",
                column: "ConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BottleGrapeVarieties");

            migrationBuilder.DropTable(
                name: "ConsumerBottles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "GrapeVarieties");

            migrationBuilder.DropTable(
                name: "Bottles");

            migrationBuilder.DropTable(
                name: "Consumers");
        }
    }
}
