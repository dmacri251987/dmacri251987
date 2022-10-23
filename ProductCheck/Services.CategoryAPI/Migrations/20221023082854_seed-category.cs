using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Services.CategoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "DMCategoriesAPI",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DMCategoriesAPI",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "DMCategoriesAPI",
                columns: new[] { "CategoryID", "CategoryName", "Description", "Picture" },
                values: new object[,]
                {
                    { 1, "Beverages", "Soft drinks, coffees, teas, beers, and ales", null },
                    { 2, "Condiments", "Sweet and savory sauces, relishes, spreads, and seasonings", null },
                    { 3, "Confections", "Desserts, candies, and sweet breads", null },
                    { 4, "Dairy Products", "Cheeses", null },
                    { 5, "Grains/Cereals", "Breads, crackers, pasta, and cereal", null },
                    { 6, "Meat/Poultry", "Prepared meats", null },
                    { 7, "Produce", "Dried fruit and bean curd", null },
                    { 8, "Seafood", "Seaweed and fish", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DMCategoriesAPI",
                keyColumn: "CategoryID",
                keyValue: 8);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "DMCategoriesAPI",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DMCategoriesAPI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
