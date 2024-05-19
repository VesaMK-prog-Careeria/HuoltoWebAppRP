using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class UpdateHuoltoIdAsIdentityHuoltopyyntoSailio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__SäiliöHu__BE746FF078774EDD",
                table: "SäiliöHuoltopyyntö");

            migrationBuilder.DropColumn(
                name: "HuoltoID",
                table: "SäiliöHuoltopyyntö");

            migrationBuilder.AddColumn<int>(
                name: "HuoltoID",
                table: "SäiliöHuoltopyyntö",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SäiliöHu__BE746FF078774EDD",
                table: "SäiliöHuoltopyyntö",
                column: "HuoltoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__SäiliöHu__BE746FF078774EDD",
                table: "SäiliöHuoltopyyntö");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "SäiliöHuoltopyyntö",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SäiliöHu__BE746FF078774EDD",
                table: "SäiliöHuoltopyyntö",
                column: "HuoltoID");
        }
    }
}
