using Microsoft.AspNetCore.Mvc.Testing;
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
        private readonly PortfolioDbContext _db;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            var scopeFactory = factory.Services.GetService<IServiceScopeFactory>()!;
            using var scope = scopeFactory.CreateScope();
            _db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();

            // Prépare un client avec DB initialisée et seedée
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                    db.Database.EnsureCreated();

                    // Reset DB
                    db.Users.RemoveRange(db.Users);
                    db.SaveChanges();

                    // Seed user
                    db.Users.Add(new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alice Dupont"
                    });
                    db.SaveChanges();
                });
            }).CreateClient();
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
            // On prend l'ID seedé
            var userId = _db.Users.First().Id;

            var response = await _client.GetAsync($"/api/users/{userId}");

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
