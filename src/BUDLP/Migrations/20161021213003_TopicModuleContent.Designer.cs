using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BUDLP.Data;

namespace BUDLP.Migrations
{
    [DbContext(typeof(PlatformDbContext))]
    [Migration("20161021213003_TopicModuleContent")]
    partial class TopicModuleContent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BUDLP.Models.AuthUserModels.AuthUserProfile", b =>
                {
                    b.Property<int>("AuthUserProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio");

                    b.Property<string>("City");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Gender");

                    b.Property<string>("Goals");

                    b.Property<string>("LevelOfEducation");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.Property<int>("YearOfBirth");

                    b.HasKey("AuthUserProfileId");

                    b.ToTable("AuthUserProfiles");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int>("Language");

                    b.Property<string>("PreReq");

                    b.Property<string>("TopicName");

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.TopicModule", b =>
                {
                    b.Property<int>("TopicModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TopicId");

                    b.Property<string>("TopicModuleTitle");

                    b.HasKey("TopicModuleId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicModule");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.TopicModuleContent", b =>
                {
                    b.Property<int>("TopicModuleContentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ModuleContent");

                    b.Property<int>("TopicModuleContentType");

                    b.Property<int>("TopicModuleId");

                    b.HasKey("TopicModuleContentId");

                    b.HasIndex("TopicModuleId");

                    b.ToTable("TopicModuleContent");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.UserProfileTopic", b =>
                {
                    b.Property<int>("UserProfileTopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PastExperience");

                    b.Property<bool>("ToLearn");

                    b.Property<int>("TopicId");

                    b.Property<string>("UserProfileId");

                    b.HasKey("UserProfileTopicId");

                    b.ToTable("UserProfileTopics");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.TopicModule", b =>
                {
                    b.HasOne("BUDLP.Models.TopicModels.Topic")
                        .WithMany("TopicModules")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.TopicModuleContent", b =>
                {
                    b.HasOne("BUDLP.Models.TopicModels.TopicModule")
                        .WithMany("TopicModules")
                        .HasForeignKey("TopicModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
