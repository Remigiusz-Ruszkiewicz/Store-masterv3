using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.Migrations
{
    public partial class categoryproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryNameId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryNameId",
                table: "Products",
                column: "CategoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategory_CategoryNameId",
                table: "Products",
                column: "CategoryNameId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategory_CategoryNameId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryNameId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryNameId",
                table: "Products");
        }
    }
}
