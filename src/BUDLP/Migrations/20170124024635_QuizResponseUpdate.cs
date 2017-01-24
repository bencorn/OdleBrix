using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class QuizResponseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizOptionId",
                table: "UserQuizResponses");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "UserQuizResponses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "UserQuizResponses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "UserQuizResponses");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "UserQuizResponses");

            migrationBuilder.AddColumn<int>(
                name: "QuizOptionId",
                table: "UserQuizResponses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
