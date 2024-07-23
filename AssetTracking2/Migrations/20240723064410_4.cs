using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Model", "OfficeId", "PriceInDollar", "PurchaseDate", "Type" },
                values: new object[,]
                {
                    { 1, "Dell", "XPS", 1, 524.0, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer" },
                    { 2, "MacBook", "Air", 2, 524.0, new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer" },
                    { 3, "Iphone", "8", 2, 524.0, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone" },
                    { 4, "Samsung", "fold", 1, 524.0, new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone" }
                });

            migrationBuilder.InsertData(
                table: "Computers",
                column: "Id",
                values: new object[]
                {
                    1,
                    2
                });

            migrationBuilder.InsertData(
                table: "Phones",
                column: "Id",
                values: new object[]
                {
                    3,
                    4
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Offices_OfficeId",
                table: "Assets",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Offices_OfficeId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets");

            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

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

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Assets");
        }
    }
}
