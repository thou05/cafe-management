using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cafe_management.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProducts_TbCategories_CategoryIdNavigationId",
                table: "TbProducts");

            migrationBuilder.DropIndex(
                name: "IX_TbProducts_CategoryIdNavigationId",
                table: "TbProducts");

            migrationBuilder.DropColumn(
                name: "CategoryIdNavigationId",
                table: "TbProducts");

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_CategoryId",
                table: "TbProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProducts_TbCategories_CategoryId",
                table: "TbProducts",
                column: "CategoryId",
                principalTable: "TbCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProducts_TbCategories_CategoryId",
                table: "TbProducts");

            migrationBuilder.DropIndex(
                name: "IX_TbProducts_CategoryId",
                table: "TbProducts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryIdNavigationId",
                table: "TbProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_CategoryIdNavigationId",
                table: "TbProducts",
                column: "CategoryIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProducts_TbCategories_CategoryIdNavigationId",
                table: "TbProducts",
                column: "CategoryIdNavigationId",
                principalTable: "TbCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
