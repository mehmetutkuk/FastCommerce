using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class CategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Category_CategoryID",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Properties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategoryID",
                table: "Properties",
                newName: "IX_Properties_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Category_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Category_CategoryId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Properties",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                newName: "IX_Properties_CategoryID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Category_CategoryID",
                table: "Properties",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
