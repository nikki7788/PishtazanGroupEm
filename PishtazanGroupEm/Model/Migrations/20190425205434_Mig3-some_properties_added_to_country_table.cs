using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Mig3some_properties_added_to_country_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndexImage",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillWorkingOption_FindingJobCVPrice",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillWorkingOption_MakingCVPrice",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillWorkingOption_MakingCoverLetterPrice",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillWorkingOption_MakingLinkedInPrice",
                table: "Country",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexImage",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SkillWorkingOption_FindingJobCVPrice",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SkillWorkingOption_MakingCVPrice",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SkillWorkingOption_MakingCoverLetterPrice",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SkillWorkingOption_MakingLinkedInPrice",
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
        }
    }
}
