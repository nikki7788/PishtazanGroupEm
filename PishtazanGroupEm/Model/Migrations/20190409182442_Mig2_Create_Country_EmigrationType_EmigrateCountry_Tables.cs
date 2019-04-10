using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Mig2_Create_Country_EmigrationType_EmigrateCountry_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmigrationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmigrationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryCoverImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageName = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCoverImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryCoverImage_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCoverVideo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VideoName = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCoverVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryCoverVideo_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmigrateCountry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    EmigrationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmigrateCountry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmigrateCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmigrateCountry_EmigrationType_EmigrationTypeId",
                        column: x => x.EmigrationTypeId,
                        principalTable: "EmigrationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryCoverImage_CountryId",
                table: "CountryCoverImage",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCoverVideo_CountryId",
                table: "CountryCoverVideo",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmigrateCountry_CountryId",
                table: "EmigrateCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmigrateCountry_EmigrationTypeId",
                table: "EmigrateCountry",
                column: "EmigrationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryCoverImage");

            migrationBuilder.DropTable(
                name: "CountryCoverVideo");

            migrationBuilder.DropTable(
                name: "EmigrateCountry");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "EmigrationType");
        }
    }
}
