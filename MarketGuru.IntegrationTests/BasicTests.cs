using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MarketGuru.IntegrationTests
{
    public class BasicTests
    : IClassFixture<WebApplicationFactory<MarketGuru.Startup>>
    {
        private readonly WebApplicationFactory<MarketGuru.Startup> _factory;

        public BasicTests(WebApplicationFactory<MarketGuru.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }
}
