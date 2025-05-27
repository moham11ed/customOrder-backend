using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace customOrder.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveForeignKeyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShapeWithDesigns_BottleDesigns_ShapeId",
                table: "ShapeWithDesigns");

            migrationBuilder.DropForeignKey(
                name: "FK_ShapeWithDesigns_LogoDesigns_DesignId",
                table: "ShapeWithDesigns");

            migrationBuilder.DropIndex(
                name: "IX_ShapeWithDesigns_DesignId",
                table: "ShapeWithDesigns");

            migrationBuilder.DropIndex(
                name: "IX_ShapeWithDesigns_ShapeId",
                table: "ShapeWithDesigns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShapeWithDesigns_DesignId",
                table: "ShapeWithDesigns",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_ShapeWithDesigns_ShapeId",
                table: "ShapeWithDesigns",
                column: "ShapeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShapeWithDesigns_BottleDesigns_ShapeId",
                table: "ShapeWithDesigns",
                column: "ShapeId",
                principalTable: "BottleDesigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShapeWithDesigns_LogoDesigns_DesignId",
                table: "ShapeWithDesigns",
                column: "DesignId",
                principalTable: "LogoDesigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
