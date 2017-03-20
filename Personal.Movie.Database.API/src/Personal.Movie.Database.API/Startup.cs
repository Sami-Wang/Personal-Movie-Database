using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Personal.Movie.Database.API.Helpers;
using Personal.Movie.Database.API.IRepository;
using Personal.Movie.Database.API.Repository;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;

namespace Personal.Movie.Database.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddMvcCore().AddJsonFormatters().AddAuthorization();

            services.AddSingleton<IManageUserRepository, ManageUserRepository>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Personal Movie Database API Documentation",
                    Version = "v1",
                    Description = "The documentations for all APIs of Personal.Movie.Database project.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Yihang Wang",
                        Email = "wyhliverpoolfc@gmail.com",
                        Url = "https://github.com/wyhliverpoolfc/Personal-Movie-Database"
                    }
                });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "application",
                    Description = "OAuth 2.0 Client Credential Grant Type",
                    TokenUrl = Configuration.GetSection("URL").Value + "connect/token",
                    Scopes = Configuration.GetSection("SwaggerScopes").GetChildren().ToDictionary(
                        ss => ss.Key, ss => ss.Value)
                });

                // Assign scope requirements to operations based on AuthorizeAttribute
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Personal.Movie.Database.API.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Configure Authentication & Authorization Server
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Configuration.GetSection("URL").Value,
                RequireHttpsMetadata = false,
                ApiName = Configuration.GetSection("Resource").GetSection("APIName").Value,
                ApiSecret = Configuration.GetSection("Resource").GetSection("APISecrets").Value,
                AllowedScopes = { Configuration.GetSection("Resource").GetSection("AllowedScope").Value },
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseMvc();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personal.Movie.Database V1");
                //c.ConfigureOAuth2("2", "Swagger00..", "", "");
            });
        }
    }
}
