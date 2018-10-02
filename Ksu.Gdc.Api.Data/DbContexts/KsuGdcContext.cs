using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.DbContexts
{
    public class KsuGdcContext : DbContext
    {
        public DbSet<ModelEntity_Officer> Officer { get; set; }

        public DbSet<ModelEntity_User> User { get; set; }

        public DbSet<ModelEntity_Group> Group { get; set; }

        public DbSet<ModelEntity_Game> Game { get; set; }

        public DbSet<JoinEntity_UserGroup> UserGroup { get; set; }

        public KsuGdcContext(DbContextOptions<KsuGdcContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
