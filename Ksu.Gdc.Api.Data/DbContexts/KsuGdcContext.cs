using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.DbContexts
{
    public class KsuGdcContext : DbContext
    {
        public KsuGdcContext(DbContextOptions<KsuGdcContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Game>()
                        .HasKey(c => new { c.UserId, c.GameId });
            modelBuilder.Entity<Group_User>()
                        .HasKey(c => new { c.GroupId, c.UserId });
        }

        public DbSet<OfficerDbEntity> Officers { get; set; }

        public DbSet<UserDbEntity> Users { get; set; }

        public DbSet<GroupDbEntity> Groups { get; set; }

        public DbSet<GameDbEntity> Games { get; set; }

        public DbSet<User_Game> Users_Games { get; set; }

        public DbSet<Group_User> Groups_Users { get; set; }
    }
}
