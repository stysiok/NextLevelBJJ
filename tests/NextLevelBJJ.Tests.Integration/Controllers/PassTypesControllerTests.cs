using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NextLevelBJJ.Api;
using NextLevelBJJ.Application.PassTypes.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NextLevelBJJ.Tests.Integration.Controllers
{
    public class PassTypesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly Fixture _fixture;

        public PassTypesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(b => b.UseEnvironment("test"));
            _fixture = new Fixture();
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

        [Fact]
        public async Task create_passType_should_create_passType()
        {
            var httpClient = _factory.CreateClient();

            var passTypeFixture = _fixture.Create<PassTypeDto>();

            var stringContent = new StringContent(JsonConvert.SerializeObject(passTypeFixture), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/passtypes", stringContent);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().Equals($"api/passtypes/{passTypeFixture.Id}");
        }

        [Fact]
        public async Task get_passType_should_return_passType()
        {
            var httpClient = _factory.CreateClient();

            var passTypeFixture = _fixture.Create<PassTypeDto>();

            var stringContent = new StringContent(JsonConvert.SerializeObject(passTypeFixture), Encoding.UTF8, "application/json");

            var postResponse = await httpClient.PostAsync("api/passtypes", stringContent);

            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var response = await httpClient.GetAsync(postResponse.Headers.Location);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var passTypeResponse = JsonConvert.DeserializeObject<PassTypeDto>(stringResponse);

            passTypeResponse.Should().Equals(passTypeFixture);
        }
        
        [Fact]
        public async Task delete_passtype_should_remove_it()
        {
            var httpClient = _factory.CreateClient();

            var passTypeFixture = _fixture.Create<PassTypeDto>();

            var stringContent = new StringContent(JsonConvert.SerializeObject(passTypeFixture), Encoding.UTF8, "application/json");

            var postResponse = await httpClient.PostAsync("api/passtypes", stringContent);

            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var response = await httpClient.DeleteAsync(postResponse.Headers.Location);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

    }
}

