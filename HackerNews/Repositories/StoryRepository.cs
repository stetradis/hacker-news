using HackerNews.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private HttpClient _client = new HttpClient();
        private readonly string _apiBaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private readonly string _apiJson = ".json?print=pretty";
        private readonly string _apiLimit = "&orderBy=\"$key\"&limitToFirst=100";
        public StoryRepository()
        {
            _client.BaseAddress = new Uri(_apiBaseUrl);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }

        public async Task<HttpResponseMessage> GetStoriesByType(string storyType)
        {
            return await _client.GetAsync($"{storyType}stories{_apiJson}{_apiLimit}");
        }

        public async Task<HttpResponseMessage> GetStoryById(int id)
        {
            return await _client.GetAsync($"item/{id}{_apiJson}");
        }
    }
}
