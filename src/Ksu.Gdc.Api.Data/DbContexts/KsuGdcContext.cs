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
            modelBuilder.Entity<DbEntity_Image>()
                .Property(i => i.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_Officer>()
                .Property(o => o.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_Officer>()
                .Property(o => o.UpdatedOn)
                .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_User>()
                .Property(u => u.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_User>()
                .Property(u => u.UpdatedOn)
                .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_UserImage>()
                .HasKey(ui => new { ui.UserId, ui.ImageId });
            modelBuilder.Entity<DbEntity_UserImage>()
                .Property(ui => ui.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_Game>()
                .Property(g => g.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_Game>()
                .Property(g => g.UpdatedOn)
                .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_Game>()
                .Property(g => g.IsFeatured)
                .HasDefaultValue(false);

            modelBuilder.Entity<DbEntity_GameImage>()
                .HasKey(gi => new { gi.GameId, gi.ImageId });
            modelBuilder.Entity<DbEntity_GameImage>()
                .Property(gi => gi.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_GameUser>()
                .HasKey(gu => new { gu.UserId, gu.GameId });
            modelBuilder.Entity<DbEntity_GameUser>()
                .Property(gu => gu.CreatedOn)
                .HasDefaultValue(DateTimeOffset.Now);

            base.OnModelCreating(modelBuilder);
        }
    }
}
