using System.Text.Json;
using Backend.Clients;
using Backend.Models;

namespace Backend.Services
{
    public class HackerNewsService
    {
        private readonly IHackerNewsClient _client;

        public HackerNewsService(IHackerNewsClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyList<int>> GetTopStoryIdsAsync(CancellationToken ct = default)
        {
            return await _client.GetTopStoryIdsAsync(ct);
        }

        public async Task<IReadOnlyList<int>> GetNewStoryIdsAsync(CancellationToken ct = default)
        {
            return await _client.GetNewStoryIdsAsync(ct);
        }

        public async Task<HackerNewsItem?> GetItemAsync(int id, CancellationToken ct = default)
        {
            return await _client.GetItemAsync(id, ct);
        }

        public async Task<IReadOnlyList<HackerNewsPreivew>> GetNewStories(
            int page = 1,
            int count = 25,
            CancellationToken ct = default
        )
        {
            var ids = await GetNewStoryIdsAsync(ct);
            var items = await Task.WhenAll(
                ids.Skip((page - 1) * count).Take(count).Select(id => GetItemAsync(id, ct))
            );
            return FormattedPreviews(items);
        }

        private List<HackerNewsPreivew> FormattedPreviews(HackerNewsItem?[] items)
        {
            return items
                .Where(item => item is not null)
                .Select(item => new HackerNewsPreivew
                {
                    Id = item!.Id,
                    Title = item.Title,
                    Url = item.Url,
                    Time = item.Time
                })
                .ToList();
        }
    }
}
