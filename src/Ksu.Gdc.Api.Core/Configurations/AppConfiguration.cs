using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class AppConfiguration
    {
        public static IConfiguration Configuration { get; private set; }

        public static IConfiguration Initialize(IHostingEnvironment env)
        {
            ConfigureAutoMapper();
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            return Configuration;
        }

        public static string GetConfig(string key)
        {
            return Configuration[key];
        }

        public static void SetConfig(string key, string value)
        {
            Configuration[key] = value;
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                // Image
                cfg.CreateMap<UpdateDto_Image, DbEntity_Image>();

                // Officer
                cfg.CreateMap<DbEntity_Officer, Dto_Officer>();
                cfg.CreateMap<DbEntity_Officer, CreateDto_Officer>();
                cfg.CreateMap<DbEntity_Officer, UpdateDto_Officer>();

                cfg.CreateMap<Dto_Officer, DbEntity_Officer>();
                cfg.CreateMap<CreateDto_Officer, DbEntity_Officer>();
                cfg.CreateMap<UpdateDto_Officer, DbEntity_Officer>();
                // User
                cfg.CreateMap<DbEntity_User, Dto_User>();
                cfg.CreateMap<DbEntity_User, CreateDto_User>();
                cfg.CreateMap<DbEntity_User, UpdateDto_User>();
                cfg.CreateMap<DbEntity_User, PatchDto_User>();
                cfg.CreateMap<DbEntity_User, AuthDto_User>();

                cfg.CreateMap<Dto_User, DbEntity_User>();
                cfg.CreateMap<CreateDto_User, DbEntity_User>();
                cfg.CreateMap<UpdateDto_User, DbEntity_User>();
                cfg.CreateMap<PatchDto_User, DbEntity_User>();
                // Game
                cfg.CreateMap<DbEntity_Game, Dto_Game>();
                cfg.CreateMap<DbEntity_Game, CreateDto_Game>();
                cfg.CreateMap<DbEntity_Game, UpdateDto_Game>();
                cfg.CreateMap<DbEntity_Game, PatchDto_Game>();

                cfg.CreateMap<Dto_Game, DbEntity_Game>();
                cfg.CreateMap<CreateDto_Game, DbEntity_Game>();
                cfg.CreateMap<UpdateDto_Game, DbEntity_Game>();
                cfg.CreateMap<PatchDto_Game, DbEntity_Game>();
            });
        }
    }
}
