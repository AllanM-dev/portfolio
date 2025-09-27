using Moq;
using Portfolio.Application.UseCases;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Test.UnitTests.Experiences
{
    public class GetExperiencesTests
    {
        [Fact]
        public void Execute_ShouldReturnExperiencesOrderedByStartDate()
        {
            var experiences = new List<Experience>
            {
                new Experience {Id = 1, Title = "Old Job", StartDate = new DateTime(2022, 1, 1)},
                new Experience {Id = 2, Title = "New Job", StartDate = new DateTime(2024, 1, 1)},
            };

            var repositoryMock = new Mock<IExperienceRepository>();
            repositoryMock.Setup(r => r.GetExperiences()).Returns(experiences);

            var useCase = new GetExperiences(repositoryMock.Object);

            var result = useCase.Execute().ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("New Job", result[0].Title);
        }
    }
}
