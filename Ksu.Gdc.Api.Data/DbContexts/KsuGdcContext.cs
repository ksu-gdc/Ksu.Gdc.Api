using System;
using System.Collections.Generic;
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
            modelBuilder.Entity<JoinEntity_UserGroup>()
                        .HasKey(m => new { m.UserId, m.GroupId });
        }

        public DbSet<ModelEntity_Officer> Officers { get; set; }

        public DbSet<ModelEntity_User> Users { get; set; }

        public DbSet<ModelEntity_Group> Groups { get; set; }

        public DbSet<ModelEntity_Role> Roles { get; set; }

        public DbSet<ModelEntity_Game> Games { get; set; }

        public DbSet<JoinEntity_UserGroup> UsersGroups { get; set; }
    }
}
