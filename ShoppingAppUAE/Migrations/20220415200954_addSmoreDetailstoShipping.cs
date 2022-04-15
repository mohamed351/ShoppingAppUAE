using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class addSmoreDetailstoShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalKgAfterExcedingTheLimit",
                table: "shippingAndDeliveries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FirstWeightLimit",
                table: "shippingAndDeliveries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterExcedingTheLimit",
                table: "shippingAndDeliveries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalKgAfterExcedingTheLimit",
                table: "shippingAndDeliveries");

            migrationBuilder.DropColumn(
                name: "FirstWeightLimit",
                table: "shippingAndDeliveries");

            migrationBuilder.DropColumn(
                name: "PriceAfterExcedingTheLimit",
                table: "shippingAndDeliveries");
        }
    }
}
