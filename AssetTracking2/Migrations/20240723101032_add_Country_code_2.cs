using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class add_Country_code_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Offices",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 1,
                column: "CountryCode",
                value: "SWE");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 2,
                column: "CountryCode",
                value: "USA");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 3,
                column: "CountryCode",
                value: "SPA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Offices");
        }
    }
}
