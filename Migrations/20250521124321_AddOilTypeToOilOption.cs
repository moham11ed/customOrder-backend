using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace customOrder.Migrations
{
    /// <inheritdoc />
    public partial class AddOilTypeToOilOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "OilOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "OilOptions");
        }
    }
}
