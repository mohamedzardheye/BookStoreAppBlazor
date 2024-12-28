using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class EmptyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51087778-5248-4439-a7ab-1b19bf48180c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4308aeaa-ce43-4a34-b3d3-9509f2b36d03", "AQAAAAIAAYagAAAAEArl6eOCsyILC3V4ZMVuLOhox+aapzTdS4ClFobEyxUxt7yolzzIwVx6HNz2LzO2qQ==", "cc30bf5a-33f6-4195-a417-13b3c498d4e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aed56a54-37fd-4a92-93c1-eb81f4d17d7d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b8e173a-a015-4315-96f8-df54f1561fca", "AQAAAAIAAYagAAAAECsJWIcoGkAHknEmew4/eB8wlFg5qaywmtYquSp27xT3KURwvIOCFIKs0kCSbZGJww==", "e616b869-88f4-4afd-a177-84537403d1e3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51087778-5248-4439-a7ab-1b19bf48180c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df54b290-df62-447a-940c-af195289a512", "AQAAAAIAAYagAAAAEBsqia1mab6sf4jXWOjcVPuLct/x7jXNod+c2QRNb3ftEALSyizAZYX10nOEDNnGiA==", "56bab2d1-9c64-4568-967a-eea7c9140153" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aed56a54-37fd-4a92-93c1-eb81f4d17d7d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7def65da-c2a2-41f5-8360-df9e31ac4eb5", "AQAAAAIAAYagAAAAEPr8hX/mEFPnM9e+jch03cKiuiIt9rR7cddS3hU54IbPa163QXvYXl4ZdPCWtPgW1g==", "7c94ff28-ab6a-4aee-b917-8710035bed90" });
        }
    }
}
