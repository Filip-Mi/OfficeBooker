using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OfficeBooker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "d0897fd3-0475-42fb-bed3-01e7380813bb", "Admin", "ADMIN" },
                    { "3d6f185f-4c1f-557g-97bg-594e67ge8321", "3190e6ef-2f54-4b00-9c24-61f3031603c4", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d", 0, "aae5014d-83e7-4614-aa31-14829b2419ed", "admin@office.com", true, false, null, "Adam", "ADMIN@OFFICE.COM", "ADMIN@OFFICE.COM", "AQAAAAIAAYagAAAAEPP97ZR1UNP6eqlqngYOreeuul1yi+fhEx7/xbByV3NfG2peefpyJ4+6NtxTDpZHUA==", null, false, "432b4cd8-1475-4d85-9ea1-997632121b0b", "Kowalski", false, "admin@office.com" },
                    { "b2c3d4e5-f6a7-5b6c-9d0e-1f2a3b4c5d6e", 0, "d59a4af0-8dda-4293-a9ba-c7c2bf3f047b", "user@office.com", true, false, null, "Jan", "USER@OFFICE.COM", "USER@OFFICE.COM", "AQAAAAIAAYagAAAAEJrVvgVKx5jIXVP3zOwt0qKDbhMbzFVf3VsEf6FdzF4EvEHbmsqEz4Vs7KF164+Rcw==", null, false, "99c0bcd9-db60-4f83-8821-d2d17e4e458b", "Nowak", false, "user@office.com" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Capacity", "Equipment", "FloorNumber", "OfficeNumber" },
                values: new object[,]
                {
                    { 1, 4, "Monitor, Whiteboard", 1, 101 },
                    { 2, 2, "Dual Monitor", 1, 102 },
                    { 3, 6, "Projector, Conference Mic", 2, 201 },
                    { 4, 1, "Standing Desk", 3, 301 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d" },
                    { "3d6f185f-4c1f-557g-97bg-594e67ge8321", "b2c3d4e5-f6a7-5b6c-9d0e-1f2a3b4c5d6e" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3d6f185f-4c1f-557g-97bg-594e67ge8321", "b2c3d4e5-f6a7-5b6c-9d0e-1f2a3b4c5d6e" });

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d6f185f-4c1f-557g-97bg-594e67ge8321");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c3d4e5-f6a7-5b6c-9d0e-1f2a3b4c5d6e");
        }
    }
}
