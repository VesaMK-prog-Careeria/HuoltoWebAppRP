using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class UpdateHuoltoIdAsIdentityHuoltopyyntoPv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__PvHuolto__BE746FF0B0DE434F",
                table: "PvHuoltopyyntö");

            migrationBuilder.DropColumn(
                name: "HuoltoID",
                table: "PvHuoltopyyntö");

            migrationBuilder.AddColumn<int>(
                name: "HuoltoID",
                table: "PvHuoltopyyntö",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__PvHuolto__BE746FF0B0DE434F",
                table: "PvHuoltopyyntö",
                column: "HuoltoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__PvHuolto__BE746FF0B0DE434F",
                table: "PvHuoltopyyntö");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "PvHuoltopyyntö",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__PvHuolto__BE746FF0B0DE434F",
                table: "PvHuoltopyyntö",
                column: "HuoltoID");
        }
    }
}
