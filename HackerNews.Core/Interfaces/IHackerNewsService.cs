using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Core.Interfaces
{
    public interface IHackerNewsService
    {
        Task<List<Story>> GetNewestStories(string searchTerm, int page, int pageSize);
    }
}
