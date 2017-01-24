using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class QuizAnswerJSONPrep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizAnswerId",
                table: "Quizzes");

            migrationBuilder.AddColumn<string>(
                name: "QuizAnswer",
                table: "Quizzes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizAnswer",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "QuizAnswerId",
                table: "Quizzes",
                nullable: false,
                defaultValue: 0);
        }
    }
}
