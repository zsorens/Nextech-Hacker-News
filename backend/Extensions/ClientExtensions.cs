using Backend.Clients;

namespace Backend.Extensions;

public static class ClientExtensions
{
    private const string BaseAddress = "https://hacker-news.firebaseio.com/v0/";

    public static IServiceCollection AddHackerNewsClient(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<HackerNewsClient>(client =>
        {
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(600);
        });

        return builder.Services;
    }
}
