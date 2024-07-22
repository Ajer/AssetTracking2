using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class Omit_Constr_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "PriceInDollar",
                value: 524.0);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "PriceInDollar",
                value: 524.0);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Model", "PriceInDollar", "PurchaseDate" },
                values: new object[] { "fold", 524.0, new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "PriceInDollar",
                value: 1024.0);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "PriceInDollar",
                value: 294.0);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Model", "PriceInDollar", "PurchaseDate" },
                values: new object[] { "fold-z", 294.0, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
