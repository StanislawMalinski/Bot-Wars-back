using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class Achievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AchievementType",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1L, "You to play this amount of games" },
                    { 2L, "You to upload this amount of bots" },
                    { 3L, "You to win this amount of games" },
                    { 4L, "You to win this amount of tournaments" }
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4856));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4859));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4861));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4863));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4866));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4868));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4870));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4875));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4913));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4923));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4926));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4928));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4930));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4933));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4938));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4940));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 2, 1, 10, 59, 25, 152, DateTimeKind.Local).AddTicks(4943));

            migrationBuilder.InsertData(
                table: "AchievementRecord",
                columns: new[] { "Id", "AchievementTypeId", "PlayerId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 10L },
                    { 2L, 2L, 1L, 15L },
                    { 3L, 1L, 2L, 10L },
                    { 4L, 2L, 2L, 15L }
                });

            migrationBuilder.InsertData(
                table: "AchievementThresholds",
                columns: new[] { "Id", "AchievementTypeId", "Threshold" },
                values: new object[,]
                {
                    { 1L, 1L, 10L },
                    { 2L, 2L, 4L },
                    { 3L, 3L, 5L },
                    { 4L, 4L, 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AchievementRecord",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AchievementRecord",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AchievementRecord",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AchievementRecord",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AchievementThresholds",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "AchievementType",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AchievementType",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AchievementType",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AchievementType",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(148));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(323));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(325));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(328));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(332));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(338));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(341));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(346));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(414));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(436));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(441));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(445));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(531));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(535));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(541));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 31, 19, 49, 40, 484, DateTimeKind.Local).AddTicks(545));
        }
    }
}
