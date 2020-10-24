using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Interfaces
{
    public interface IStoryRepository
    {
        Task<HttpResponseMessage> GetStoriesByType(string storyType);
        Task<HttpResponseMessage> GetStoryById(int id);
    }
}
