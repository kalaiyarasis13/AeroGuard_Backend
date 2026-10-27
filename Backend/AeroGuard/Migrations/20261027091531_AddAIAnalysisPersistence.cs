using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroGuard.Migrations
{
    /// <inheritdoc />
    public partial class AddAIAnalysisPersistence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AIAnalysisRuns",
                columns: table => new
                {
                    AnalysisRunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageID = table.Column<int>(type: "int", nullable: false),
                    AnalysisType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ResultSource = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModelVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIAnalysisRuns", x => x.AnalysisRunID);
                    table.ForeignKey(
                        name: "FK_AIAnalysisRuns_SurveyImages_ImageID",
                        column: x => x.ImageID,
                        principalTable: "SurveyImages",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIFindings",
                columns: table => new
                {
                    FindingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalysisRunID = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Confidence = table.Column<double>(type: "float", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    Y1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    Y2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIFindings", x => x.FindingID);
                    table.ForeignKey(
                        name: "FK_AIFindings_AIAnalysisRuns_AnalysisRunID",
                        column: x => x.AnalysisRunID,
                        principalTable: "AIAnalysisRuns",
                        principalColumn: "AnalysisRunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIAnalysisRuns_ImageID",
                table: "AIAnalysisRuns",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_AIFindings_AnalysisRunID",
                table: "AIFindings",
                column: "AnalysisRunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIFindings");

            migrationBuilder.DropTable(
                name: "AIAnalysisRuns");
        }
    }
}
