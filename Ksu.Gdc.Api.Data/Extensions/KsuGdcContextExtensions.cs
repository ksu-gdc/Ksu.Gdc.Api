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

            var games = new List<GameDbEntity>()
            {
                new GameDbEntity()
                {
                    Title = "Overload",
                    Description = "Overload. A modern take on 80s arcade games, battle a continuous wave of enemies as the game continues to speed up. Use the mouse to controller your character. Left click to shoot clockwise, right click to shoot counter-clockwise, and both buttons at the same time to fire your super.",
                    Url = "https://jessej37.itch.io/overload"
                },
                new GameDbEntity()
                {
                    Title = "Operation Inundation",
                    Description = "Operation Inundation is a one to two player puzzle game. Featuring four levels with original art and music, it follows Agent 842 on his mission to retrieve something from the lowest level of a flooded building.",
                    Url = "https://pi-memorizer.itch.io/operation-inundation"
                },
                new GameDbEntity()
                {
                    Title = "FPS Prototype",
                    Description = "Pre-alpha prototype for an upcoming FPS game. The intent of this prototype was to create and refine the movement and shooting mechanics - the bread and butter of FPS gamefeel.",
                    Url = "https://crimsonseven.itch.io/pre-alpha-game"
                },
                new GameDbEntity()
                {
                    Title = "Incursion",
                    Description = "Move with arrow keys. Shoot with Space. Avoid, destroy, and collect health to survive.",
                    Url = "https://crimsonseven.itch.io/incursion"
                },
                new GameDbEntity()
                {
                    Title = "Furmoji Frenzy",
                    Description = "Furmoji are the hot new toy this Christmas, and unfortunately, Santa's running low on supplies. It's your job to help him assemble them, wrap them up, then send them on their way. Due to the labour laws set forth by the elf union, Elves must not be required to do a task for more than 20 seconds at a time. However, Santa, being the shrewd old old man he is, he found a loophole: you and your partner must switch tasks every 20 seconds. So grab a friend and venture into the minds of four people who haven't slept well in over 60 hours. ***Two Xbox 360 Controllers are required to play this game***",
                    Url = "https://studiodingwing.itch.io/furmoji-frenzy"
                },
                new GameDbEntity()
                {
                    Title = "Brave New World",
                    Description = "Kidnapped and imprisoned on the Eagle's Planet, you play as a young worm trying to escape and make your way home. Dig your way through multiple levels, all the while solving puzzles and avoiding eagles.",
                    Url = "https://pi-memorizer.itch.io/brave-new-worm"
                }
            };

            context.AddRange(games);
            context.AddRange(officers);
            context.SaveChanges();
        }
    }
}
