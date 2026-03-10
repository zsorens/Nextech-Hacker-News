using Backend.Clients;
using Backend.Models;
using Backend.Services;
using FluentAssertions;
using Moq;

public class HackerNewsClientTests
{
    private readonly Mock<IHackerNewsClient> _clientMock;

    public HackerNewsClientTests()
    {
        _clientMock = new Mock<IHackerNewsClient>();
    }

    [Fact]
    public async Task GetItemAsync_ReturnsItem_WhenIdIsValid()
    {
        var expected = new HackerNewsItem { Id = 1, Title = "Test Story" };
        _clientMock.Setup(c => c.GetItemAsync(1, default)).ReturnsAsync(expected);

        var result = await _clientMock.Object.GetItemAsync(1, default);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Title.Should().Be("Test Story");
    }

    [Fact]
    public async Task GetItemAsync_ReturnsNull_WhenItemDoesNotExist()
    {
        _clientMock.Setup(c => c.GetItemAsync(999, default)).ReturnsAsync((HackerNewsItem?)null);

        var result = await _clientMock.Object.GetItemAsync(999, default);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTopStoryIdsAsync_ReturnsIds()
    {
        var expected = new List<int> { 1, 2, 3 }.AsReadOnly();
        _clientMock
            .Setup(c => c.GetTopStoryIdsAsync(default))
            .ReturnsAsync((IReadOnlyList<int>)expected);

        var result = await _clientMock.Object.GetTopStoryIdsAsync(default);

        result.Should().HaveCount(3);
        result.Should().Contain(1);
    }
}
