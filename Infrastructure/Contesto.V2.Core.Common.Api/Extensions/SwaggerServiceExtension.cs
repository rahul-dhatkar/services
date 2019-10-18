using Contesto.V2.Core.Common.Api.ConfigurationSettings;
using Contesto.V2.Core.Common.Api.OperationFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace Contesto.V2.Core.Common.Api.Extensions
{
    /// <summary>
    /// Swagger Service Extension
    /// </summary>
    public static class SwaggerServiceExtension
    {
        private static IConfigurationRoot Configuration;
        /// <summary>
        /// Adds the swagger documentation.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AppConfiguration>(options => configuration.GetSection("AppConfiguration").Bind(options));

            Configuration = configuration;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Info()
                {
                    Title = Configuration["AppConfiguration:Title"],
                    Version = Configuration["AppConfiguration:Version"],
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact() { Name = Configuration["AppConfiguration:Contact:Name"], Email = Configuration["AppConfiguration:Contact:Email"], Url = Configuration["AppConfiguration:Contact:Url"] }
                });
               // c.OperationFilter<FileUploadOperation>();
                var isAntiforgeryOn = Convert.ToBoolean(Configuration["AppConfiguration:IsAntiforgeryOn"]);
                if (isAntiforgeryOn)
                {
                    c.OperationFilter<HeaderTokenOperationFilter>();
                }
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllEnumsAsStrings();
                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            return services;
        }

        /// <summary>
        /// Uses the swagger documentation.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "service V1");
                c.DocumentTitle = "System";
                c.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}
