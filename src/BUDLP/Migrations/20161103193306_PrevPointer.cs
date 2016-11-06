using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BUDLP.Migrations
{
    public partial class PrevPointer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrevTopicModuleContentId",
                table: "TopicModuleContent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrevTopicModuleId",
                table: "TopicModules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevTopicModuleContentId",
                table: "TopicModuleContent");

            migrationBuilder.DropColumn(
                name: "PrevTopicModuleId",
                table: "TopicModules");
        }
    }
}
