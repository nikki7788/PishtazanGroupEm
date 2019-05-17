using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Mig5changing_tables_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryCoverImage_Country_CountryId",
                table: "CountryCoverImage");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryCoverVideo_Country_CountryId",
                table: "CountryCoverVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmigrateCountry_Country_CountryId",
                table: "EmigrateCountry");

            migrationBuilder.DropForeignKey(
                name: "FK_EmigrateCountry_EmigrationType_EmigrationTypeId",
                table: "EmigrateCountry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmigrationType",
                table: "EmigrationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmigrateCountry",
                table: "EmigrateCountry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryCoverVideo",
                table: "CountryCoverVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryCoverImage",
                table: "CountryCoverImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TouristOption_BookingHotel",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TouristOption_BookingPlane",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TouristOption_TakingEmbassyInterview",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TouristOption_TakingInvitation",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TouristOption_TakingTrainTicket",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TouristOption_TravelArrangment",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "EmigrationType",
                newName: "EmigrationTypes");

            migrationBuilder.RenameTable(
                name: "EmigrateCountry",
                newName: "EmigrateCountrys");

            migrationBuilder.RenameTable(
                name: "CountryCoverVideo",
                newName: "CountryCoverVideos");

            migrationBuilder.RenameTable(
                name: "CountryCoverImage",
                newName: "CountryCoverImages");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_EmigrateCountry_EmigrationTypeId",
                table: "EmigrateCountrys",
                newName: "IX_EmigrateCountrys_EmigrationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmigrateCountry_CountryId",
                table: "EmigrateCountrys",
                newName: "IX_EmigrateCountrys_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryCoverVideo_CountryId",
                table: "CountryCoverVideos",
                newName: "IX_CountryCoverVideos_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryCoverImage_CountryId",
                table: "CountryCoverImages",
                newName: "IX_CountryCoverImages_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmigrationTypes",
                table: "EmigrationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmigrateCountrys",
                table: "EmigrateCountrys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryCoverVideos",
                table: "CountryCoverVideos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryCoverImages",
                table: "CountryCoverImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryCoverImages_Countries_CountryId",
                table: "CountryCoverImages",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryCoverVideos_Countries_CountryId",
                table: "CountryCoverVideos",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmigrateCountrys_Countries_CountryId",
                table: "EmigrateCountrys",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmigrateCountrys_EmigrationTypes_EmigrationTypeId",
                table: "EmigrateCountrys",
                column: "EmigrationTypeId",
                principalTable: "EmigrationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryCoverImages_Countries_CountryId",
                table: "CountryCoverImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryCoverVideos_Countries_CountryId",
                table: "CountryCoverVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_EmigrateCountrys_Countries_CountryId",
                table: "EmigrateCountrys");

            migrationBuilder.DropForeignKey(
                name: "FK_EmigrateCountrys_EmigrationTypes_EmigrationTypeId",
                table: "EmigrateCountrys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmigrationTypes",
                table: "EmigrationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmigrateCountrys",
                table: "EmigrateCountrys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryCoverVideos",
                table: "CountryCoverVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryCoverImages",
                table: "CountryCoverImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "EmigrationTypes",
                newName: "EmigrationType");

            migrationBuilder.RenameTable(
                name: "EmigrateCountrys",
                newName: "EmigrateCountry");

            migrationBuilder.RenameTable(
                name: "CountryCoverVideos",
                newName: "CountryCoverVideo");

            migrationBuilder.RenameTable(
                name: "CountryCoverImages",
                newName: "CountryCoverImage");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "IX_EmigrateCountrys_EmigrationTypeId",
                table: "EmigrateCountry",
                newName: "IX_EmigrateCountry_EmigrationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmigrateCountrys_CountryId",
                table: "EmigrateCountry",
                newName: "IX_EmigrateCountry_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryCoverVideos_CountryId",
                table: "CountryCoverVideo",
                newName: "IX_CountryCoverVideo_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryCoverImages_CountryId",
                table: "CountryCoverImage",
                newName: "IX_CountryCoverImage_CountryId");

            migrationBuilder.AddColumn<int>(
                name: "TouristOption_BookingHotel",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TouristOption_BookingPlane",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TouristOption_TakingEmbassyInterview",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TouristOption_TakingInvitation",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TouristOption_TakingTrainTicket",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TouristOption_TravelArrangment",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmigrationType",
                table: "EmigrationType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmigrateCountry",
                table: "EmigrateCountry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryCoverVideo",
                table: "CountryCoverVideo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryCoverImage",
                table: "CountryCoverImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryCoverImage_Country_CountryId",
                table: "CountryCoverImage",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryCoverVideo_Country_CountryId",
                table: "CountryCoverVideo",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmigrateCountry_Country_CountryId",
                table: "EmigrateCountry",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmigrateCountry_EmigrationType_EmigrationTypeId",
                table: "EmigrateCountry",
                column: "EmigrationTypeId",
                principalTable: "EmigrationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
