using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class AddAutoInfoKuva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kuva",
                table: "AutoInfo");

            migrationBuilder.CreateTable(
                name: "Kuva",
                columns: table => new
                {
                    KuvaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KuvaNimi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KuvaData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AutoInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuva", x => x.KuvaID);
                    table.ForeignKey(
                        name: "FK_Kuva_AutoInfo",
                        column: x => x.AutoInfoId,
                        principalTable: "AutoInfo",
                        principalColumn: "AutoInfoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kuva_AutoInfoId",
                table: "Kuva",
                column: "AutoInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kuva");

            migrationBuilder.AddColumn<byte[]>(
                name: "Kuva",
                table: "AutoInfo",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
