using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace ApiTest.Api.Integration.Tests.Fixtures
{
    public class ApiTestServerFixture
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory = new WebApplicationFactory<Startup>();

        public ApiTestServerFixture()
        {
            this._webApplicationFactory = new WebApplicationFactory<Startup>();
            this.HttpClient = this._webApplicationFactory.CreateClient();
        }

        public HttpClient HttpClient { get; private set; }
    }
}
