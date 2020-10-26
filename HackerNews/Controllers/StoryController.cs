using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Interfaces;
using HackerNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private IStoryRepository _repo;
        private IMemoryCache _cache;

        public StoryController(IStoryRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        // GET: api/<StoryController>
        [HttpGet]
        public async Task<int[]> GetNewStoryIds(string storyType)
        {
            int[] ids = new int[] { };
           
            var newStoryIds = await _repo.GetStoriesByType(storyType);
            if (newStoryIds.IsSuccessStatusCode)
            {
                string newStoryIdsResult = newStoryIds.Content.ReadAsStringAsync().Result;
                ids = JsonConvert.DeserializeObject<int[]>(newStoryIdsResult);
            }
            return ids;
        }

        // GET /api/<StoryController>/story/new
        [HttpGet("stories/{storyType}")]  
        public async Task<List<Story>> GetStories(string storyType)
        {
            List<Story> stories = new List<Story>();
            var ids = await GetNewStoryIds(storyType);
            
            foreach (int id in ids)
            {
                var storyResult = await GetStoriesById(id);
                if (storyResult != null)
                {
                    stories.Add(storyResult);
                }
            }          
            return stories;
        }

        // GET api/<StoryController>/5
        [HttpGet("{id}")]
        public async Task<Story> GetStoriesById(int id)
        {
            Story story = new Story();
          
            if (!_cache.TryGetValue(id, out story))
            {
                var getResponse = await _repo.GetStoryById(id);
                if (getResponse.IsSuccessStatusCode)
                {
                    var responseResult = getResponse.Content.ReadAsStringAsync().Result;
                    story = JsonConvert.DeserializeObject<Story>(responseResult);
                }
                
                _cache.Set(id, story);
            }

            return story;
        }
    }
}
