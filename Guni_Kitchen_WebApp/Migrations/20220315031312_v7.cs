using Microsoft.EntityFrameworkCore.Migrations;

namespace Guni_Kitchen_WebApp.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_image",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductImageFileUrl",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImageType",
                table: "Products",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageFileUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImageType",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Product_image",
                table: "Products",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
