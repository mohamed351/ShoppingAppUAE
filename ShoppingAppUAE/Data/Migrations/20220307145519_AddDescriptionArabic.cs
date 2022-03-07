using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Data.Migrations
{
    public partial class AddDescriptionArabic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionArabic",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEnglish",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionArabic",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescriptionEnglish",
                table: "Products");
        }
    }
}
