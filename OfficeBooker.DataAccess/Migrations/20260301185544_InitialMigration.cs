using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OfficeBooker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    ReservationStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Capacity", "Equipment", "FloorNumber", "OfficeNumber" },
                values: new object[,]
                {
                    { 1, 10, "[]", 0, 101 },
                    { 2, 8, "[]", 0, 102 },
                    { 3, 12, "[]", 0, 103 },
                    { 4, 10, "[]", 1, 201 },
                    { 5, 15, "[]", 1, 202 },
                    { 6, 10, "[]", 1, 203 },
                    { 7, 5, "[]", 1, 204 },
                    { 8, 20, "[]", 2, 301 },
                    { 9, 10, "[]", 2, 302 },
                    { 10, 8, "[]", 2, 303 }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Piotr", "Nowak" },
                    { 2, "Aneta", "Kowalska" },
                    { 3, "Mateusz", "Wiśniewski" },
                    { 4, "Karolina", "Wójcik" },
                    { 5, "Jakub", "Kowalczyk" },
                    { 6, "Małgorzata", "Kamińska" },
                    { 7, "Wojciech", "Lewandowski" },
                    { 8, "Katarzyna", "Zielińska" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OfficeId",
                table: "Reservations",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_WorkerId",
                table: "Reservations",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
