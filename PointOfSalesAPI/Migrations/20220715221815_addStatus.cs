using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSalesAPI.Migrations
{
    public partial class addStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerialStatus",
                table: "ProductBarCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialStatus",
                table: "ProductBarCodes");
        }
    }
}
