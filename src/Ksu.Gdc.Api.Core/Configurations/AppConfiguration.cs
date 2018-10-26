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
                // Officer
                cfg.CreateMap<ModelEntity_Officer, Dto_Officer>();
                cfg.CreateMap<ModelEntity_Officer, CreateDto_Officer>();
                cfg.CreateMap<ModelEntity_Officer, UpdateDto_Officer>();

                cfg.CreateMap<Dto_Officer, ModelEntity_Officer>();
                cfg.CreateMap<CreateDto_Officer, ModelEntity_Officer>();
                cfg.CreateMap<UpdateDto_Officer, ModelEntity_Officer>();
                // User
                cfg.CreateMap<ModelEntity_User, Dto_User>();
                cfg.CreateMap<ModelEntity_User, CreateDto_User>();
                cfg.CreateMap<ModelEntity_User, UpdateDto_User>();
                cfg.CreateMap<ModelEntity_User, AuthDto_User>();

                cfg.CreateMap<Dto_User, ModelEntity_User>();
                cfg.CreateMap<CreateDto_User, ModelEntity_User>();
                cfg.CreateMap<UpdateDto_User, ModelEntity_User>();
                // Group
                cfg.CreateMap<ModelEntity_Group, Dto_Group>();
                cfg.CreateMap<ModelEntity_Group, CreateDto_Group>();
                cfg.CreateMap<ModelEntity_Group, UpdateDto_Group>();

                cfg.CreateMap<Dto_Group, ModelEntity_Group>();
                cfg.CreateMap<CreateDto_Group, ModelEntity_Group>();
                cfg.CreateMap<UpdateDto_Group, ModelEntity_Group>();
                // Game
                cfg.CreateMap<ModelEntity_Game, Dto_Game>();
                cfg.CreateMap<ModelEntity_Game, CreateDto_Game>();
                cfg.CreateMap<ModelEntity_Game, UpdateDto_Game>();

                cfg.CreateMap<Dto_Game, ModelEntity_Game>();
                cfg.CreateMap<CreateDto_Game, ModelEntity_Game>();
                cfg.CreateMap<UpdateDto_Game, ModelEntity_Game>();
            });
        }
    }
}
