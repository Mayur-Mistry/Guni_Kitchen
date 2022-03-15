using Microsoft.EntityFrameworkCore.Migrations;

namespace Guni_Kitchen_WebApp.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImageType",
                table: "Products",
                newName: "ProductImageContentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImageContentType",
                table: "Products",
                newName: "ProductImageType");
        }
    }
}
