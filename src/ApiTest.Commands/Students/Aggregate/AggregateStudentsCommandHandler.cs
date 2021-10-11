using ApiTest.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTest.Commands.Students.Aggregate
{
    public class AggregateStudentsCommandHandler : IRequestHandler<AggregateStudentsCommand, AggregateStudentsResult>
    {
        private readonly IConfigurationRoot _configRoot;
        private readonly IHttpClientFactory _httpClientFactory;
        readonly ILogger<AggregateStudentsCommandHandler> _logger;

        private string PostAggregationUrl => _configRoot?[Constants.TESTAPI_POST_AGGREGATION_URL] ?? string.Empty;

        public AggregateStudentsCommandHandler(IConfiguration configRoot, ILogger<AggregateStudentsCommandHandler> logger, IHttpClientFactory httpClientFactory)
        {
            _configRoot = (IConfigurationRoot)configRoot;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AggregateStudentsResult> Handle(AggregateStudentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                HttpContent body = new StringContent(JsonConvert.SerializeObject(request.StudentAggregate));
                body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var httpClient = _httpClientFactory.CreateClient(Constants.TESTAPI_CLIENT);
                var apiResponse = await httpClient.PutAsync(PostAggregationUrl, body, cancellationToken);
                return new AggregateStudentsResult(apiResponse.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError("Error while submitting students aggregation. Exception: {0}", e);
                throw;
            }
        }
    }
}
