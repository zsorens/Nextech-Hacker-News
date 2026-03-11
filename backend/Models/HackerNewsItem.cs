namespace Backend.Models;

public class HackerNewsItem //modeld based on github docs for hacker api
{
    public int Id { get; init; }
    public bool Deleted { get; init; }
    public string? Type { get; init; }
    public string? By { get; init; }
    public long Time { get; init; }
    public string? Text { get; init; }
    public bool? Dead { get; init; }
    public int? Parent { get; init; }
    public int? Poll { get; init; }
    public int[]? Kids { get; init; }
    public string? Url { get; init; }
    public int? Score { get; init; }
    public string? Title { get; init; }
    public int[]? Parts { get; init; }
    public int? Descendants { get; init; }
}
