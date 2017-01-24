using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BUDLP.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthUserProfiles",
                columns: table => new
                {
                    AuthUserProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bio = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Goals = table.Column<string>(nullable: true),
                    LevelOfEducation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    YearOfBirth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserProfiles", x => x.AuthUserProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    COrder = table.Column<int>(nullable: false),
                    CPLUSOrder = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    Language = table.Column<int>(nullable: false),
                    MATLABOrder = table.Column<int>(nullable: false),
                    PreReq = table.Column<string>(nullable: true),
                    TopicName = table.Column<string>(nullable: true),
                    TotalTime = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "TopicModuleStates",
                columns: table => new
                {
                    TopicModuleStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthenticatedUserId = table.Column<string>(nullable: true),
                    FirstVisited = table.Column<DateTime>(nullable: true),
                    LastVisited = table.Column<DateTime>(nullable: true),
                    LearningState = table.Column<int>(nullable: false),
                    TimeSpent = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    TopicModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicModuleStates", x => x.TopicModuleStateId);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuizAnswerId = table.Column<int>(nullable: false),
                    QuizType = table.Column<int>(nullable: false),
                    TopicModuleContentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "UserQuizResponses",
                columns: table => new
                {
                    UserQuizResponseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Correct = table.Column<bool>(nullable: false),
                    QuizId = table.Column<int>(nullable: false),
                    QuizOptionId = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizResponses", x => x.UserQuizResponseId);
                });

            migrationBuilder.CreateTable(
                name: "TopicModules",
                columns: table => new
                {
                    TopicModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Language = table.Column<int>(nullable: false),
                    NextTopicModuleUrl = table.Column<string>(nullable: true),
                    PrevTopicModuleUrl = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(nullable: false),
                    TopicModuleTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicModules", x => x.TopicModuleId);
                    table.ForeignKey(
                        name: "FK_TopicModules_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileTopics",
                columns: table => new
                {
                    UserProfileTopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ListOrder = table.Column<string>(nullable: true),
                    PastExperience = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ToLearn = table.Column<bool>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileTopics", x => x.UserProfileTopicId);
                    table.ForeignKey(
                        name: "FK_UserProfileTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizOptions",
                columns: table => new
                {
                    QuizOptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuizId = table.Column<int>(nullable: false),
                    QuizOptionText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizOptions", x => x.QuizOptionId);
                    table.ForeignKey(
                        name: "FK_QuizOptions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicModuleContent",
                columns: table => new
                {
                    TopicModuleContentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Class = table.Column<int>(nullable: false),
                    ContentGroup = table.Column<int>(nullable: false),
                    Language = table.Column<int>(nullable: false),
                    ModuleContent = table.Column<string>(nullable: true),
                    ModuleDescription = table.Column<string>(nullable: true),
                    ModuleTime = table.Column<float>(nullable: false),
                    ModuleTitle = table.Column<string>(nullable: true),
                    PriorLearned = table.Column<int>(nullable: false),
                    QuizId = table.Column<int>(nullable: false),
                    RelativeOrdering = table.Column<int>(nullable: false),
                    TopicModuleContentType = table.Column<int>(nullable: false),
                    TopicModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicModuleContent", x => x.TopicModuleContentId);
                    table.ForeignKey(
                        name: "FK_TopicModuleContent_TopicModules_TopicModuleId",
                        column: x => x.TopicModuleId,
                        principalTable: "TopicModules",
                        principalColumn: "TopicModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLearningStates",
                columns: table => new
                {
                    UserLearningStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthenticatedUserId = table.Column<string>(nullable: true),
                    FirstVisited = table.Column<DateTime>(nullable: true),
                    LastVisited = table.Column<DateTime>(nullable: true),
                    LearningState = table.Column<int>(nullable: false),
                    TimeSpent = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    TopicModuleContentId = table.Column<int>(nullable: false),
                    TopicModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLearningStates", x => x.UserLearningStateId);
                    table.ForeignKey(
                        name: "FK_UserLearningStates_TopicModuleContent_TopicModuleContentId",
                        column: x => x.TopicModuleContentId,
                        principalTable: "TopicModuleContent",
                        principalColumn: "TopicModuleContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicModules_TopicId",
                table: "TopicModules",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicModuleContent_TopicModuleId",
                table: "TopicModuleContent",
                column: "TopicModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileTopics_TopicId",
                table: "UserProfileTopics",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizOptions_QuizId",
                table: "QuizOptions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLearningStates_TopicModuleContentId",
                table: "UserLearningStates",
                column: "TopicModuleContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUserProfiles");

            migrationBuilder.DropTable(
                name: "UserProfileTopics");

            migrationBuilder.DropTable(
                name: "TopicModuleStates");

            migrationBuilder.DropTable(
                name: "QuizOptions");

            migrationBuilder.DropTable(
                name: "UserQuizResponses");

            migrationBuilder.DropTable(
                name: "UserLearningStates");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "TopicModuleContent");

            migrationBuilder.DropTable(
                name: "TopicModules");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
