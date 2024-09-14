using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HackerNews.Core.Services;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;

namespace HackerNewsApi.Tests
{
    public class HackerNewsServiceTests
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly HttpClient _httpClient;
        private readonly ILogger<HackerNewsService> _loggerMock;
        private readonly HackerNewsService _service;

        public HackerNewsServiceTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            _httpClient = _mockHttp.ToHttpClient();
            _loggerMock = new Mock<ILogger<HackerNewsService>>().Object;
            _service = new HackerNewsService(_httpClient, _loggerMock);
        }

        [Fact]
        public async Task GetNewestStories_ReturnsStories()
        {
            // Arrange
            var storiesJson = JsonSerializer.Serialize(new List<int> { 1, 2 });
            var storyJson1 = JsonSerializer.Serialize(new Story { id = 1, title = "Test Story", url = "http://example.com" });
            var storyJson2 = JsonSerializer.Serialize(new Story { id = 2, title = "Another Story", url = "http://example.com/2" });

            _mockHttp.When("https://hacker-news.firebaseio.com/v0/newstories.json")
                     .Respond("application/json", storiesJson);

            _mockHttp.When("https://hacker-news.firebaseio.com/v0/item/1.json")
                     .Respond("application/json", storyJson1);

            _mockHttp.When("https://hacker-news.firebaseio.com/v0/item/2.json")
                     .Respond("application/json", storyJson2);

            // Act
            var stories = await _service.GetNewestStories(null,1, 10);

            // Assert
            stories.Should().HaveCount(2); 
            stories[0].title.Should().Be("Test Story");
            stories[1].title.Should().Be("Another Story");
            Mock.Get(_loggerMock).Verify(l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.AtLeastOnce);
        }
    }
}
