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

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Services;
using Ksu.Gdc.Api.Data.DbContexts;
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

            AppConfiguration.Initialize(builder.Build());
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
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPortfolioService, PortfolioService>();

            var awsOptions = new AWSOptions()
            {
                Credentials = new BasicAWSCredentials(AppConfiguration.GetConfig("AWS_S3_AccessKey"),
                                                      AppConfiguration.GetConfig("AWS_S3_SecretKey")),
                Region = RegionEndpoint.USEast2
            };
            services.AddAWSService<IAmazonS3>(awsOptions);

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
                }
                else
                {
                    //app.UseExceptionHandler();
                    app.UseHsts();
                }
                ksuGdcContext.Database.EnsureCreated();

                app.UseHttpsRedirection();
                app.UseStatusCodePages();
                app.UseCors(builder =>
                {
                    builder.WithOrigins(AppConfiguration.GetConfig("App_Url"));
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
                app.UseMvc();
            }
        }
    }
}