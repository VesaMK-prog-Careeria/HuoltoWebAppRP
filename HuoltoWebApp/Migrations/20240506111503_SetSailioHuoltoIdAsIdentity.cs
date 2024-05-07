using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    public partial class SetSailioHuoltoIdAsIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__SäiliöHu__BE746FF0346755C9",
                table: "SäiliöHuollot");

            migrationBuilder.Sql(@"
                ALTER TABLE SäiliöHuollot ADD TempId int IDENTITY(1,1);
                UPDATE SäiliöHuollot SET TempId = HuoltoID;
                ALTER TABLE SäiliöHuollot DROP COLUMN HuoltoID;
                EXEC sp_rename 'SäiliöHuollot.TempId', 'HuoltoID', 'COLUMN';
            ");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SäiliöHu__BE746FF0346755C9",
                table: "SäiliöHuollot",
                column: "HuoltoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__SäiliöHu__BE746FF0346755C9",
                table: "SäiliöHuollot");

            migrationBuilder.AlterColumn<int>(
                name: "HuoltoID",
                table: "SäiliöHuollot",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SäiliöHu__BE746FF042AD424C",
                table: "SäiliöHuollot",
                column: "HuoltoID");
        }
    }
}
