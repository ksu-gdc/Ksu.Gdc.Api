using System;
using System.Collections.Generic;
using System.IO;
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
            services.AddCors();

            services.AddScoped<IOfficerService, OfficerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPortfolioService, PortfolioService>();

            throw new Exception(AppConfiguration.GetConfig("connectionStrings:MYSQLCONNSTR_localdb"));

            services.AddDbContext<KsuGdcContext>(options => options
                                                 .UseMySql(AppConfiguration.GetConfig("connectionStrings:MySql_KsuGdc")));
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
                }
                else
                {
                    app.UseExceptionHandler();
                    app.UseHsts();
                }
                ksuGdcContext.Database.EnsureCreated();

                ksuGdcContext.EnsureSeedDataForContext();

                app.UseHttpsRedirection();
                app.UseStatusCodePages();

                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<OfficerDbEntity, OfficerDto>();
                    cfg.CreateMap<UserDbEntity, UserDto>();
                    cfg.CreateMap<GroupDbEntity, GroupDto>();
                    cfg.CreateMap<GameDbEntity, GameDto>();
                });

                app.UseCors(builder => builder.WithOrigins(
                    AppConfiguration.GetConfig("AppHostUrl")
                ));
                app.UseMvc();
            }
        }
    }
}