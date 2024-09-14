using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using HackerNewsApi.Middleware;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace HackerNewsApi.Tests
{
    public class ApiKeyMiddlewareTests
    {
        private readonly Mock<ILogger<ApiKeyMiddleware>> _loggerMock;
        private readonly IConfiguration _configuration;


        public ApiKeyMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<ApiKeyMiddleware>>();

           
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("secret.json");

            _configuration = configurationBuilder.Build();
        }
        [Fact]
        public async Task InvokeAsync_ValidApiKey_InvokesNextMiddleware()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers["ApiKey"] = _configuration.GetValue<string>("ApiKey");

            var next = new Mock<RequestDelegate>();
            var middleware = new ApiKeyMiddleware(next.Object, _configuration);

            // Act
            await middleware.Invoke(context);

            // Assert
            next.Verify(n => n(It.IsAny<HttpContext>()), Times.Once);
            _loggerMock.Verify(l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Never);
        }

        [Fact]
        public async Task InvokeAsync_InvalidApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers["ApiKey"] = "invalid-key";

            var next = new Mock<RequestDelegate>();
            var middleware = new ApiKeyMiddleware(next.Object, _configuration);

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
            
        }
    }
}
