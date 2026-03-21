using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniGuide.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.QuestionID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    InterGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterMarksPercent = table.Column<double>(type: "float", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetPerSemester = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HECRanking = table.Column<int>(type: "int", nullable: false),
                    EstablishedYear = table.Column<int>(type: "int", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasHostel = table.Column<bool>(type: "bit", nullable: false),
                    HasTransport = table.Column<bool>(type: "bit", nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.UniversityID);
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizResults",
                columns: table => new
                {
                    ResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendedField = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreBreakDown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuizResults", x => x.ResultID);
                    table.ForeignKey(
                        name: "FK_StudentQuizResults_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationYears = table.Column<int>(type: "int", nullable: false),
                    FeePerSemester = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MinMeritPercent = table.Column<double>(type: "float", nullable: false),
                    RequiredInterGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    Shift = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_Programs_Universities_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionDeadlines",
                columns: table => new
                {
                    DeadLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionOpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestRequired = table.Column<bool>(type: "bit", nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionDeadlines", x => x.DeadLineID);
                    table.ForeignKey(
                        name: "FK_AdmissionDeadlines_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionDeadlines_Universities_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CareerPaths",
                columns: table => new
                {
                    CareerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinSalaryPKR = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaxSalaryPKR = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IndustryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemandLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillRequired = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerPaths", x => x.CareerID);
                    table.ForeignKey(
                        name: "FK_CareerPaths_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedUniversities",
                columns: table => new
                {
                    SavedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedUniversities", x => x.SavedID);
                    table.ForeignKey(
                        name: "FK_SavedUniversities_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavedUniversities_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedUniversities_Universities_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionDeadlines_ProgramID",
                table: "AdmissionDeadlines",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionDeadlines_UniversityID",
                table: "AdmissionDeadlines",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPaths_ProgramID",
                table: "CareerPaths",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_UniversityID",
                table: "Programs",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedUniversities_ProgramID",
                table: "SavedUniversities",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedUniversities_StudentID",
                table: "SavedUniversities",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedUniversities_UniversityID",
                table: "SavedUniversities",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_StudentID",
                table: "StudentQuizResults",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionDeadlines");

            migrationBuilder.DropTable(
                name: "CareerPaths");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "SavedUniversities");

            migrationBuilder.DropTable(
                name: "StudentQuizResults");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
