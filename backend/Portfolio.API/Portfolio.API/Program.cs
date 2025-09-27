using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Portfolio.Application.UseCases;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection de d�pendances
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
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
        options.RoutePrefix = string.Empty; // Swagger sera dispo � la racine "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

public partial class Program
{
    protected Program() { }
} // Pour les tests d'int�gration