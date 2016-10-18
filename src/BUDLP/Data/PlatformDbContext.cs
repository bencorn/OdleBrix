using BUDLP.Data.Migrations;
using BUDLP.Models.AuthUserModels;
using BUDLP.Models.TopicModels;
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
    }
}
