using System;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Ksu.Gdc.Api.Configuration;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.DbContexts
{
    public class KsuGdcContext : DbContext
    {
        public DbSet<ModelEntity_Game> Games { get; set; }

        public DbSet<ModelEntity_Group> Groups { get; set; }

        public DbSet<ModelEntity_Officer> Officers { get; set; }

        public DbSet<ModelEntity_User> Users { get; set; }

        public DbSet<JoinEntity_UserGroup> UserGroup { get; set; }

        public KsuGdcContext(DbContextOptions<KsuGdcContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JoinEntity_UserGroup>()
                        .HasKey(ug => new { ug.UserId, ug.GroupId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
