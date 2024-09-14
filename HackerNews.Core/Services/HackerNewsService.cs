using HackerNews.Core.Interfaces;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace HackerNews.Core.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HackerNewsService> _logger;
        public HackerNewsService(HttpClient httpClient, ILogger<HackerNewsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Story>> GetNewestStories(string searchTerm, int page, int pageSize)
        {
            _logger.LogInformation("Fetching newest stories from Hacker News API");
            try
            {
                var idsResponse = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/newstories.json");
                var ids = JsonSerializer.Deserialize<List<int>>(idsResponse);

                var stories = new List<Story>();
                foreach (var id in ids.Take(100)) 
                {
                    var storyResponse = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                    var story = JsonSerializer.Deserialize<Story>(storyResponse);
                    if (story != null && story.url != null) 
                    {
                        stories.Add(story);
                    }
                }
               
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    stories = stories
                        .Where(story => story.title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                _logger.LogInformation("Fetched {Count} stories", stories.Count);
                return stories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stories from Hacker News API");
                throw;
                
            }
        }
    }
}
