using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class addSEOData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SEOData",
                table: "SEOData");

            migrationBuilder.RenameTable(
                name: "SEOData",
                newName: "SEODatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEODatas",
                table: "SEODatas",
                columns: new[] { "ActionName", "ControllerName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SEODatas",
                table: "SEODatas");

            migrationBuilder.RenameTable(
                name: "SEODatas",
                newName: "SEOData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SEOData",
                table: "SEOData",
                columns: new[] { "ActionName", "ControllerName" });
        }
    }
}
