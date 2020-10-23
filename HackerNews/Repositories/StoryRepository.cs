using HackerNews.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private HttpClient _client = new HttpClient();
        private readonly string _apiBaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private readonly string _apiJson = ".json?print=pretty";
        public StoryRepository()
        {
            _client.BaseAddress = new Uri(_apiBaseUrl);
        }

        public async Task<HttpResponseMessage> GetStoriesByType(string storyType)
        {
            return await _client.GetAsync($"{storyType}stories{_apiJson}");
        }

        public async Task<HttpResponseMessage> GetStoryById(int id)
        {
            return await _client.GetAsync($"item/{id}{_apiJson}");
        }
    }
}
