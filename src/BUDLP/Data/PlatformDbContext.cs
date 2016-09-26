using BUDLP.Data.Migrations;
using BUDLP.Models.AuthUserModels;
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

    }
}
