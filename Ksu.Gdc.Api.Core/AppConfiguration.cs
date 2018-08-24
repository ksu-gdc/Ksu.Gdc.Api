using System;
using Microsoft.Extensions.Configuration;

namespace Ksu.Gdc.Api.Core
{
    public static class AppConfiguration
    {
        public static IConfiguration Configuration { get; set; }

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
