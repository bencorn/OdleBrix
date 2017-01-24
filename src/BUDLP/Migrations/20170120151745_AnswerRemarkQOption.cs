using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class AnswerRemarkQOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerRemark",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "Quizzes");

            migrationBuilder.AddColumn<string>(
                name: "AnswerRemark",
                table: "QuizOptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerRemark",
                table: "QuizOptions");

            migrationBuilder.AddColumn<string>(
                name: "AnswerRemark",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "Quizzes",
                nullable: true);
        }
    }
}
