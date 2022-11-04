using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesIdentity.DataAccess.Data.Migrations
{
    public partial class AddUserRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRol",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRol", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "User",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@admin.com", "Admin", "Admin", "1234", "admin" },
                    { 2, "pepe@admin.com", "Pepe", "Pepe", "1234", "pepe" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRol",
                columns: new[] { "Id", "IdClient", "IdRol" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRol",
                schema: "Identity");
        }
    }
}
