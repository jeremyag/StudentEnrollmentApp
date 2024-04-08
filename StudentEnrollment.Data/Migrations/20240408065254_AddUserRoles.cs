using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3f4631bd-f907-4409-b416-ba356312e659", 0, "59019115-019e-461a-8d94-7e1f703689e3", null, "schooluser@localhost.com", true, "School", "User", false, null, "SCHOOLUSER@LOCALHOST.COM", "SCHOOLUSER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEJk37qHj8TySaRZuecMM2D8nIm6w3fXzCt3i1BIuHm3I0G14KxIcOrJJq+86uuLbvQ==", null, false, "b0091705-27fb-47d7-987b-19f4b177ac6b", false, "schooluser@localhost.com" },
                    { "408aa945-3d84-4421-8342-7269ec64d949", 0, "f4a049a9-c99e-41da-ba09-e66f6ee1cb0f", null, "schooladmin@localhost.com", true, "School", "Admin", false, null, "SCHOOLADMIN@LOCALHOST.COM", "SCHOOLADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEL5P/NmbAoRlGKonPCDFx0+MBdHBl9HSJaUzQVuiSf0E0phWAXX1b8PDqL38dbUO+w==", null, false, "17a43d4a-3c0b-4c9c-a914-faa2674855e0", false, "schooladmin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "504830c0-9bce-4981-a8a8-e2d47a4f7db2", "3f4631bd-f907-4409-b416-ba356312e659" },
                    { "67a2d6e2-c096-4a73-8541-18e9a915c60d", "408aa945-3d84-4421-8342-7269ec64d949" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "504830c0-9bce-4981-a8a8-e2d47a4f7db2", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "67a2d6e2-c096-4a73-8541-18e9a915c60d", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");
        }
    }
}
