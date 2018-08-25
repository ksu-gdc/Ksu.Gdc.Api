using System;
using System.Linq;
using System.Collections.Generic;

using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Data.DbContexts;

namespace Ksu.Gdc.Api.Data.Extensions
{
    public static class KsuGdcContextExtensions
    {
        public static void EnsureSeedDataForContext(this KsuGdcContext context)
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
                    User = new UserDbEntity()
                    {
                        Id = 1,
                        Username = "laurenlynch",
                        FirstName = "Lauren",
                        LastName = "Lynch",
                        Description = "Lauren's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Advisor",
                    User = new UserDbEntity()
                    {
                        Id = 2,
                        Username = "nathanbean",
                        FirstName = "Nathan",
                        LastName = "Bean",
                        Description = "Nathan's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Vice President",
                    User = new UserDbEntity()
                    {
                        Id = 3,
                        Username = "carsonholt",
                        FirstName = "Carson",
                        LastName = "Holt",
                        Description = "Carson's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Treasurer",
                    User = new UserDbEntity()
                    {
                        Id = 4,
                        Username = "stevenzwahl",
                        FirstName = "Steven",
                        LastName = "Zwahl",
                        Description = "Steven's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Event Manager",
                    User = new UserDbEntity()
                    {
                        Id = 5,
                        Username = "nathanmcclain",
                        FirstName = "Nathan",
                        LastName = "McClain",
                        Description = "Nathan's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Industry Liaison",
                    User = new UserDbEntity()
                    {
                        Id = 6,
                        Username = "jessemolenda",
                        FirstName = "Jesse",
                        LastName = "Molenda",
                        Description = "Jesse's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Website Manager",
                    User = new UserDbEntity()
                    {
                        Id = 7,
                        Username = "daytonltaylor",
                        FirstName = "Dayton",
                        LastName = "Taylor",
                        Description = "Dayton's Description."
                    }
                },
                new OfficerDbEntity()
                {
                    Position = "Social Media Manager",
                    User = new UserDbEntity()
                    {
                        Id = 8,
                        Username = "kyleingram",
                        FirstName = "Kyle",
                        LastName = "Ingram",
                        Description = "Kyle's Description."
                    }
                }
            };

            context.AddRange(officers);
            context.SaveChanges();
        }
    }
}
