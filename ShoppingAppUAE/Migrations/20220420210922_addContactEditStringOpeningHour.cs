using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class addContactEditStringOpeningHour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpenTo",
                table: "ContactInfos",
                newName: "OpenHours");

            migrationBuilder.RenameColumn(
                name: "OpenFrom",
                table: "ContactInfos",
                newName: "HolyDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpenHours",
                table: "ContactInfos",
                newName: "OpenTo");

            migrationBuilder.RenameColumn(
                name: "HolyDays",
                table: "ContactInfos",
                newName: "OpenFrom");
        }
    }
}
