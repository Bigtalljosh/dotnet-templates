using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyProject.Business.House;
using MyProject.Data;
using MyProject.Data.Repositories;
using MyProject.Api.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;

namespace MyProject.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly string _siteVersion;
        private readonly string _namespace;
        private readonly string _deploymentHost;
        private readonly bool _enableSwagger;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;

            var assemblyLocation = typeof(Startup).GetTypeInfo().Assembly.Location;
            _siteVersion = FileVersionInfo.GetVersionInfo(assemblyLocation).ProductVersion;
            _namespace = typeof(Startup).Namespace;

            _deploymentHost = _configuration.GetValue<string>("DeploymentHost");
            _enableSwagger = _configuration.GetValue<bool>("FeatureToggles:EnableSwagger");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddMemoryCache();
            services.AddHealthChecks();

            services.AddCors(options =>
            {
                var allowedOrigins = _configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[0];
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins);
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
            });

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("Purplebricks.Microservice");
            });

            services.AddScoped<IHouseRepository, EntityFrameworkHouseRepository>();
            services.AddScoped<IHouseService, HouseService>();

            if (_enableSwagger)
            {
                var apiDetails = _configuration.GetSection("ApiDetails").Get<ApiDetails>();
                services.AddSwagger(apiDetails);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHealthChecks("/health");

            if (_enableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(opts =>
                {
                    opts.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Startup).Namespace);
                });
            }
        }
    }
}
