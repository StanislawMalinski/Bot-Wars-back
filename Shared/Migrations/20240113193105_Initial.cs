using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6775));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6778));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6781));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6784));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6788));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6791));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6794));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6797));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6800));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6843));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6846));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6850));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6853));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6857));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6859));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6862));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6865));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 31, 5, 102, DateTimeKind.Local).AddTicks(6869));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3293));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3296));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3298));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3301));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3304));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3306));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3309));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3312));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3342));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3352));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3355));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3357));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3361));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3363));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3366));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 13, 20, 28, 23, 617, DateTimeKind.Local).AddTicks(3372));
        }
    }
}
