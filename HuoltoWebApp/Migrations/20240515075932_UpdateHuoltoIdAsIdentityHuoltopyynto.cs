using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class UpdateHuoltoIdAsIdentityHuoltopyynto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__AutoHuol__BE746FF02A295F37",
                table: "AutoHuoltopyyntö");

            migrationBuilder.DropColumn(
                name: "HuoltoID",
                table: "AutoHuoltopyyntö");

            migrationBuilder.AddColumn<int>(
                name: "HuoltoID",
                table: "AutoHuoltopyyntö",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__AutoHuol__BE746FF02A295F37",
                table: "AutoHuoltopyyntö",
                column: "HuoltoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__AutoHuol__BE746FF02A295F37",
                table: "AutoHuoltopyyntö");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "AutoHuoltopyyntö",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__AutoHuol__BE746FF02A295F37",
                table: "AutoHuoltopyyntö",
                column: "HuoltoID");
        }
    }
}
