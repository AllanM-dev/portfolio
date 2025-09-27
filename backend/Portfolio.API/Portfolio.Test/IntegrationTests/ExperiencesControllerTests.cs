using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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

        public ExperiencesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    using (var scope = services.BuildServiceProvider().CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
                        db.Database.EnsureCreated();
                    }
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
        }
    }
}
