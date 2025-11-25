using AuthenticationAPI_NetCore6.Helpers;
using Microsoft.OpenApi.Models;

namespace AuthenticationAPI_NetCore6
{
    /// <summary>
    /// Classe de extensão para configurar o Swagger/OpenAPI com suporte aos tokens de autenticação customizados.
    /// Adiciona os campos Token-Client e Token-Application na interface do Swagger UI.
    /// </summary>
    public static class ServiceCollectionExtensionsSwagger
    {
        private static string TokenClient = AuthenticationApi_Helpers.TokenClient;
        private static string TokenApplication = AuthenticationApi_Helpers.TokenApplication;

        /// <summary>
        /// Adiciona a configuração do Swagger ao container de serviços.
        /// Configura os campos de autenticação Token-Client e Token-Application no Swagger UI.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        /// <returns>A coleção de serviços com o Swagger configurado.</returns>
        public static IServiceCollection AddSwaggerConfiguraton(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });

                // Define o campo Token-Client como um header de segurança no Swagger
                c.AddSecurityDefinition(TokenClient, new OpenApiSecurityScheme
                {
                    Description = $"Enter your {TokenClient} Key below:",
                    Name = TokenClient,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
                );

                // Define o campo Token-Application como um header de segurança no Swagger
                c.AddSecurityDefinition(TokenApplication, new OpenApiSecurityScheme
                {
                    Description = $"Enter your {TokenApplication} Key below:",
                    Name = TokenApplication,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
                );

                // Adiciona o requisito de segurança Token-Client para todos os endpoints
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

                // Adiciona o requisito de segurança Token-Application para todos os endpoints
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

        /// <summary>
        /// Configura o uso do Swagger na pipeline de requisições HTTP.
        /// Habilita o Swagger JSON endpoint e a interface Swagger UI.
        /// </summary>
        /// <param name="app">O application builder.</param>
        /// <returns>O application builder com o Swagger configurado.</returns>
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;

        }
    }
}