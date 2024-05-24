using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class DropCreateTableKuvat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kuva");

            migrationBuilder.CreateTable(
                name: "Kuva",
                columns: table => new
                {
                    KuvaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KuvaNimi = table.Column<string>(maxLength: 255, nullable: false),
                    KuvaData = table.Column<byte[]>(nullable: false),
                    AutoInfoId = table.Column<int>(nullable: true),
                    SäiliöInfoId = table.Column<int>(nullable: true),
                    PvInfoId = table.Column<int>(nullable: true),
                    KuvaType = table.Column<string>(maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Kuva_SäiliöInfo",
                        column: x => x.SäiliöInfoId,
                        principalTable: "SäiliöInfo",
                        principalColumn: "SäiliöInfoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kuva_PvInfo",
                        column: x => x.PvInfoId,
                        principalTable: "PvInfo",
                        principalColumn: "PvInfoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kuva_AutoInfoId",
                table: "Kuva",
                column: "AutoInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Kuva_SäiliöInfoId",
                table: "Kuva",
                column: "SäiliöInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Kuva_PvInfoId",
                table: "Kuva",
                column: "PvInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Kuva_EntityId",
                table: "Kuva",
                column: "EntityId");
        }
        

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kuva");

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
    }
}
