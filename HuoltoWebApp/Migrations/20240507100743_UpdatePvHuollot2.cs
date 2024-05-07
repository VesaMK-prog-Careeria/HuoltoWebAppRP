using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class UpdatePvHuollot2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__PvHuollo__BE746FF09B0C99D1",
                table: "PvHuollot");

            migrationBuilder.DropColumn(
                name: "HuoltoID",
                table: "PvHuollot");

            migrationBuilder.AddColumn<int>(
                name: "HuoltoID",
                table: "PvHuollot",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__PvHuollo__BE746FF09B0C99D1",
                table: "PvHuollot",
                column: "HuoltoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__PvHuollo__BE746FF09B0C99D1",
                table: "PvHuollot");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "PvHuollot",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__PvHuollo__BE746FF09B0C99D1",
                table: "PvHuollot",
                column: "HuoltoID");
        }
    }
}
