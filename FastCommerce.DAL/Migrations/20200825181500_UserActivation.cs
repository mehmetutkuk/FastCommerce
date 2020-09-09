using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class UserActivation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivationChannelType",
                table: "UserActivations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationChannelType",
                table: "UserActivations");
        }
    }
}
