using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AutoMapper;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Configurations
{
    public static class AppConfiguration
    {
        public static IConfiguration Configuration { get; private set; }

        public static void Initialize(IConfiguration config)
        {
            Configuration = config;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ModelEntity_Officer, Dto_Officer>();
                cfg.CreateMap<ModelEntity_User, Dto_User>();
                cfg.CreateMap<CreateDto_User, ModelEntity_User>();
                cfg.CreateMap<UpdateDto_User, ModelEntity_User>();
                cfg.CreateMap<ModelEntity_Group, Dto_Group>();
                cfg.CreateMap<ModelEntity_Game, Dto_Game>();
            });
        }

        public static string GetConfig(string key)
        {
            return Configuration[key];
        }

        public static void SetConfig(string key, string value)
        {
            Configuration[key] = value;
        }
    }
}
