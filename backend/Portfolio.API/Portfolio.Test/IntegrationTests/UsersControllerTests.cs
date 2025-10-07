using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.DTOs.Users;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.Test.IntegrationTests
{
    public class UsersControllerTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly Guid _userId;
        private readonly string _dbPath;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            // 🔹 Crée une base SQLite temporaire unique pour ce test
            _dbPath = Path.Combine(Path.GetTempPath(), $"portfolio_users_test_{Guid.NewGuid()}.db");

            Guid seededUserId = Guid.NewGuid();

            // 🔹 Crée un client avec un environnement de test configuré
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // 1️⃣ Supprimer l’enregistrement d’origine du DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<PortfolioDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    // 2️⃣ Ajouter une nouvelle instance du DbContext pointant vers le fichier SQLite temporaire
                    services.AddDbContext<PortfolioDbContext>(options =>
                        options.UseSqlite($"Data Source={_dbPath}"));

                    // 3️⃣ Créer et seed la base
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    db.Users.Add(new User
                    {
                        Id = seededUserId,
                        Name = "Alice Dupont"
                    });
                    db.SaveChanges();
                });
            }).CreateClient();

            _userId = seededUserId;
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/api/users");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
            Assert.NotNull(users);
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task GetUserById_ExistingId_ShouldReturnOk()
        {
            var response = await _client.GetAsync($"/api/users/{_userId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            Assert.NotNull(user);
            Assert.Equal("Alice Dupont", user.Name);
        }

        [Fact]
        public async Task GetUserById_NonExistingId_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync($"/api/users/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ValidDto_ShouldReturnCreated()
        {
            var newUser = new CreateUserDto
            {
                Name = "Bob Martin"
            };

            var response = await _client.PostAsJsonAsync("/api/users", newUser);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<UserDto>();
            Assert.NotNull(created);
            Assert.Equal("Bob Martin", created.Name);
        }

        [Fact]
        public async Task CreateUser_InvalidDto_ShouldReturnBadRequest()
        {
            var invalidUser = new CreateUserDto
            {
                Name = "" // vide → invalide
            };

            var response = await _client.PostAsJsonAsync("/api/users", invalidUser);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
