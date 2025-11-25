using AuthenticationAPI_NetCore6;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os controllers à aplicação
builder.Services.AddControllers();

// Configura o Swagger/OpenAPI para documentação da API
// Mais informações em: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Adiciona a configuração customizada do Swagger com os tokens de autenticação
builder.Services.AddSwaggerConfiguraton(builder.Configuration);

var app = builder.Build();

// Configuração do pipeline de requisições HTTP

// Autenticação via Tokens da API
// Comente a linha abaixo se desejar testar a autenticação apenas via Attribute nos controllers
// Quando ativo, este middleware valida os tokens em TODAS as requisições
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger apenas em ambiente de desenvolvimento
    app.UseSwaggerConfiguration();
}

// Adiciona o middleware de autenticação customizada à pipeline
// Este middleware valida os tokens Token-Client e Token-Application em todas as requisições
app.UseMiddleware<AuthenticationApi>();

// Redireciona requisições HTTP para HTTPS
app.UseHttpsRedirection();

// Habilita o middleware de autorização do ASP.NET Core
app.UseAuthorization();

// Mapeia os controllers para as rotas
app.MapControllers();

app.Run();
