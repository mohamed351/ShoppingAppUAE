using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class AddOrdersMoreDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderAddress",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderEmail",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderPhone",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalEmail",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalPhone",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderAddress",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderEmail",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderPhone",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OriginalEmail",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OriginalPhone",
                table: "orders");
        }
    }
}
