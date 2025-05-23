using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace customOrder.Migrations
{
    /// <inheritdoc />
    public partial class AddOilTypeToOilOption2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "OilOptions",
                nullable: false,
                defaultValue: 1); // Defaults to Shampoo (1)
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
