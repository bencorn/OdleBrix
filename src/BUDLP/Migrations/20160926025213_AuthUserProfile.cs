using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BUDLP.Migrations
{
    public partial class AuthUserProfile : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUserProfiles");
        }
    }
}
