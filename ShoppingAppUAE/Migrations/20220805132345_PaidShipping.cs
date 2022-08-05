using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class PaidShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaidShipping",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidShipping",
                table: "Categories");
        }
    }
}
