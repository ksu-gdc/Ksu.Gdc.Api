using System;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.DbContexts
{
    public class KsuGdcContext : DbContext
    {
        public DbSet<DbEntity_Officer> Officers { get; set; }

        public DbSet<DbEntity_User> Users { get; set; }

        public DbSet<DbEntity_Group> Groups { get; set; }

        public DbSet<DbEntity_GroupUser> GroupUsers { get; set; }

        public DbSet<DbEntity_Game> Games { get; set; }

        public KsuGdcContext(DbContextOptions<KsuGdcContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbEntity_Officer>()
                       .Property(g => g.CreatedOn)
                       .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_Officer>()
                        .Property(g => g.UpdatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_User>()
                       .Property(g => g.CreatedOn)
                       .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_User>()
                        .Property(g => g.UpdatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_Group>()
                        .Property(g => g.CreatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_Group>()
                        .Property(g => g.UpdatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_GroupUser>()
                        .HasKey(ug => new { ug.GroupId, ug.UserId });
            modelBuilder.Entity<DbEntity_GroupUser>()
                        .Property(g => g.CreatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_GroupUser>()
                        .Property(g => g.UpdatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<DbEntity_Game>()
                        .Property(g => g.CreatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);
            modelBuilder.Entity<DbEntity_Game>()
                        .Property(g => g.UpdatedOn)
                        .HasDefaultValue(DateTimeOffset.Now);

            base.OnModelCreating(modelBuilder);
        }
    }
}
