using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class TestTPT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computers_Assets_Id",
                        column: x => x.Id,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Assets_Id",
                        column: x => x.Id,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "Phones");
        }
    }
}
