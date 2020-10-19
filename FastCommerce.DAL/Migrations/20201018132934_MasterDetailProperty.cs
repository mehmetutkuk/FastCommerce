using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FastCommerce.DAL.Migrations
{
    public partial class MasterDetailProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockProperties");

            migrationBuilder.DropColumn(
                name: "PropertyValue",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PropertyDetail",
                columns: table => new
                {
                    PropertyDetailId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(nullable: false),
                    PropertyValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetail", x => x.PropertyDetailId);
                    table.ForeignKey(
                        name: "FK_PropertyDetail_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPropertyCombinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyDetailId = table.Column<int>(nullable: false),
                    PropertyID = table.Column<int>(nullable: true),
                    StockId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPropertyCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockPropertyCombinations_PropertyDetail_PropertyDetailId",
                        column: x => x.PropertyDetailId,
                        principalTable: "PropertyDetail",
                        principalColumn: "PropertyDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPropertyCombinations_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockPropertyCombinations_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetail_PropertyId",
                table: "PropertyDetail",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPropertyCombinations_PropertyDetailId",
                table: "StockPropertyCombinations",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPropertyCombinations_PropertyID",
                table: "StockPropertyCombinations",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPropertyCombinations_StockId",
                table: "StockPropertyCombinations",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPropertyCombinations");

            migrationBuilder.DropTable(
                name: "PropertyDetail");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "PropertyValue",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockProperties",
                columns: table => new
                {
                    StockPropertiesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyID = table.Column<int>(type: "integer", nullable: false),
                    StockId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockProperties", x => x.StockPropertiesId);
                    table.ForeignKey(
                        name: "FK_StockProperties_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockProperties_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockProperties_PropertyID",
                table: "StockProperties",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_StockProperties_StockId",
                table: "StockProperties",
                column: "StockId");
        }
    }
}
