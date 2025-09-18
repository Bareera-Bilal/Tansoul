using System;
using System.Text.Json;
using Microsoft.Extensions.Options;
using P1WEBMVC.Models.DomainModels;

namespace P1WEBMVC.Services;

public class ChatGPTService
{
    private readonly string _apiKey;

    public ChatGPTService(IOptions<OpenAISettings> options)
    {
        _apiKey = options.Value.ApiKey;
    }

    public async Task<string> AskAsync(string userPrompt)
    {
        using var http = new HttpClient();
        http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var body = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful medical assistant for a healthcare website." },
                new { role = "user", content = userPrompt }
            }
        };

        var response = await http.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", body);
        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        var content = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        return content ?? string.Empty;
    }
}
