using System;
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
using AspNetCore.Security.CAS;

using Ksu.Gdc.Api.Core.Configurations;
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = new PathString("/login");
                    })
                    .AddCAS(options =>
                    {
                        options.ServiceHost = AppConfiguration.GetConfig("KsuCas_ServiceHost");
                        options.ServiceForceHTTPS = true;
                        options.CasServerUrlBase = AppConfiguration.GetConfig("KsuCas_BaseUrl");
                        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    });

            services.AddScoped<IAuthenticationService, AuthenticationService>();
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

                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<OfficerDbEntity, OfficerDto>();
                    cfg.CreateMap<UserDbEntity, UserDto>();
                    cfg.CreateMap<GroupDbEntity, GroupDto>();
                    cfg.CreateMap<GameDbEntity, GameDto>();
                });

                app.UseHttpsRedirection();
                app.UseStatusCodePages();
                app.UseCors(builder => builder.WithOrigins(
                    AppConfiguration.GetConfig("CorsUrl_WebApp"),
                    AppConfiguration.GetConfig("CorsUrl_WebApp_Testing")
                ));
                app.UseAuthentication();
                app.UseMvc();
            }
        }
    }
}