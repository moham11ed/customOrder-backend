using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace customOrder.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ProductNames_ProductNamesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductNamesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UploadedImagePath",
                table: "Orders",
                newName: "ShapeImageUrl");

            migrationBuilder.RenameColumn(
                name: "SelectedOils",
                table: "Orders",
                newName: "SelectedOilsJson");

            migrationBuilder.RenameColumn(
                name: "ProductNamesId",
                table: "Orders",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "LogoDesignUrl",
                table: "Orders",
                newName: "DesignUrl");

            migrationBuilder.RenameColumn(
                name: "BottleDesignUrl",
                table: "Orders",
                newName: "CustomImage");

            migrationBuilder.AddColumn<string>(
                name: "ClientCity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientCountry",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientStreet",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DesignId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShapeId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientCountry",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientPhone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientStreet",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DesignId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShapeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShapeImageUrl",
                table: "Orders",
                newName: "UploadedImagePath");

            migrationBuilder.RenameColumn(
                name: "SelectedOilsJson",
                table: "Orders",
                newName: "SelectedOils");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "ProductNamesId");

            migrationBuilder.RenameColumn(
                name: "DesignUrl",
                table: "Orders",
                newName: "LogoDesignUrl");

            migrationBuilder.RenameColumn(
                name: "CustomImage",
                table: "Orders",
                newName: "BottleDesignUrl");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
