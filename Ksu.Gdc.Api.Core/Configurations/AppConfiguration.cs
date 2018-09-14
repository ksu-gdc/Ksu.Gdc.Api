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
                cfg.CreateMap<OfficerDbEntity, OfficerDto>();
                cfg.CreateMap<UserDbEntity, UserDto>();
                cfg.CreateMap<UserForCreationDto, UserDbEntity>();
                cfg.CreateMap<UserForUpdateDto, UserDbEntity>();
                cfg.CreateMap<GroupDbEntity, GroupDto>();
                cfg.CreateMap<GameDbEntity, GameDto>();
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
