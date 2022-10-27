using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.OpenApi.Models;

namespace AuthenticationAPI_NetCore6
{
    public static class ServiceCollectionExtensionsSwagger
    {
        private static string TokenClient = AuthenticationApi_Helpers.TokenClient;
        private static string TokenApplication = AuthenticationApi_Helpers.TokenApplication;

        public static IServiceCollection AddSwaggerConfiguraton(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });

                c.AddSecurityDefinition(TokenClient, new OpenApiSecurityScheme
                {
                    Description = $"Enter your {TokenClient} Key below:",
                    Name = TokenClient,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
                );

                c.AddSecurityDefinition(TokenApplication, new OpenApiSecurityScheme
                {
                    Description = $"Enter your {TokenApplication} Key below:",
                    Name = TokenApplication,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
                );

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = TokenClient
                                },
                            },
                            new List<string>()
                        }
                    }
                );

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = TokenApplication
                                },
                            },
                            new List<string>()
                        }
                    }
                );
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;

        }
    }
}