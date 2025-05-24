using System.Net.Http.Json;
using Shelfish.Api.Services.Interfaces;

namespace Shelfish.Api.Services;

public class GoogleBooksService : IGoogleBooksService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public GoogleBooksService(HttpClient http, IConfiguration cfg)
    {
        _http   = http;
        _apiKey = cfg["GOOGLE_BOOKS_API_KEY"] ?? string.Empty;
    }

    public async Task<GoogleBook?> FetchAsync(string isbn)
    {
        // Step 1: search to get volume ID
        var searchUrl = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}&key={_apiKey}";
        var searchRes = await _http.GetFromJsonAsync<SearchRoot>(searchUrl);

        var id = searchRes?.Items?.FirstOrDefault()?.Id;
        if (id is null) return null;

        // Step 2: fetch details
        var detailUrl = $"https://www.googleapis.com/books/v1/volumes/{id}?key={_apiKey}";
        return await _http.GetFromJsonAsync<GoogleBook>(detailUrl);
    }

    /* ---- private helper record ---- */
    private record SearchRoot(List<Item>? Items)
    {
        public record Item(string Id);
    }
}
