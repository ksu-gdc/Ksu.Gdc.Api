﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using Ksu.Gdc.Api.Core;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Web.Services;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Extensions;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            AppConfiguration.Configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IOfficerService, OfficerService>();
            services.AddScoped<IUserService, UserService>();

            var connectionString = Configuration["connectionStrings:memberDBConnectionString"];
            services.AddDbContext<MemberContext>(options => options.UseMySql(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MemberContext memberContext)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var memberDBContext = serviceScope.ServiceProvider.GetRequiredService<MemberContext>();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    memberDBContext.Database.EnsureDeleted();
                }
                else
                {
                    app.UseExceptionHandler();
                    app.UseHsts();
                }
                memberDBContext.Database.EnsureCreated();

                memberContext.EnsureSeedDataForContext();

                app.UseHttpsRedirection();
                app.UseStatusCodePages();

                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<OfficerDbEntity, OfficerDto>();
                    cfg.CreateMap<UserDbEntity, UserDto>();
                });

                app.UseMvc();
            }
        }
    }
}