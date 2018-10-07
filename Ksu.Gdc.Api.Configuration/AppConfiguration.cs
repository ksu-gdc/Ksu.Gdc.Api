using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Ksu.Gdc.Api.Configuration
{
    public static class AppConfiguration
    {
        public static IConfiguration Configuration { get; private set; }

        public static IConfiguration Initialize(IHostingEnvironment env)
        {
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
    }
}
