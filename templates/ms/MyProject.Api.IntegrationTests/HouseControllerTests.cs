using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace MyProject.Api.IntegrationTests
{
    public class HouseControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public HouseControllerTests(WebApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task List_ShouldReturnAllHouses()
        {
            var response = await Client.GetAsync("/House");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var results = await response.Content.ReadAsStringAsync();
            results.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithSingleHouse_WhenPassedAValidIdThatExists()
        {
            var testId = 123;
            var response = await Client.GetAsync($"/House/{testId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var results = await response.Content.ReadAsStringAsync();
            results.Should().Contain("This is a nice house!");
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenPassedAnIdThatDoesNotExist()
        {
            var testId = 999;
            var response = await Client.GetAsync($"/House/{testId}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var results = await response.Content.ReadAsStringAsync();
            results.Should().Contain("404");
        }
    }
}
