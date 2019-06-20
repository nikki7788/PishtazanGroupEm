using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Mig10Abstract_Prop_added_to_the_EmigrationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "EmigrationTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "EmigrationTypes");
        }
    }
}
