using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class RenamedPropertiesIdToPropertyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProperties_Properties_PropertyID",
                table: "CategoryProperties");

            migrationBuilder.DropColumn(
                name: "PropertiesId",
                table: "CategoryProperties");

            migrationBuilder.RenameColumn(
                name: "PropertyID",
                table: "CategoryProperties",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProperties_PropertyID",
                table: "CategoryProperties",
                newName: "IX_CategoryProperties_PropertyId");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "CategoryProperties",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProperties_Properties_PropertyId",
                table: "CategoryProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProperties_Properties_PropertyId",
                table: "CategoryProperties");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "CategoryProperties",
                newName: "PropertyID");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryProperties_PropertyId",
                table: "CategoryProperties",
                newName: "IX_CategoryProperties_PropertyID");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyID",
                table: "CategoryProperties",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PropertiesId",
                table: "CategoryProperties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProperties_Properties_PropertyID",
                table: "CategoryProperties",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
