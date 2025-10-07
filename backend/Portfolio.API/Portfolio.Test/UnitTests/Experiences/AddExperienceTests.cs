using Moq;
using Portfolio.Application.UseCases.Experiences;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Test.UnitTests.Experiences
{
    public class AddExperienceTests
    {
        [Fact]
        public void Execute_ShouldAddExperience_WhenStartDateIsValid()
        {
            // Arrange
            var experience = new Experience
            {
                Id = 1,
                Title = "Software Engineer",
                Company = "Nexton",
                StartDate = new DateTime(2023, 1, 1)
            };

            var repositoryMock = new Mock<IExperienceRepository>();

            var useCase = new AddExperience(repositoryMock.Object);

            // Act
            useCase.Execute(experience);

            // Assert
            repositoryMock.Verify(r => r.Add(experience), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenStartDateIsInFuture()
        {
            // Arrange
            var experience = new Experience
            {
                Id = 2,
                Title = "Future Job",
                Company = "SpaceX",
                StartDate = DateTime.Now.AddDays(10) // date future
            };

            var repositoryMock = new Mock<IExperienceRepository>();

            var useCase = new AddExperience(repositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(experience));
            Assert.Equal("Start date cannot be in the future.", exception.Message);

            repositoryMock.Verify(r => r.Add(It.IsAny<Experience>()), Times.Never);
        }
    }
}
