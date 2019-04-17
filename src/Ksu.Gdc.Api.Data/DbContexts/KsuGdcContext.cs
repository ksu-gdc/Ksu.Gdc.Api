using System;
using Microsoft.EntityFrameworkCore;

using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.DbContexts
{
    public class KsuGdcContext : DbContext
    {
        public DbSet<DbEntity_Image> Images { get; set; }

        public DbSet<DbEntity_Officer> Officers { get; set; }

        public DbSet<DbEntity_User> Users { get; set; }

        public DbSet<DbEntity_UserImage> UserImages { get; set; }

        public DbSet<DbEntity_Game> Games { get; set; }

        public DbSet<DbEntity_GameImage> GameImages { get; set; }

        public DbSet<DbEntity_GameUser> GameUsers { get; set; }

        public KsuGdcContext(DbContextOptions<KsuGdcContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbEntity_User>()
                .Property(u => u.HasVerifiedInfo)
                .HasDefaultValue(false);

            modelBuilder.Entity<DbEntity_UserImage>()
                .HasKey(ui => new { ui.UserId, ui.ImageId });

            modelBuilder.Entity<DbEntity_Game>()
                .Property(g => g.IsFeatured)
                .HasDefaultValue(false);

            modelBuilder.Entity<DbEntity_GameImage>()
                .HasKey(gi => new { gi.GameId, gi.ImageId });

            modelBuilder.Entity<DbEntity_GameUser>()
                .HasKey(gu => new { gu.UserId, gu.GameId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
