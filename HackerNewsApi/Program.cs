using HackerNews.Core.Interfaces;
using HackerNews.Core.Models;
using HackerNews.Core.Services;
using HackerNewsApi.Middleware;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Configuration
    .AddJsonFile("secret.json", optional: true, reloadOnChange: true);

builder.Services.Configure<ApiKeyOptions>(builder.Configuration.GetSection("ApiKey"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<HackerNewsService>();
builder.Services.AddScoped<IHackerNewsService, CachedHackerNewsService>(); 

builder.Services.AddMemoryCache(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowLocalhost4200");
app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
