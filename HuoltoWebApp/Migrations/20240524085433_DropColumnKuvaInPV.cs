using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class DropColumnKuvaInPV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kuva",
                table: "PvInfo");

            migrationBuilder.DropColumn(
                name: "Kuva",
                table: "SäiliöInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Kuva",
                table: "PvInfo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Kuva",
                table: "SäiliöInfo",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
