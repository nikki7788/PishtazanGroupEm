using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Mig6Add_Abstract_prop_to_the_countrynotmapped_removed_from_TouristOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Countries",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Countries");
        }
    }
}
