using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class AddAddressName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "Addresses");
        }
    }
}
