using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class QuizBooleanContentModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "TopicModuleContent");

            migrationBuilder.AddColumn<bool>(
                name: "Quiz",
                table: "TopicModuleContent",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quiz",
                table: "TopicModuleContent");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "TopicModuleContent",
                nullable: false,
                defaultValue: 0);
        }
    }
}
