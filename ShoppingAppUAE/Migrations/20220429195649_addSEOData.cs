using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class addSEOData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SEOData",
                columns: table => new
                {
                    ActionName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnotherTags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEOData", x => new { x.ActionName, x.ControllerName });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SEOData");
        }
    }
}
