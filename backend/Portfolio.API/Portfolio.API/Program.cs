using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Portfolio.Application.UseCases.Experiences;
using Portfolio.Application.UseCases.Users;
using Portfolio.Application.UseCases.VersionCVs;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection de dépendances
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVersionCVRepository, VersionCVRepository>();
builder.Services.AddScoped<AddUser>();
builder.Services.AddScoped<GetUsers>();
builder.Services.AddScoped<AddVersionCV>();
builder.Services.AddScoped<GetVersionsByUser>();
builder.Services.AddScoped<GetExperiences>();
builder.Services.AddScoped<GetExperienceById>();
builder.Services.AddScoped<AddExperience>();
builder.Services.AddScoped<UpdateExperience>();
builder.Services.AddScoped<DeleteExperience>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1");
        options.RoutePrefix = string.Empty; // Swagger sera dispo à la racine "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

public partial class Program
{
    protected Program() { }
} // Pour les tests d'intégration