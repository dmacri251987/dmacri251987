using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QuantityPerUnit",
                table: "DMProductsAPI",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "DMProductsAPI",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DMProductsAPI",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DMProductsAPI",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DMProductsAPI",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DMProductsAPI",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DMProductsAPI",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "QuantityPerUnit",
                table: "DMProductsAPI",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
