using ApiTest.Api.Controllers;
using ApiTest.Commands.Students.Aggregate;
using ApiTest.Models;
using ApiTest.Queries.Students.GetStats;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ApiTest.Api.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<ISender> senderMock;
        private readonly StudentsController sut;

        public StudentsControllerTests()
        {
            this.senderMock = new Mock<ISender>();
            this.sut = new StudentsController(this.senderMock.Object);
        }

        [Fact]
        public async Task GetStudentsStats_WhenCalled_ShouldSendQuery()
        {
            // Arrange

            this.senderMock
                .Setup(m => m.Send(It.IsAny<GetStudentsStatsQuery>(), default))
                .ReturnsAsync(It.IsAny<StudentsAggregate>());

            // Act
            await this.sut.GetStudentsStats();

            // Assert
            this.senderMock.Verify(x => x.Send(It.IsAny<GetStudentsStatsQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task GetStudentsStats_WhenExistsAggregation_ShouldBeOkObjectResult()
        {
            // Arrange
            this.senderMock
                .Setup(m => m.Send(It.IsAny<GetStudentsStatsQuery>(), default))
                .ReturnsAsync(new StudentsAggregate());

            // Act
            var actual = await this.sut.GetStudentsStats();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoMoqData]
        public async Task PostStudentsStats_WhenCalled_ShouldSendCommand(AggregateStudentsCommand command)
        {
            // Arrange
            this.senderMock
                .Setup(m => m.Send(command, default))
                .ReturnsAsync(It.IsAny<AggregateStudentsResult>());

            // Act
            await this.sut.PostStudentsStats(command);

            // Assert
            this.senderMock.Verify(x => x.Send(It.IsAny<AggregateStudentsCommand>(), default), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task PostStudentsStats_ShouldBeOkObjectResult(AggregateStudentsCommand command)
        {
            // Arrange
            this.senderMock
                .Setup(m => m.Send(command, default))
                .ReturnsAsync(new AggregateStudentsResult(true));

            // Act
            var actual = await this.sut.PostStudentsStats(command);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
        }

    }
}
