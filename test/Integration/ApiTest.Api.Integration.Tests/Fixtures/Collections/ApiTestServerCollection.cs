using Xunit;

namespace ApiTest.Api.Integration.Tests.Fixtures.Collections
{
    [CollectionDefinition(nameof(ApiTestServerCollection))]
    public class ApiTestServerCollection : ICollectionFixture<ApiTestServerFixture>
    {
    }
}
