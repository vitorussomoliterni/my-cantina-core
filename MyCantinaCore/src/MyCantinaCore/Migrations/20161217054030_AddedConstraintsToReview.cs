using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCantinaCore.Migrations
{
    public partial class AddedConstraintsToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_ConsumerId",
                table: "Reviews");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Reviews_ConsumerId_BottleId",
                table: "Reviews",
                columns: new[] { "ConsumerId", "BottleId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Reviews_ConsumerId_BottleId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ConsumerId",
                table: "Reviews",
                column: "ConsumerId");
        }
    }
}
