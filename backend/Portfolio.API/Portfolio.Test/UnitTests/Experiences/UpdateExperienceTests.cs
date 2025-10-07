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
    public class UpdateExperienceTests
    {
        [Fact]
        public void Execute_ShouldUpdateExperience_WhenDataIsValid()
        {
            // Arrange
            var experience = new Experience
            {
                Id = 1,
                Title = "Software Engineer",
                Company = "Nexton",
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2023, 1, 1)
            };

            var repositoryMock = new Mock<IExperienceRepository>();
            repositoryMock.Setup(r => r.GetById(experience.Id)).Returns(experience);

            var useCase = new UpdateExperience(repositoryMock.Object);

            // Act
            useCase.Execute(experience);

            // Assert
            repositoryMock.Verify(r => r.Update(experience), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenExperienceDoesNotExist()
        {
            // Arrange
            var experience = new Experience
            {
                Id = 99,
                Title = "Ghost Job",
                Company = "Nowhere",
                StartDate = new DateTime(2022, 1, 1)
            };

            var repositoryMock = new Mock<IExperienceRepository>();
            repositoryMock.Setup(r => r.GetById(experience.Id)).Returns((Experience?)null);

            var useCase = new UpdateExperience(repositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(experience));
            Assert.Equal("Experience not found.", exception.Message);

            repositoryMock.Verify(r => r.Update(It.IsAny<Experience>()), Times.Never);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenStartDateIsInFuture()
        {
            // Arrange
            var experience = new Experience
            {
                Id = 1,
                Title = "Future Job",
                Company = "SpaceX",
                StartDate = DateTime.Now.AddDays(5)
            };

            var repositoryMock = new Mock<IExperienceRepository>();
            repositoryMock.Setup(r => r.GetById(experience.Id)).Returns(experience);

            var useCase = new UpdateExperience(repositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(experience));
            Assert.Equal("Start date cannot be in the future.", exception.Message);

            repositoryMock.Verify(r => r.Update(It.IsAny<Experience>()), Times.Never);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenEndDateIsBeforeStartDate()
        {
            // Arrange
            var experience = new Experience
            {
                Id = 1,
                Title = "Weird Job",
                Company = "X Corp",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2022, 1, 1) // EndDate avant StartDate
            };

            var repositoryMock = new Mock<IExperienceRepository>();
            repositoryMock.Setup(r => r.GetById(experience.Id)).Returns(experience);

            var useCase = new UpdateExperience(repositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(experience));
            Assert.Equal("End date cannot be earlier than start date.", exception.Message);

            repositoryMock.Verify(r => r.Update(It.IsAny<Experience>()), Times.Never);
        }
    }
}
