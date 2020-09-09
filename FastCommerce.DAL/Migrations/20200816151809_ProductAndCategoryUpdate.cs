using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FastCommerce.DAL.Migrations
{
    public partial class ProductAndCategoryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyDetails");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyValue",
                table: "Properties",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryID",
                table: "Properties",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Category_CategoryID",
                table: "Properties",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Category_CategoryID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyValue",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateTable(
                name: "PropertyDetails",
                columns: table => new
                {
                    PropertyDetailID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyDetailName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetails", x => x.PropertyDetailID);
                });
        }
    }
}
