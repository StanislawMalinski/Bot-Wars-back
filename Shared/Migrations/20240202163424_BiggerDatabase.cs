using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class BiggerDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AchievementThresholds",
                columns: new[] { "Id", "AchievementTypeId", "Threshold" },
                values: new object[,]
                {
                    { 5L, 1L, 20L },
                    { 6L, 2L, 8L },
                    { 7L, 3L, 10L },
                    { 8L, 4L, 5L }
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7883));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7947));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7949));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7953));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7956));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7962));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(7964));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8002));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 17, 34, 24, 237, DateTimeKind.Local).AddTicks(8080));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5781));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5838));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5841));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5843));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5846));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5849));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5852));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5857));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5860));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5895));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5906));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5909));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5911));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5918));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5920));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5923));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 2, 16, 22, 45, 429, DateTimeKind.Local).AddTicks(5926));
        }
    }
}
