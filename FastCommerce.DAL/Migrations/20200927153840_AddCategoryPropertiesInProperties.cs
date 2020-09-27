using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class AddCategoryPropertiesInProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryPropertiesId",
                table: "Properties",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryPropertiesId",
                table: "Properties");
        }
    }
}
