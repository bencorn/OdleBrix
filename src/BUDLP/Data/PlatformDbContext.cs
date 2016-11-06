using BUDLP.Data.Migrations;
using BUDLP.Models.AuthUserModels;
using BUDLP.Models.TopicModels;
using BUDLP.Models.TopicQuizzes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Data
{
    public class PlatformDbContext : DbContext
    {
        public PlatformDbContext(DbContextOptions<PlatformDbContext> options)
                : base(options)
        {
        }
        public DbSet<AuthUserProfile> AuthUserProfiles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserProfileTopic> UserProfileTopics { get; set; }
        public DbSet<TopicModule> TopicModules { get; set; }
        public DbSet<TopicModuleContent> TopicModuleContent { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
    }
}
