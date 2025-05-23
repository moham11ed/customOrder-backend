using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace customOrder.Migrations
{
    /// <inheritdoc />
    public partial class AddShapeWithDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShapeWithDesigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShapeId = table.Column<int>(type: "int", nullable: false),
                    DesignId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShapeWithDesigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShapeWithDesigns_BottleDesigns_ShapeId",
                        column: x => x.ShapeId,
                        principalTable: "BottleDesigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShapeWithDesigns_LogoDesigns_DesignId",
                        column: x => x.DesignId,
                        principalTable: "LogoDesigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShapeWithDesigns_DesignId",
                table: "ShapeWithDesigns",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_ShapeWithDesigns_ShapeId",
                table: "ShapeWithDesigns",
                column: "ShapeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShapeWithDesigns");
        }
    }
}
