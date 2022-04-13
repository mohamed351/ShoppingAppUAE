using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppUAE.Migrations
{
    public partial class addShippingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shippingAndDeliveries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingAndDeliveries", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shippingAndDeliveries");
        }
    }
}
