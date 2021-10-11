using ApiTest.Models;
using ApiTest.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTest.Queries.Students.GetStats
{
    public class GetStudentsStatsQueryHandler : IRequestHandler<GetStudentsStatsQuery, StudentsAggregate>
    {
        private readonly IConfigurationRoot _configRoot;
        private readonly IHttpClientFactory _httpClientFactory;
        readonly ILogger<GetStudentsStatsQueryHandler> _logger;

        private string GetStudentsUrl => _configRoot?[Constants.TESTAPI_GET_STUDENTS_URL] ?? string.Empty;

        public GetStudentsStatsQueryHandler(IConfiguration configRoot, ILogger<GetStudentsStatsQueryHandler> logger, IHttpClientFactory httpClientFactory)
        {
            _configRoot = (IConfigurationRoot)configRoot;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<StudentsAggregate> Handle(GetStudentsStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.TESTAPI_CLIENT);
                var apiResponse = await httpClient.GetAsync(GetStudentsUrl, cancellationToken);
                var students =
                    JsonConvert.DeserializeObject<List<Student>>(
                        await apiResponse.Content.ReadAsStringAsync(cancellationToken));

                return new StudentsAggregate(students);
            }
            catch (Exception e)
            {
                _logger.LogError("Error while handling student stats query. Exception: {0}", e);
                throw;
            }

        }
    }
}
