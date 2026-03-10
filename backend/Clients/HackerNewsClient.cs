using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Backend.Models;

namespace Backend.Clients;

public interface IHackerNewsClient
{
    Task<HackerNewsItem?> GetItemAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<int>> GetTopStoryIdsAsync(CancellationToken ct = default);
    Task<IReadOnlyList<int>> GetNewStoryIdsAsync(CancellationToken ct = default);
    Task<IReadOnlyList<int>> GetStoryIdsAsync(string endpoint, CancellationToken ct = default);
}

public class HackerNewsClient(HttpClient http) : IHackerNewsClient
{
    public async Task<IReadOnlyList<int>> GetTopStoryIdsAsync(CancellationToken ct = default)
    {
        return await GetStoryIdsAsync("topstories", ct);
    }

    public async Task<IReadOnlyList<int>> GetNewStoryIdsAsync(CancellationToken ct = default)
    {
        return await GetStoryIdsAsync("newstories", ct);
    }

    public async Task<HackerNewsItem?> GetItemAsync(int id, CancellationToken ct = default)
    {
        return await http.GetFromJsonAsync<HackerNewsItem>($"item/{id}.json", ct);
    }

    public async Task<IReadOnlyList<int>> GetStoryIdsAsync(string endpoint, CancellationToken ct)
    {
        var ids = await http.GetFromJsonAsync<int[]>($"{endpoint}.json", ct);
        return ids ?? [];
    }
}
