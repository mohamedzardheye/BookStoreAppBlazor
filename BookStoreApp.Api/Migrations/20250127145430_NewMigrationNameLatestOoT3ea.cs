using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationNameLatestOoT3ea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9cac6922-ba24-400c-80ee-c4e941159a0b", null, "User", "USER" },
                    { "cd2e52c4-4e26-48b6-9d23-ac77456d91f8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "51087778-5248-4439-a7ab-1b19bf48180c", 0, "9f2af988-e36e-4fbc-8355-8a4a7e135b2c", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAECVee4SlqzfPR4rQxSLWTR1AQJ/63UJ9LAE04VwtasoqAnjLlubUz5/hIB2mjHWEFw==", null, false, "1ae7efa1-8369-4a4f-be9c-08d09bfcf6c3", false, "admin@bookstore.com" },
                    { "aed56a54-37fd-4a92-93c1-eb81f4d17d7d", 0, "e8ac8da6-e4c7-4086-b8ba-414781e134ed", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAELe/Z5WoVDadtI4yqPZaAeOQzNzD2N84yn53NbPxUKmHV+hA6bq1s1neiRhiXrZ6PQ==", null, false, "3536833e-1709-44a0-8567-2304d790b665", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cd2e52c4-4e26-48b6-9d23-ac77456d91f8", "51087778-5248-4439-a7ab-1b19bf48180c" },
                    { "9cac6922-ba24-400c-80ee-c4e941159a0b", "aed56a54-37fd-4a92-93c1-eb81f4d17d7d" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd2e52c4-4e26-48b6-9d23-ac77456d91f8", "51087778-5248-4439-a7ab-1b19bf48180c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9cac6922-ba24-400c-80ee-c4e941159a0b", "aed56a54-37fd-4a92-93c1-eb81f4d17d7d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cac6922-ba24-400c-80ee-c4e941159a0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd2e52c4-4e26-48b6-9d23-ac77456d91f8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51087778-5248-4439-a7ab-1b19bf48180c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aed56a54-37fd-4a92-93c1-eb81f4d17d7d");
        }
    }
}
