using AuthenticationAPI_NetCore6;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguraton(builder.Configuration);

var app = builder.Build();

// Authentication API Tokens
// Comment out the next line if you want to test the Authentication Api only with Attribute.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseMiddleware<AuthenticationApi>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
