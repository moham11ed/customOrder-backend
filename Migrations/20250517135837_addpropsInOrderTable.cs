using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace customOrder.Migrations
{
    /// <inheritdoc />
    public partial class addpropsInOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductNamesId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductNamesId",
                table: "Orders",
                column: "ProductNamesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ProductNames_ProductNamesId",
                table: "Orders",
                column: "ProductNamesId",
                principalTable: "ProductNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ProductNames_ProductNamesId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ProductNames");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductNamesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductNamesId",
                table: "Orders");
        }
    }
}
