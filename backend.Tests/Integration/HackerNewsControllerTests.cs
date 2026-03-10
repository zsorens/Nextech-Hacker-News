using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

public class HackerNewsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HackerNewsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetNewStories_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/hacker-news/new?page=1&count=5");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetItem_ReturnsNotFound_WhenIdInvalid()
    {
        var response = await _client.GetAsync("/api/hacker-news/item/0");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetNewStories_ReturnsPaginatedResults()
    {
        var response = await _client.GetAsync("/api/hacker-news/new?page=1&count=5");
        var content = await response.Content.ReadFromJsonAsync<List<object>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().HaveCountLessOrEqualTo(5);
    }
}
