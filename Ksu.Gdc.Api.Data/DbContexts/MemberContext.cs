using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.DbContexts
{
    public class MemberContext : DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options) : base(options) { }

        public DbSet<OfficerDbEntity> Officers { get; set; }

        public DbSet<UserDbEntity> Users { get; set; }
    }
}
