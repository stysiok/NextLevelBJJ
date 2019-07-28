using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NextLevelBJJ.Api;
using NextLevelBJJ.Application.PassTypes.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NextLevelBJJ.Tests.Integration.Controllers
{
    public class PassTypesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PassTypesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(b => b.UseEnvironment("test"));
        }

        [Fact]
        public async Task get_passtypes_should_return_empty_collection()
        {
            var httpClient = _factory.CreateClient();

            var response = await httpClient.GetAsync("api/passtypes");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var passTypes = JsonConvert.DeserializeObject<IEnumerable<PassTypeDto>>(content);

            passTypes.Should().BeEmpty();
        }
    }
}

