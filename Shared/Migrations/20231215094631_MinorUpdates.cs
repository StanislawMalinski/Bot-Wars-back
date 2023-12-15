using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class MinorUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(758));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(939));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(942));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(947));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(967));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 6L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 7L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 8L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 9L,
                column: "isBanned",
                value: true);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 10L,
                column: "isBanned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1022));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1067));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1072));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1076));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1079));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1082));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1085));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 15, 10, 46, 30, 426, DateTimeKind.Local).AddTicks(1089));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "Players");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8221));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8233));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8236));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8238));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8241));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(8244));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9603));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9658));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9671));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9682));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9693));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9711));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9729));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9734));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2023, 12, 9, 18, 48, 10, 898, DateTimeKind.Local).AddTicks(9747));
        }
    }
}
