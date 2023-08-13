using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZwalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDifficultyandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { new Guid("6b11cf7d-65ce-4a54-a1f8-ad9596774ea7"), "Medium" },
                    { new Guid("d959df6c-e8ef-4aa7-b4b7-cea3f608bf9a"), "Easy" },
                    { new Guid("e400333f-b2e5-4967-843c-f09690a0cc58"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("3f9dc9bd-c7b4-4000-8553-cb24e49ab959"), "LKO", "Lucknow", "https://ghoomophiro.com/wp-content/uploads/2022/10/Places-to-visit-in-lucknow-scaled.jpg" },
                    { new Guid("4a921308-71d5-4d7a-a842-b5c493f9ff8e"), "DL", "Delhi", "https://cdn.britannica.com/37/189837-050-F0AF383E/New-Delhi-India-War-Memorial-arch-Sir.jpg" },
                    { new Guid("c2e0849a-53f8-432f-840b-bca4aba3158f"), "MUB", "Mumbai", "https://cdn.britannica.com/26/84526-050-45452C37/Gateway-monument-India-entrance-Mumbai-Harbour-coast.jpg" },
                    { new Guid("eb9775e6-ba49-4e46-81e0-503cc751ca3f"), "PUN", "Pune", "https://mittalbuilders.com/wp-content/uploads/2020/12/Reasons-to-settle-down-in-Pune-1400x700.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("6b11cf7d-65ce-4a54-a1f8-ad9596774ea7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("d959df6c-e8ef-4aa7-b4b7-cea3f608bf9a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("e400333f-b2e5-4967-843c-f09690a0cc58"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3f9dc9bd-c7b4-4000-8553-cb24e49ab959"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4a921308-71d5-4d7a-a842-b5c493f9ff8e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c2e0849a-53f8-432f-840b-bca4aba3158f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("eb9775e6-ba49-4e46-81e0-503cc751ca3f"));
        }
    }
}
