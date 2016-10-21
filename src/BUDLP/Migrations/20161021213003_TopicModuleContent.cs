using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BUDLP.Migrations
{
    public partial class TopicModuleContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicModule",
                columns: table => new
                {
                    TopicModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TopicId = table.Column<int>(nullable: false),
                    TopicModuleTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicModule", x => x.TopicModuleId);
                    table.ForeignKey(
                        name: "FK_TopicModule_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicModuleContent",
                columns: table => new
                {
                    TopicModuleContentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleContent = table.Column<string>(nullable: true),
                    TopicModuleContentType = table.Column<int>(nullable: false),
                    TopicModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicModuleContent", x => x.TopicModuleContentId);
                    table.ForeignKey(
                        name: "FK_TopicModuleContent_TopicModule_TopicModuleId",
                        column: x => x.TopicModuleId,
                        principalTable: "TopicModule",
                        principalColumn: "TopicModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicModule_TopicId",
                table: "TopicModule",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicModuleContent_TopicModuleId",
                table: "TopicModuleContent",
                column: "TopicModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicModuleContent");

            migrationBuilder.DropTable(
                name: "TopicModule");
        }
    }
}
