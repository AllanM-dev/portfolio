using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.DTOs.VersionCvs;
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
    public class VersionCVsControllerTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly string _dbPath;
        private readonly Guid _userId;
        private readonly int _versionId;

        public VersionCVsControllerTests(WebApplicationFactory<Program> factory)
        {
            _dbPath = Path.Combine(Path.GetTempPath(), $"portfolio_versioncvs_test_{Guid.NewGuid()}.db");

            Guid seededUserId = Guid.NewGuid();
            int seededVersionId = -999;

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Supprimer l’ancien DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<PortfolioDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Créer un nouveau contexte SQLite temporaire
                    services.AddDbContext<PortfolioDbContext>(options =>
                        options.UseSqlite($"Data Source={_dbPath}"));

                    // Seed des données
                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();

                        var user = new User
                        {
                            Id = seededUserId,
                            Name = "Alice Dupont"
                        };
                        db.Users.Add(user);

                        var version = new VersionCV
                        {
                            UserId = seededUserId,
                            Name = "v1"
                        };
                        db.VersionsCV.Add(version);
                        db.SaveChanges();

                        seededVersionId = version.Id;
                    }
                });
            }).CreateClient();

            _userId = seededUserId;
            _versionId = seededVersionId;
        }

        [Fact]
        public async Task GetByUser_ShouldReturnOkAndList()
        {
            var response = await _client.GetAsync($"/api/versioncvs/user/{_userId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var versions = await response.Content.ReadFromJsonAsync<List<VersionCVDto>>();
            Assert.NotNull(versions);
            Assert.Single(versions);
            Assert.Equal("v1", versions.First().Name);
        }

        [Fact]
        public async Task GetById_ExistingId_ShouldReturnOk()
        {
            var response = await _client.GetAsync($"/api/versioncvs/{_versionId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var version = await response.Content.ReadFromJsonAsync<VersionCVDto>();
            Assert.NotNull(version);
            Assert.Equal("v1", version.Name);
        }

        [Fact]
        public async Task GetById_NonExistingId_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync($"/api/versioncvs/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateVersionCV_ValidDto_ShouldReturnCreated()
        {
            var newVersion = new CreateVersionCVDto
            {
                UserId = _userId,
                Name = "v2"
            };

            var response = await _client.PostAsJsonAsync("/api/versioncvs", newVersion);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<VersionCVDto>();
            Assert.NotNull(created);
            Assert.Equal("v2", created.Name);
        }

        [Fact]
        public async Task CreateVersionCV_InvalidDto_ShouldReturnBadRequest()
        {
            var invalidVersion = new CreateVersionCVDto
            {
                UserId = _userId,
                Name = "" // invalide
            };

            var response = await _client.PostAsJsonAsync("/api/versioncvs", invalidVersion);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
