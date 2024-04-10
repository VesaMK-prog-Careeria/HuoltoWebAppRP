using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Huoltopaikat",
                columns: table => new
                {
                    HuoltoPaikkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Huoltopaikka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Huoltopa__DADB2282F9D2430F", x => x.HuoltoPaikkaID);
                });

            migrationBuilder.CreateTable(
                name: "Muistutustyyppi",
                columns: table => new
                {
                    MuistutustyyppiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MuistutustyyppiNimi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muistutustyyppi", x => x.MuistutustyyppiID);
                });

            migrationBuilder.CreateTable(
                name: "PV",
                columns: table => new
                {
                    PvID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    REKNRO = table.Column<string>(name: "REK-NRO", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Katsastus = table.Column<DateTime>(type: "date", nullable: true),
                    ADR = table.Column<DateTime>(type: "date", nullable: true),
                    Välitarkastus = table.Column<DateTime>(type: "date", nullable: true),
                    Määräaikaistarkastus = table.Column<DateTime>(type: "date", nullable: true),
                    PvInfoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PV", x => x.PvID);
                });

            migrationBuilder.CreateTable(
                name: "Säiliö",
                columns: table => new
                {
                    SäiliöID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SäiliöNro = table.Column<int>(type: "int", nullable: false),
                    Vakaus = table.Column<DateTime>(type: "date", nullable: true),
                    Välitarkastus = table.Column<DateTime>(type: "date", nullable: true),
                    Määräaikaistarkastus = table.Column<DateTime>(type: "date", nullable: true),
                    SäiliöInfoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Säiliö", x => x.SäiliöID);
                });

            migrationBuilder.CreateTable(
                name: "PvHuollot",
                columns: table => new
                {
                    HuoltoID = table.Column<int>(type: "int", nullable: false),
                    PvID = table.Column<int>(type: "int", nullable: false),
                    HuoltoPvm = table.Column<DateTime>(type: "date", nullable: true),
                    HuoltoPaikkaID = table.Column<int>(type: "int", nullable: true),
                    HuollonKuvaus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PvHuollo__BE746FF09B0C99D1", x => x.HuoltoID);
                    table.ForeignKey(
                        name: "FK_PvHuollot_Pv",
                        column: x => x.PvID,
                        principalTable: "PV",
                        principalColumn: "PvID");
                });

            migrationBuilder.CreateTable(
                name: "PvHuoltopyyntö",
                columns: table => new
                {
                    HuoltoID = table.Column<int>(type: "int", nullable: false),
                    PvID = table.Column<int>(type: "int", nullable: false),
                    HuollonKuvaus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PvHuolto__BE746FF0B0DE434F", x => x.HuoltoID);
                    table.ForeignKey(
                        name: "FK_PvHuoltopyyntö_PV",
                        column: x => x.PvID,
                        principalTable: "PV",
                        principalColumn: "PvID");
                });

            migrationBuilder.CreateTable(
                name: "PvInfo",
                columns: table => new
                {
                    PvInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PvID = table.Column<int>(type: "int", nullable: false),
                    InfoTXT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PvInfo", x => x.PvInfoID);
                    table.ForeignKey(
                        name: "FK_PvInfo_Pv",
                        column: x => x.PvID,
                        principalTable: "PV",
                        principalColumn: "PvID");
                });

            migrationBuilder.CreateTable(
                name: "PvMuistutus",
                columns: table => new
                {
                    PvMuistutusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PvID = table.Column<int>(type: "int", nullable: true),
                    MuistutusPVM = table.Column<DateTime>(type: "date", nullable: true),
                    Muistutustyyppi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PvMuistu__1BD388FDD942EBB5", x => x.PvMuistutusID);
                    table.ForeignKey(
                        name: "FK_PvMuistutus_Muistutustyyppi",
                        column: x => x.Muistutustyyppi,
                        principalTable: "Muistutustyyppi",
                        principalColumn: "MuistutustyyppiID");
                    table.ForeignKey(
                        name: "FK_PvMuistutus_PV",
                        column: x => x.PvID,
                        principalTable: "PV",
                        principalColumn: "PvID");
                });

            migrationBuilder.CreateTable(
                name: "Auto",
                columns: table => new
                {
                    AutoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    REKNRO = table.Column<string>(name: "REK-NRO", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SäiliöID = table.Column<int>(type: "int", nullable: true),
                    Katsastus = table.Column<DateTime>(type: "date", nullable: true),
                    ADR = table.Column<DateTime>(type: "date", nullable: true),
                    Piirturi = table.Column<DateTime>(type: "date", nullable: true),
                    Alkolukko = table.Column<DateTime>(type: "date", nullable: true),
                    Nopeudenrajoitin = table.Column<DateTime>(type: "date", nullable: true),
                    AutoInfoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auto", x => x.AutoID);
                    table.ForeignKey(
                        name: "FK_Auto_Säiliö",
                        column: x => x.SäiliöID,
                        principalTable: "Säiliö",
                        principalColumn: "SäiliöID");
                });

            migrationBuilder.CreateTable(
                name: "SäiliöHuollot",
                columns: table => new
                {
                    HuoltoID = table.Column<int>(type: "int", nullable: false),
                    SäiliöID = table.Column<int>(type: "int", nullable: false),
                    HuoltoPvm = table.Column<DateTime>(type: "date", nullable: true),
                    HuoltoPaikkaID = table.Column<int>(type: "int", nullable: true),
                    HuollonKuvaus = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SäiliöHu__BE746FF042AD424C", x => x.HuoltoID);
                    table.ForeignKey(
                        name: "FK_SäiliöHuollot_Säiliö",
                        column: x => x.SäiliöID,
                        principalTable: "Säiliö",
                        principalColumn: "SäiliöID");
                });

            migrationBuilder.CreateTable(
                name: "SäiliöHuoltopyyntö",
                columns: table => new
                {
                    HuoltoID = table.Column<int>(type: "int", nullable: false),
                    SäiliöID = table.Column<int>(type: "int", nullable: false),
                    HuollonKuvaus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SäiliöHu__BE746FF078774EDD", x => x.HuoltoID);
                    table.ForeignKey(
                        name: "FK_SäiliöHuoltopyyntö_Säiliö",
                        column: x => x.SäiliöID,
                        principalTable: "Säiliö",
                        principalColumn: "SäiliöID");
                });

            migrationBuilder.CreateTable(
                name: "SäiliöInfo",
                columns: table => new
                {
                    SäiliöInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SäiliöID = table.Column<int>(type: "int", nullable: false),
                    InfoTXT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SäiliöInfo", x => x.SäiliöInfoID);
                    table.ForeignKey(
                        name: "FK_SäiliöInfo_Säiliö",
                        column: x => x.SäiliöID,
                        principalTable: "Säiliö",
                        principalColumn: "SäiliöID");
                });

            migrationBuilder.CreateTable(
                name: "SäiliöMuistutus",
                columns: table => new
                {
                    SäiliöMuistutusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SäiliöID = table.Column<int>(type: "int", nullable: true),
                    MuistutusPVM = table.Column<DateTime>(type: "date", nullable: true),
                    Muistutustyyppi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SäiliöMu__41E798D7F1E7485D", x => x.SäiliöMuistutusID);
                    table.ForeignKey(
                        name: "FK_SäiliöMuistutus_Muistutustyyppi",
                        column: x => x.Muistutustyyppi,
                        principalTable: "Muistutustyyppi",
                        principalColumn: "MuistutustyyppiID");
                    table.ForeignKey(
                        name: "FK_SäiliöMuistutus_Säiliö",
                        column: x => x.SäiliöID,
                        principalTable: "Säiliö",
                        principalColumn: "SäiliöID");
                });

            migrationBuilder.CreateTable(
                name: "AutoHuollot",
                columns: table => new
                {
                    HuollonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoID = table.Column<int>(type: "int", nullable: false),
                    HuoltoPvm = table.Column<DateTime>(type: "datetime", nullable: false),
                    HuoltopaikkaID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HuollonKuvaus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AutoHuol__5FD7B1EA7BC416AB", x => x.HuollonID);
                    table.ForeignKey(
                        name: "FK_AutoHuollot_Auto",
                        column: x => x.AutoID,
                        principalTable: "Auto",
                        principalColumn: "AutoID");
                });

            migrationBuilder.CreateTable(
                name: "AutoHuoltopyyntö",
                columns: table => new
                {
                    HuoltoID = table.Column<int>(type: "int", nullable: false),
                    AutoID = table.Column<int>(type: "int", nullable: false),
                    HuollonKuvaus = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AutoHuol__BE746FF02A295F37", x => x.HuoltoID);
                    table.ForeignKey(
                        name: "FK_AutoHuoltopyyntö_Auto",
                        column: x => x.AutoID,
                        principalTable: "Auto",
                        principalColumn: "AutoID");
                });

            migrationBuilder.CreateTable(
                name: "AutoInfo",
                columns: table => new
                {
                    AutoInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoID = table.Column<int>(type: "int", nullable: false),
                    InfoTXT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kuva = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoInfo", x => x.AutoInfoID);
                    table.ForeignKey(
                        name: "FK_AutoInfo_Auto",
                        column: x => x.AutoID,
                        principalTable: "Auto",
                        principalColumn: "AutoID");
                });

            migrationBuilder.CreateTable(
                name: "AutoMuistutus",
                columns: table => new
                {
                    AutoMuistutusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoID = table.Column<int>(type: "int", nullable: true),
                    MuistutusPVM = table.Column<DateTime>(type: "date", nullable: true),
                    Muistutustyyppi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AutoMuis__37A01D1C38893DA6", x => x.AutoMuistutusID);
                    table.ForeignKey(
                        name: "FK_AutoMuistutus_Auto",
                        column: x => x.AutoID,
                        principalTable: "Auto",
                        principalColumn: "AutoID");
                    table.ForeignKey(
                        name: "FK_AutoMuistutus_Muistutustyyppi",
                        column: x => x.Muistutustyyppi,
                        principalTable: "Muistutustyyppi",
                        principalColumn: "MuistutustyyppiID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auto_SäiliöID",
                table: "Auto",
                column: "SäiliöID");

            migrationBuilder.CreateIndex(
                name: "IX_AutoHuollot_AutoID",
                table: "AutoHuollot",
                column: "AutoID");

            migrationBuilder.CreateIndex(
                name: "IX_AutoHuoltopyyntö_AutoID",
                table: "AutoHuoltopyyntö",
                column: "AutoID");

            migrationBuilder.CreateIndex(
                name: "UQ__AutoInfo__6B232964F93818B5",
                table: "AutoInfo",
                column: "AutoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoMuistutus_AutoID",
                table: "AutoMuistutus",
                column: "AutoID");

            migrationBuilder.CreateIndex(
                name: "IX_AutoMuistutus_Muistutustyyppi",
                table: "AutoMuistutus",
                column: "Muistutustyyppi");

            migrationBuilder.CreateIndex(
                name: "IX_PvHuollot_PvID",
                table: "PvHuollot",
                column: "PvID");

            migrationBuilder.CreateIndex(
                name: "IX_PvHuoltopyyntö_PvID",
                table: "PvHuoltopyyntö",
                column: "PvID");

            migrationBuilder.CreateIndex(
                name: "UQ__PvInfo__9A008323337D0F32",
                table: "PvInfo",
                column: "PvID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PvMuistutus_Muistutustyyppi",
                table: "PvMuistutus",
                column: "Muistutustyyppi");

            migrationBuilder.CreateIndex(
                name: "IX_PvMuistutus_PvID",
                table: "PvMuistutus",
                column: "PvID");

            migrationBuilder.CreateIndex(
                name: "IX_SäiliöHuollot_SäiliöID",
                table: "SäiliöHuollot",
                column: "SäiliöID");

            migrationBuilder.CreateIndex(
                name: "IX_SäiliöHuoltopyyntö_SäiliöID",
                table: "SäiliöHuoltopyyntö",
                column: "SäiliöID");

            migrationBuilder.CreateIndex(
                name: "UQ__SäiliöIn__21300E71C1C62E57",
                table: "SäiliöInfo",
                column: "SäiliöID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SäiliöMuistutus_Muistutustyyppi",
                table: "SäiliöMuistutus",
                column: "Muistutustyyppi");

            migrationBuilder.CreateIndex(
                name: "IX_SäiliöMuistutus_SäiliöID",
                table: "SäiliöMuistutus",
                column: "SäiliöID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoHuollot");

            migrationBuilder.DropTable(
                name: "AutoHuoltopyyntö");

            migrationBuilder.DropTable(
                name: "AutoInfo");

            migrationBuilder.DropTable(
                name: "AutoMuistutus");

            migrationBuilder.DropTable(
                name: "Huoltopaikat");

            migrationBuilder.DropTable(
                name: "PvHuollot");

            migrationBuilder.DropTable(
                name: "PvHuoltopyyntö");

            migrationBuilder.DropTable(
                name: "PvInfo");

            migrationBuilder.DropTable(
                name: "PvMuistutus");

            migrationBuilder.DropTable(
                name: "SäiliöHuollot");

            migrationBuilder.DropTable(
                name: "SäiliöHuoltopyyntö");

            migrationBuilder.DropTable(
                name: "SäiliöInfo");

            migrationBuilder.DropTable(
                name: "SäiliöMuistutus");

            migrationBuilder.DropTable(
                name: "Auto");

            migrationBuilder.DropTable(
                name: "PV");

            migrationBuilder.DropTable(
                name: "Muistutustyyppi");

            migrationBuilder.DropTable(
                name: "Säiliö");
        }
    }
}
