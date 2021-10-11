using ApiTest.Api.Integration.Tests.Fixtures;
using ApiTest.Api.Integration.Tests.Fixtures.Collections;
using ApiTest.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ApiTest.Api.Integration.Tests.Controllers
{
    [Collection(nameof(ApiTestServerCollection))]
    public class StudentsControllerTests
    {
        private readonly ApiTestServerFixture _fixture;

        public StudentsControllerTests(ApiTestServerFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task GetStudentsStats_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this._fixture.HttpClient.BaseAddress ?? throw new InvalidOperationException("Base address required"))
            {
                Path = "api/students/stats"
            };

            // Act
            var response = await this._fixture.HttpClient.GetAsync(uriBuilder.Uri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostStudentsStats_ValidRequest_HttpStatusCodeOK()
        {
            // Arrange
            var uriBuilder = new UriBuilder(this._fixture.HttpClient.BaseAddress ?? throw new InvalidOperationException("Base address required"))
            {
                Path = "api/students/aggregate"
            };
            var testAggregation = new StudentsAggregate
            {
                YearWithHighestAttendance = 2011,
                YearWithHighestOverallGpa = 2016,
                Top10StudentIdsWithHighestGpa = new List<int>(new[] { 4, 10, 11, 20, 18, 13, 24, 6, 22, 9 }),
                StudentIdMostInconsistent = 15
            };
            HttpContent body = new StringContent(JsonConvert.SerializeObject(testAggregation));
            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await this._fixture.HttpClient.PostAsync(uriBuilder.Uri, body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
