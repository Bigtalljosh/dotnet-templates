using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MyProject.Api.DependencyInjection
{
    internal static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, ApiDetails apiDetails)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", CreateInfoForApiVersion("v1", apiDetails));

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Authorization header using Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        private static OpenApiInfo CreateInfoForApiVersion(string version, ApiDetails apiDetails)
        {
            var info = new OpenApiInfo()
            {
                Title = $"{apiDetails.Title} {version} - {apiDetails.Owners}",
                Version = version,
                Description = $"### {apiDetails.Description}\n" +
                              $"### [{apiDetails.Title} Git Repository]({apiDetails.GitRepoUrl})"
            };

            return info;
        }
    }
}