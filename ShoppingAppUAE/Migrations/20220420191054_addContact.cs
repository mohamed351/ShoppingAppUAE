using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class addContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionSEOArabic",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionSEOEnglish",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsArabic",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsEnglish",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionSEOArabic",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "DescriptionSEOEnglish",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "KeyWordsArabic",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "KeyWordsEnglish",
                table: "ContactInfos");
        }
    }
}
