using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesProduct.DataAccess.Data.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    QuantityPerUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitsInStock = table.Column<short>(type: "smallint", nullable: true),
                    UnitsOnOrder = table.Column<short>(type: "smallint", nullable: true),
                    ReorderLevel = table.Column<short>(type: "smallint", nullable: true),
                    Discontinued = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.InsertData(
                schema: "Product",
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "Discontinued", "ProductName", "QuantityPerUnit", "ReorderLevel", "SupplierID", "UnitPrice", "UnitsInStock", "UnitsOnOrder" },
                values: new object[,]
                {
                    { 1, 1, false, "Chai", "10 boxes x 20 bags", (short)10, 1, 18m, (short)39, (short)0 },
                    { 2, 1, false, "Chang", "24 - 12 oz bottles", (short)25, 1, 19m, (short)17, (short)40 },
                    { 3, 2, false, "Aniseed Syrup", "12 - 550 ml bottles", (short)25, 1, 10m, (short)13, (short)70 },
                    { 4, 2, false, "Chef Anton's Cajun Seasoning", "48 - 6 oz jars", (short)0, 2, 22m, (short)53, (short)0 },
                    { 5, 2, true, "Chef Anton's Gumbo Mix", "36 boxes", (short)0, 2, 21m, (short)0, (short)0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "Product");
        }
    }
}
