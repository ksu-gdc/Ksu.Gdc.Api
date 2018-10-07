using System;
using System.Linq;
using System.Collections.Generic;

using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Data.Extensions
{
    public static class KsuGdcContextExtensions
    {
        public static void EnsureSeedDataForContext(this KsuGdcContext context)
        {
            var officers = new List<ModelEntity_Officer>();
            if (!context.Officers.Any())
            {
                officers.AddRange(new List<ModelEntity_Officer>()
                {
                    new ModelEntity_Officer()
                    {
                        Position = "Advisor",
                        User = new ModelEntity_User()
                        {
                            UserId = 1,
                            Username = "nathanbean",
                            FirstName = "Nathan",
                            LastName = "Bean",
                            Description = "Nathan's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "President",
                        User = new ModelEntity_User()
                        {
                            UserId = 2,
                            Username = "kyletoom",
                            FirstName = "Kyle",
                            LastName = "Toom",
                            Description = "Kyle's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Vice President",
                        User = new ModelEntity_User()
                        {
                            UserId = 3,
                            Username = "bripriddle",
                            FirstName = "Bri",
                            LastName = "Priddle",
                            Description = "Bri's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Treasurer",
                        User = new ModelEntity_User()
                        {
                            UserId = 4,
                            Username = "stevenzwahl",
                            FirstName = "Steven",
                            LastName = "Zwahl",
                            Description = "Steven's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Secretary",
                        User = new ModelEntity_User()
                        {
                            UserId = 5,
                            Username = "carsonholt",
                            FirstName = "Carson",
                            LastName = "Holt",
                            Description = "Carson's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Website Manager",
                        User = new ModelEntity_User()
                        {
                            UserId = 6,
                            Username = "daytontaylor",
                            FirstName = "Dayton",
                            LastName = "Taylor",
                            Description = "Dayton's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Social Media Manager",
                        User = new ModelEntity_User()
                        {
                            UserId = 7,
                            Username = "timothyprice",
                            FirstName = "Timothy",
                            LastName = "Price",
                            Description = "Timothy's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Industry Liaison",
                        User = new ModelEntity_User()
                        {
                            UserId = 8,
                            Username = "jessemolenda",
                            FirstName = "Jesse",
                            LastName = "Molenda",
                            Description = "Jesse's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Industry Liaison",
                        User = new ModelEntity_User()
                        {
                            UserId = 9,
                            Username = "johnchapple",
                            FirstName = "John",
                            LastName = "Chapple",
                            Description = "John's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Event Coordinator",
                        User = new ModelEntity_User()
                        {
                            UserId = 10,
                            Username = "nathanmcclain",
                            FirstName = "Nathan",
                            LastName = "McClain",
                            Description = "Nathan's Description."
                        }
                    },
                    new ModelEntity_Officer()
                    {
                        Position = "Recruitment and Retention Officers",
                        User = new ModelEntity_User()
                        {
                            UserId = 11,
                            Username = "lilyfulton",
                            FirstName = "Lily",
                            LastName = "Fulton",
                            Description = "Lily's Description"
                        }
                    }
                });
                context.Officers.AddRange(officers);
            }

            var groups = new List<ModelEntity_Group>();
            if (!context.Groups.Any())
            {
                groups.AddRange(new List<ModelEntity_Group>()
                {
                    new ModelEntity_Group()
                    {
                        Name = "TestGroup"
                    },
                    new ModelEntity_Group()
                    {
                        Name = "TestGroup2"
                    }
                });
                context.Groups.AddRange(groups);
            }

            var games = new List<ModelEntity_Game>();
            if (!context.Games.Any())
            {
                games.AddRange(new List<ModelEntity_Game>()
                {
                    new ModelEntity_Game()
                    {
                        Title = "Overload",
                        Description = "Overload. A modern take on 80s arcade games, battle a continuous wave of enemies as the game continues to speed up. Use the mouse to controller your character. Left click to shoot clockwise, right click to shoot counter-clockwise, and both buttons at the same time to fire your super.",
                        Url = "https://jessej37.itch.io/overload",
                        User = officers[5].User
                    },
                    new ModelEntity_Game()
                    {
                        Title = "Operation Inundation",
                        Description = "Operation Inundation is a one to two player puzzle game. Featuring four levels with original art and music, it follows Agent 842 on his mission to retrieve something from the lowest level of a flooded building.",
                        Url = "https://pi-memorizer.itch.io/operation-inundation",
                        Group = groups[0]
                    },
                    new ModelEntity_Game()
                    {
                        Title = "FPS Prototype",
                        Description = "Pre-alpha prototype for an upcoming FPS game. The intent of this prototype was to create and refine the movement and shooting mechanics - the bread and butter of FPS gamefeel.",
                        Url = "https://crimsonseven.itch.io/pre-alpha-game",
                        User = officers[5].User
                    },
                    new ModelEntity_Game()
                    {
                        Title = "Incursion",
                        Description = "Move with arrow keys. Shoot with Space. Avoid, destroy, and collect health to survive.",
                        Url = "https://crimsonseven.itch.io/incursion"
                    },
                    new ModelEntity_Game()
                    {
                        Title = "Furmoji Frenzy",
                        Description = "Furmoji are the hot new toy this Christmas, and unfortunately, Santa's running low on supplies. It's your job to help him assemble them, wrap them up, then send them on their way. Due to the labour laws set forth by the elf union, Elves must not be required to do a task for more than 20 seconds at a time. However, Santa, being the shrewd old old man he is, he found a loophole: you and your partner must switch tasks every 20 seconds. So grab a friend and venture into the minds of four people who haven't slept well in over 60 hours. ***Two Xbox 360 Controllers are required to play this game***",
                        Url = "https://studiodingwing.itch.io/furmoji-frenzy"
                    },
                    new ModelEntity_Game()
                    {
                        Title = "Brave New World",
                        Description = "Kidnapped and imprisoned on the Eagle's Planet, you play as a young worm trying to escape and make your way home. Dig your way through multiple levels, all the while solving puzzles and avoiding eagles.",
                        Url = "https://pi-memorizer.itch.io/brave-new-worm"
                    }
                });
                context.Games.AddRange(games);
            }

            var userGroups = new List<JoinEntity_UserGroup>();
            if (!context.UserGroup.Any())
            {
                userGroups.AddRange(new List<JoinEntity_UserGroup>()
                {
                    new JoinEntity_UserGroup()
                    {
                        User = officers[5].User,
                        Group = groups[0]
                    },
                    new JoinEntity_UserGroup()
                    {
                        User = officers[5].User,
                        Group = groups[1]
                    },
                    new JoinEntity_UserGroup()
                    {
                        User = officers[1].User,
                        Group = groups[0]
                    }
                });
                context.UserGroup.AddRange(userGroups);
            }

            context.SaveChanges();
        }
    }
}
