using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class SailioHuoltoPvHuoltoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HuoltoID",
                table: "SäiliöHuollot",
                newName: "HuollonID");

            migrationBuilder.RenameColumn(
                name: "HuoltoID",
                table: "PvHuollot",
                newName: "HuollonID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HuoltoPvm",
                table: "SäiliöHuollot",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HuollonKuvaus",
                table: "SäiliöHuollot",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HuoltoPvm",
                table: "PvHuollot",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HuollonKuvaus",
                table: "PvHuollot",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HuollonID",
                table: "SäiliöHuollot",
                newName: "HuoltoID");

            migrationBuilder.RenameColumn(
                name: "HuollonID",
                table: "PvHuollot",
                newName: "HuoltoID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HuoltoPvm",
                table: "SäiliöHuollot",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HuollonKuvaus",
                table: "SäiliöHuollot",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HuoltoPvm",
                table: "PvHuollot",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HuollonKuvaus",
                table: "PvHuollot",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
