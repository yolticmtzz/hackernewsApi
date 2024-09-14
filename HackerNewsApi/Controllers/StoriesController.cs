using HackerNews.Core.Interfaces;
using HackerNews.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly ILogger<StoriesController> _logger;
        public StoriesController(IHackerNewsService hackerNewsService, ILogger<StoriesController> logger)
        {
            _hackerNewsService = hackerNewsService;
            _logger = logger;
        }

        [HttpGet("newest")]
        public async Task<IActionResult> GetNewestStories( [FromQuery] int page = 1, [FromQuery] int pageSize = 10,[FromQuery] string searchTerm = null)
        {
            _logger.LogInformation("Fetching newest stories from Hacker News Controller");
            try
            {
                var stories = await _hackerNewsService.GetNewestStories(searchTerm,page, pageSize);
                return Ok(stories);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error fetching stories from Ctonroller News API");

                return StatusCode(500, "Internal server error");
            }
        }
    }

}
