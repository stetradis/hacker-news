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
        public async Task<int[]> GetNewStoryIds()
        {
            int[] ids = new int[] { };
            string storyType = "new";
           
            var newStoryIds = await _repo.GetStoriesByType(storyType);
            if (newStoryIds.IsSuccessStatusCode)
            {
                string newStoryIdsResult = newStoryIds.Content.ReadAsStringAsync().Result;
                ids = JsonConvert.DeserializeObject<int[]>(newStoryIdsResult);
            }
            return ids;
        }

        [HttpGet("stories")]  // GET /api/<StoryController>/story
        public async Task<List<Story>> GetStories()
        {
            List<Story> stories = new List<Story>();
            var ids = (await GetNewStoryIds());

            var storyResultList = await Task.WhenAll(ids.Select(GetStoriesById));
            
            return storyResultList.ToList();
        }



        // GET api/<StoryController>/5
        [HttpGet("{id}")]
        public async Task<Story> GetStoriesById(int id)
        {
            return await _cache.GetOrCreateAsync<Story>(id,
                async cacheEntry =>
                {
                    Story story = new Story();

                    var cacheResponse = await _repo.GetStoryById(id);
                    if (cacheResponse.IsSuccessStatusCode)
                    {
                        var result = cacheResponse.Content.ReadAsStringAsync().Result;
                        story = JsonConvert.DeserializeObject<Story>(result);                  
                    }

                    if (story != null)
                    {
                        return story;
                    }
                    else
                    {
                        story = new Story();
                        return story;
                    }
                    
                });
        }

    }
}
