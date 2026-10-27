using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroGuard.Migrations
{
    /// <inheritdoc />
    public partial class InitialSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SurveyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveyStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyID);
                });

            migrationBuilder.CreateTable(
                name: "SurveyImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyImages", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_SurveyImages_Surveys_SurveyID",
                        column: x => x.SurveyID,
                        principalTable: "Surveys",
                        principalColumn: "SurveyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyImages_SurveyID",
                table: "SurveyImages",
                column: "SurveyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyImages");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
