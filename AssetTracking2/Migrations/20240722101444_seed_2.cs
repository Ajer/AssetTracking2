using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class seed_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Brand",
                value: "Dell");

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Model", "PriceInDollar", "PurchaseDate", "Type" },
                values: new object[,]
                {
                    { 2, "MacBook", "Air", 1024.0, new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer" },
                    { 3, "Iphone", "8", 294.0, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone" },
                    { 4, "Samsung", "fold-z", 294.0, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Brand",
                value: "Samsung");

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Model", "PriceInDollar", "PurchaseDate", "Type" },
                values: new object[] { 2, "Iphone", "8", 294.0, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone" });
        }
    }
}
