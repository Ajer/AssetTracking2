using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPrice",
                table: "Assets");

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Model", "PriceInDollar", "PurchaseDate", "Type" },
                values: new object[,]
                {
                    { 1, "Samsung", "XPS", 524.0, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer" },
                    { 2, "Iphone", "8", 294.0, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<double>(
                name: "LocalPrice",
                table: "Assets",
                type: "float",
                nullable: true);
        }
    }
}
