using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cafe_management.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProducts_TbCategories_ProductCategoryId",
                table: "TbProducts");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "TbProducts",
                newName: "CategoryIdNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_TbProducts_ProductCategoryId",
                table: "TbProducts",
                newName: "IX_TbProducts_CategoryIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProducts_TbCategories_CategoryIdNavigationId",
                table: "TbProducts",
                column: "CategoryIdNavigationId",
                principalTable: "TbCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProducts_TbCategories_CategoryIdNavigationId",
                table: "TbProducts");

            migrationBuilder.RenameColumn(
                name: "CategoryIdNavigationId",
                table: "TbProducts",
                newName: "ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TbProducts_CategoryIdNavigationId",
                table: "TbProducts",
                newName: "IX_TbProducts_ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProducts_TbCategories_ProductCategoryId",
                table: "TbProducts",
                column: "ProductCategoryId",
                principalTable: "TbCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
