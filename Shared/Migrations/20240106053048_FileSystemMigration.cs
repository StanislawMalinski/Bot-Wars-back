using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class FileSystemMigration : Migration
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

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7132));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7135));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7145));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7147));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastModification",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7153));

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
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7262));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PostedDate",
                value: new DateTime(2024, 1, 6, 6, 30, 47, 891, DateTimeKind.Local).AddTicks(7269));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

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
