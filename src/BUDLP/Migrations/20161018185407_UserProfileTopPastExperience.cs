using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class UserProfileTopPastExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PastExperience",
                table: "UserProfileTopics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PastExperience",
                table: "UserProfileTopics");
        }
    }
}
