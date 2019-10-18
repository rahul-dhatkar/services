// -------------------------------------------------------------------------------------------
// ** Copyright © 2018, Fulcrum Digital                                  **
// ** All rights reserved.                                                                  **
// **                                                                                       **
// ** Redistribution, re-engineering or use of this code - in source                        **
// ** or binary forms with or without modifications, are not                                **
// ** permitted without prior written consent from appropriate person                       **
// ** in Fulcrum Digital                                                 **
// **                                                                                       **
// **                                                                                       **
// ** Author    : Fulcrum World Wide                                                        **
// ** Created   : 12-Jun-18                                                                 **
// ** Purpose   : Startup                                                                   **
// **                                                                                       **
// **                                                                                       **
// **                                                                                       **
// ** Change Log:                                                                           **
// ** ==================================                                                    **
// ** Name          Date         Purpose                                                    **
// ** Dhiraj G      12-06-18     Created                                                    **
// **                                                                                       **
// -------------------------------------------------------------------------------------------

using Contesto.V2.Core.Common.Api.OperationFilters;
using Contesto.V2.Core.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetEscapades.AspNetCore.SecurityHeaders;
using Newtonsoft.Json.Serialization;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Extensions;
using Contesto.V2.Core.Infrastructure.LoggerService.Extensions;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Extensions;
using Contesto.V2.Core.Infrastructure.Security.AntiForgeryTokenService.Extensions;
using Contesto.V2.Core.Common.Api.ConfigurationSettings;
using System;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;

namespace Contesto.V2.Core.Common.Api.Base
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        /// <value>
        /// The application configuration.
        /// </value>
        public AppConfiguration AppConfiguration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _hostingEnvironment = env;

        }



        /// <summary>
        /// Bases the configure services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void BaseConfigureServices(IServiceCollection services)
        {


            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // Add DbConfiguration reader
            services.AddDbConfigurationService(Configuration);
            //SetUpConfigurationManager(services);

            //File Provider
            var physicalProvider = _hostingEnvironment.ContentRootFileProvider;
            services.AddSingleton(physicalProvider);

            // Caching Configuration
            services.AddMemoryCache();

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    x => x.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Enable JwtBearerIdentityService
            services.AddJwtBearerIdentityService(Configuration);

            var isAntiforgeryOn = Convert.ToBoolean(Configuration["AppConfiguration:IsAntiforgeryOn"]);
            if (isAntiforgeryOn)
            {
                services.AddAntiForgeryTokenService(Configuration);
                services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
            }

            //MVC/WEBAPI Configuration - Force Camel Case to JSON

            services.AddMvc(options =>
            {
                if (isAntiforgeryOn)
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }

                options.Filters.Add(typeof(CustomExceptionFilter));
                options.ReturnHttpNotAcceptable = true;
            }).AddJsonOptions(opts => { opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Swagger UI Configuration
            //services.AddCustomHeaders();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["AppConfiguration:Version"], new Info()
                {
                    Title = Configuration["AppConfiguration:Title"],
                    Version = Configuration["AppConfiguration:Version"],
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact() { Name = Configuration["AppConfiguration:Contact:Name"], Email = Configuration["AppConfiguration:Contact:Email"], Url = Configuration["AppConfiguration:Contact:Url"] }
                });

                if (isAntiforgeryOn)
                {
                    c.OperationFilter<HeaderTokenOperationFilter>();
                }
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllEnumsAsStrings();
                //Swagger 2.+ support
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

            //services.AddSwaggerDocumentation(Configuration);

            services.ConfigureSwaggerGen(options =>
            {
                options.DescribeStringEnumsInCamelCase();
                options.DescribeAllEnumsAsStrings();
            });
        }

        /// <summary>
        /// Bases the configure. This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void BaseConfigure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(Infrastructure.LoggerService.Dtos.LoggerTypeEnum.Database, LogLevel.Information, Configuration.GetConnectionString("DefaultConnection"));

            var policyCollection = new HeaderPolicyCollection()
                .AddFrameOptionsDeny()
                .AddXssProtectionBlock()
                .AddContentTypeOptionsNoSniff()
                .AddStrictTransportSecurityMaxAge(maxAgeInSeconds: 60 * 60 * 24 * 365) // maxage = one year in seconds
                .AddReferrerPolicyOriginWhenCrossOrigin()
                .RemoveServerHeader()
                .AddCustomHeader("X-FrameworkOne-Header", "FulcrumOne");

#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0612 // Type or member is obsolete
            app.UseApplicationInsightsRequestTelemetry();
#pragma warning restore CS0612 // Type or member is obsolete

#pragma warning disable CS0612 // Type or member is obsolete
            app.UseApplicationInsightsExceptionTelemetry();
#pragma warning restore CS0612 // Type or member is obsolete

            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwaggerDocumentation();
        }
    }
}