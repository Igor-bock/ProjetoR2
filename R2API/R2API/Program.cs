using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ReiglassWCF;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var m_api_cors = "api_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: m_api_cors,
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", jwt =>
//    {
//        jwt.Authority = "https://localhost:5001";
//        jwt.RequireHttpsMetadata = false;
//        jwt.SaveToken = true;
//        jwt.Events.OnTokenValidated += (a) =>
//            {
                
//                return Task.CompletedTask;
//            };
//        jwt.Audience = "esperanto";
//    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Cargo", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireClaim("role", "Administrador", "Gerente", "Empregado", "Visitante");
    });
});

var m_configuracoes = builder.Configuration.GetSection("Configuracoes");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = "https://localhost:5001";
        opt.Audience = "esperanto";
    });

var app = builder.Build();
app.Logger.LogInformation("Iniciando Backend de R2 Glass em {DT}", DateTime.Now.ToLongTimeString());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(m_api_cors);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
