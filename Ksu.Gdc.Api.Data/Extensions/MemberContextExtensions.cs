using System;
using System.Linq;
using System.Collections.Generic;

using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Data.DbContexts;

namespace Ksu.Gdc.Api.Data.Extensions
{
    public static class MemberContextExtensions
    {
        public static void EnsureSeedDataForContext(this MemberContext context)
        {
            if (context.Officers.Any())
            {
                return;
            }

            var officers = new List<OfficerDbEntity>()
            {
                new OfficerDbEntity()
                {
                    Position = "President",
                    FirstName = "Lauren",
                    LastName = "Lynch",
                    Description = "Lauren's Description.",
                    User = new UserDbEntity()
                    {
                        Id = 1,
                        Username = "laurenlynch"
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Advisor",
                    FirstName = "Nathan",
                    LastName = "Bean",
                    Description = "Nathan's Description.",
                    User = new UserDbEntity()
                    {
                        Id = 2,
                        Username = "nathanbean"
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Website Manager",
                    FirstName = "Dayton",
                    MiddleName = "Lee",
                    LastName = "Taylor",
                    Description = "Dayton's Description.",
                    User = new UserDbEntity()
                    {
                        Id = 3,
                        Username = "daytonltaylor"
                    }
                }
            };

            context.AddRange(officers);
            context.SaveChanges();
        }
    }
}
