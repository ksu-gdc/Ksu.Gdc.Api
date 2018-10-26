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
using Microsoft.AspNetCore.Mvc.Formatters;
using Amazon.Runtime;
using Amazon;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Services;
using Ksu.Gdc.Api.Data;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Extensions;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web
{
    public class Startup
    {
        IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = AppConfiguration.Initialize(env);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", p => p
                                  .AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
                options.AddPolicy("AllowAppOnly", p => p
                                  .WithOrigins(AppConfiguration.GetConfig("App_Url"))
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
            });

            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOfficerService, OfficerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGameService, GameService>();

            var awsOptions = new AWSOptions()
            {
                Credentials = new BasicAWSCredentials(AppConfiguration.GetConfig("AWS_S3_AccessKey"),
                                                      AppConfiguration.GetConfig("AWS_S3_SecretKey")),
                Region = RegionEndpoint.USEast2
            };
            services.AddAWSService<IAmazonS3>(awsOptions);

            services.AddDbContext<KsuGdcContext>(options => options
                                                 .UseMySql(AppConfiguration.GetConfig("connectionStrings:MySql_KsuGdc")));
            services.AddScoped<DisconnectedData>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddMvcOptions(options =>
                    {
                        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var ksuGdcContext = serviceScope.ServiceProvider.GetRequiredService<KsuGdcContext>();

                loggerFactory.AddConsole();
                loggerFactory.AddDebug();

                ksuGdcContext.Database.Migrate();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    ksuGdcContext.EnsureSeedDataForContext();
                }
                else
                {
                    //app.UseExceptionHandler();
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStatusCodePages();
                app.UseCors("AllowAppOnly");
                app.UseMvc();
            }
        }
    }
}