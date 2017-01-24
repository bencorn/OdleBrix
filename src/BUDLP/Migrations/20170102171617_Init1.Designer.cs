using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BUDLP.Data;

namespace BUDLP.Migrations
{
    [DbContext(typeof(PlatformDbContext))]
    [Migration("20170102171617_Init1")]
    partial class Init1
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

                    b.Property<int>("COrder");

                    b.Property<int>("CPLUSOrder");

                    b.Property<int>("CourseId");

                    b.Property<int>("Language");

                    b.Property<int>("MATLABOrder");

                    b.Property<string>("PreReq");

                    b.Property<string>("TopicName");

                    b.Property<float>("TotalTime");

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.TopicModule", b =>
                {
                    b.Property<int>("TopicModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Language");

                    b.Property<string>("NextTopicModuleUrl");

                    b.Property<string>("PrevTopicModuleUrl");

                    b.Property<int>("TopicId");

                    b.Property<string>("TopicModuleTitle");

                    b.HasKey("TopicModuleId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicModules");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.TopicModuleContent", b =>
                {
                    b.Property<int>("TopicModuleContentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Class");

                    b.Property<int>("ContentGroup");

                    b.Property<int>("Language");

                    b.Property<string>("ModuleContent");

                    b.Property<string>("ModuleDescription");

                    b.Property<float>("ModuleTime");

                    b.Property<string>("ModuleTitle");

                    b.Property<int>("PriorLearned");

                    b.Property<int>("QuizId");

                    b.Property<int>("RelativeOrdering");

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

                    b.Property<string>("ListOrder");

                    b.Property<int>("PastExperience");

                    b.Property<int>("Status");

                    b.Property<bool>("ToLearn");

                    b.Property<int>("TopicId");

                    b.Property<string>("UserProfileId");

                    b.HasKey("UserProfileTopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("UserProfileTopics");
                });

            modelBuilder.Entity("BUDLP.Models.TopicModuleState", b =>
                {
                    b.Property<int>("TopicModuleStateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthenticatedUserId");

                    b.Property<DateTime?>("FirstVisited");

                    b.Property<DateTime?>("LastVisited");

                    b.Property<int>("LearningState");

                    b.Property<int>("TimeSpent");

                    b.Property<int>("TopicId");

                    b.Property<int>("TopicModuleId");

                    b.HasKey("TopicModuleStateId");

                    b.ToTable("TopicModuleStates");
                });

            modelBuilder.Entity("BUDLP.Models.TopicQuizzes.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuizAnswerId");

                    b.Property<int>("QuizType");

                    b.Property<int>("TopicModuleContentId");

                    b.HasKey("QuizId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("BUDLP.Models.TopicQuizzes.QuizOption", b =>
                {
                    b.Property<int>("QuizOptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuizId");

                    b.Property<string>("QuizOptionText");

                    b.HasKey("QuizOptionId");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizOptions");
                });

            modelBuilder.Entity("BUDLP.Models.TopicQuizzes.UserQuizResponse", b =>
                {
                    b.Property<int>("UserQuizResponseId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Correct");

                    b.Property<int>("QuizId");

                    b.Property<int>("QuizOptionId");

                    b.Property<string>("UserProfileId");

                    b.HasKey("UserQuizResponseId");

                    b.ToTable("UserQuizResponses");
                });

            modelBuilder.Entity("BUDLP.Models.UserLearningState", b =>
                {
                    b.Property<int>("UserLearningStateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthenticatedUserId");

                    b.Property<DateTime?>("FirstVisited");

                    b.Property<DateTime?>("LastVisited");

                    b.Property<int>("LearningState");

                    b.Property<int>("TimeSpent");

                    b.Property<int>("TopicId");

                    b.Property<int>("TopicModuleContentId");

                    b.Property<int>("TopicModuleId");

                    b.HasKey("UserLearningStateId");

                    b.HasIndex("TopicModuleContentId");

                    b.ToTable("UserLearningStates");
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
                    b.HasOne("BUDLP.Models.TopicModels.TopicModule", "TopicModule")
                        .WithMany("TopicModuleContent")
                        .HasForeignKey("TopicModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BUDLP.Models.TopicModels.UserProfileTopic", b =>
                {
                    b.HasOne("BUDLP.Models.TopicModels.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BUDLP.Models.TopicQuizzes.QuizOption", b =>
                {
                    b.HasOne("BUDLP.Models.TopicQuizzes.Quiz")
                        .WithMany("QuizOptions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BUDLP.Models.UserLearningState", b =>
                {
                    b.HasOne("BUDLP.Models.TopicModels.TopicModuleContent")
                        .WithMany("UserLearningState")
                        .HasForeignKey("TopicModuleContentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
