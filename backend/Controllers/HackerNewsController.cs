using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Controllers;

[ApiController]
[Route("api/hacker-news")]
public class HackerNewsController : ControllerBase
{
    private readonly HackerNewsService _service;
    private readonly IMemoryCache _cache;

    public HackerNewsController(HackerNewsService service, IMemoryCache cache)
    {
        _service = service;
        _cache = cache;
    }

    /// <summary>Returns a single item by ID.</summary>
    [HttpGet("item/{id:int}")]
    public async Task<IActionResult> GetItem(int id, CancellationToken ct = default)
    {
        var item = await _service.GetItemAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Returns newest stories.</summary>
    [HttpGet("new")]
    public async Task<IActionResult> GetNewStories(
        [FromQuery] int page = 1,
        [FromQuery] int count = 25,
        CancellationToken ct = default
    )
    {
        var ids = await _cache.GetOrCreateAsync(
            $"new:{count}:{page}",
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1); // smaller time so that we have the freshest stores
                return _service.GetNewStories(page, count, ct);
            }
        );

        return Ok(ids);
    }
}
