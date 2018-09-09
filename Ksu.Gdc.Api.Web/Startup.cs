﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Formatters;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Services;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Extensions;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            AppConfiguration.Initialize(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddMvcOptions(options =>
                    {
                        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    });
            services.AddCors();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOfficerService, OfficerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPortfolioService, PortfolioService>();

            services.AddDbContext<KsuGdcContext>(options =>
            {
                options.UseMySql(AppConfiguration.GetConfig("connectionStrings:MySql_KsuGdc"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var ksuGdcContext = serviceScope.ServiceProvider.GetRequiredService<KsuGdcContext>();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    ksuGdcContext.Database.EnsureDeleted();
                    ksuGdcContext.Database.EnsureCreated();
                    ksuGdcContext.EnsureSeedDataForContext();
                }
                else
                {
                    ksuGdcContext.Database.EnsureCreated();
                    app.UseExceptionHandler();
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStatusCodePages();
                app.UseCors(builder =>
                {
                    builder.WithOrigins(AppConfiguration.GetConfig("App_Url"));
                    builder.AllowAnyMethod();
                });
                app.UseMvc();
            }
        }
    }
}