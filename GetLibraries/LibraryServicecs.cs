using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetLibraries;

class LibraryServicecs(HttpClient httpClient)
{
    public async Task<List<LibraryModel>> GetLibraries()
    {
        var response = await httpClient.GetAsync("https://codeTraveler.io/MauiLibraries");
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        var libraries = await JsonSerializer.DeserializeAsync<List<LibraryModel>>(stream);

        return libraries ?? throw new InvalidOperationException("Libraries cannot be null");
    }
}

class LibraryModel
{
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required string Url { get; set; } = string.Empty;
}
