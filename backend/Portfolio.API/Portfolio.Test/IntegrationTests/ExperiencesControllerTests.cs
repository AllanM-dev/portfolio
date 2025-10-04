using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.DTOs.Experiences;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Test.IntegrationTests
{
    public class ExperiencesControllerTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly PortfolioDbContext _db;

        public ExperiencesControllerTests(WebApplicationFactory<Program> factory)
        {
            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>()!;
            using var scope = scopeFactory.CreateScope();
            _db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();

                    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                    db.Database.EnsureCreated();

                    db.Experiences.RemoveRange(db.Experiences);
                    db.SaveChanges();

                    db.Experiences.AddRange(new List<Experience>
                    {
                        new Experience
                        {
                            Title = "Software Engineer",
                            Company = "Tech Corp",
                            StartDate = new DateTime(2020, 1, 1),
                            EndDate = null,
                            Description = "Developed web applications using .NET Core and React."
                        },
                        new Experience
                        {
                            Title = "Junior Developer",
                            Company = "Web Solutions",
                            StartDate = new DateTime(2018, 6, 1),
                            EndDate = new DateTime(2019, 12, 31),
                            Description = "Assisted in the development of client websites and applications."
                        }
                    });
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetExperiences_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/api/experiences");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var experiences = await response.Content.ReadFromJsonAsync<List<Experience>>();
            Assert.NotNull(experiences);
            Assert.NotEmpty(experiences);
        }

        [Fact]
        public async Task GetExperienceById_ExistingId_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/api/experiences/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var experience = await response.Content.ReadFromJsonAsync<ExperienceDto>();
            Assert.NotNull(experience);
            Assert.Equal("Software Engineer", experience.Title);
        }

        [Fact]
        public async Task GetExperienceById_NonExistingId_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync("/api/experiences/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateExperience_ValidDto_ShouldReturnCreated()
        {
            var newExperience = new CreateExperienceDto
            {
                Title = "Développeur Angular",
                Company = "TechCorp",
                Description = "Front-end SPA",
                StartDate = DateTime.UtcNow.AddMonths(-6)
            };

            var response = await _client.PostAsJsonAsync("/api/experiences", newExperience);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<ExperienceDto>();
            Assert.NotNull(created);
            Assert.Equal("Développeur Angular", created.Title);
        }

        [Fact]
        public async Task CreateExperience_InvalidDto_ShouldReturnBadRequest()
        {
            var invalidDto = new CreateExperienceDto
            {
                // Title manquant => modèle invalide
                Company = "TechCorp",
                Description = "Test sans titre",
                StartDate = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/api/experiences", invalidDto);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
