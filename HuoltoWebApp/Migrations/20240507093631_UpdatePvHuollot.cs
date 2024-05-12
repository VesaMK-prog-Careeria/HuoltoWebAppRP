using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class UpdatePvHuollot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PvHuollot",
                table: "PvHuollot");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "PvHuollot",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PvHuollot",
                table: "PvHuollot",
                column: "HuoltoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PvHuollot",
                table: "PvHuollot");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "PvHuollot",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PvHuollot",
                table: "PvHuollot",
                column: "HuoltoID");
        }
    }
}
