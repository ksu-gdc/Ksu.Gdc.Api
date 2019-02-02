using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Amazon.Runtime;
using Amazon;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Services;
using Ksu.Gdc.Api.Data;
using Ksu.Gdc.Api.Data.DbContexts;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AppConfiguration.GetConfig("JwtAuth_Issuer"),
                    ValidAudience = AppConfiguration.GetConfig("JwtAuth_Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.GetConfig("JwtAuth_Key")))
                };
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOfficerService, OfficerService>();
            services.AddScoped<IUserService, UserService>();
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var ksuGdcContext = serviceScope.ServiceProvider.GetRequiredService<KsuGdcContext>();

                ksuGdcContext.Database.Migrate();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    //app.UseExceptionHandler();
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStatusCodePages();
                app.UseCors("AllowAppOnly");
                app.UseAuthentication();
                app.UseMvc();
            }
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KsuGdcContext>
    {
        public KsuGdcContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var builder = new DbContextOptionsBuilder<KsuGdcContext>();
            var connectionString = configuration.GetConnectionString("MySql_KsuGdc");
            builder.UseMySql(connectionString);
            return new KsuGdcContext(builder.Options);
        }
    }
}