using HackerNews.Core.Interfaces;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Core.Services
{
    public class CachedHackerNewsService : IHackerNewsService
    {
        private readonly HackerNewsService _inner;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachedHackerNewsService> _logger;
        public CachedHackerNewsService(HackerNewsService inner, IMemoryCache cache, ILogger<CachedHackerNewsService> logger)
        {
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<Story>> GetNewestStories(string searchTerm,int page, int pageSize)
        {
            _logger.LogInformation("Fetching newest stories from Cache Hacker News API");
            try
            {
               
                var cacheKey = "allNewestStories";

                
                if (_cache.TryGetValue(cacheKey, out List<Story> cachedStories))
                {
                    _logger.LogInformation("Returning cached stories from memory");

                    
                    return cachedStories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }

                _logger.LogInformation("Fetching new stories from API");

                
                var stories = await _inner.GetNewestStories(null,1, 100); 

                
                _cache.Set(cacheKey, stories, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

                
                return stories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stories from Cache Hacker News API");
                throw;
            }
        }


    }

}
