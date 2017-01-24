using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class Remark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerRemark",
                table: "QuizOptions");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Quizzes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Quizzes");

            migrationBuilder.AddColumn<string>(
                name: "AnswerRemark",
                table: "QuizOptions",
                nullable: true);
        }
    }
}
