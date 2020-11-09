using HackerNews.Interfaces;
using HackerNews.Models;
using HackerNews.Repositories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HackerNews.Test
{
    public class StoryTest
    {
        private IStoryRepository _storyRepository;

        [SetUp]
        public void Setup()
        {
            _storyRepository = new StoryRepository();
        }

        [Test]
        public void GetSuccessHttpResponseForNewStories()
        {
            var stories = _storyRepository.GetStoriesByType("new");
            var result = stories.Result.IsSuccessStatusCode;

            Assert.IsTrue(result);
        }

        [Test]
        public void GetSuccessHttpResponseForTopStories()
        {
            var stories = _storyRepository.GetStoriesByType("top");
            var result = stories.Result.IsSuccessStatusCode;

            Assert.IsTrue(result);
        }

        [Test]
        public void GetSuccessHttpResponseForJobStories()
        {
            var stories = _storyRepository.GetStoriesByType("job");
            var result = stories.Result.IsSuccessStatusCode;

            Assert.IsTrue(result);
        }

        [Test]
        public void GetFailureHttpResponse_WhenAStoryTypeIsntProvided()
        {
            var stories = _storyRepository.GetStoriesByType("");
            var result = stories.Result.IsSuccessStatusCode;

            Assert.IsFalse(result);
        }

        [Test]
        public void GetNullObjectResponse_WhenAValidStoryIdIsntProvided()
        {
            var story = _storyRepository.GetStoryById(0);
            var response = story.Result.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Story>(response);

            Assert.IsNull(result);
        }

        [Test]
        public void GetSuccessHttpResponse_WhenAValidStoryIdIsProvided()
        {
            var story = _storyRepository.GetStoryById(23007676);
            var result = story.Result.IsSuccessStatusCode;

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenAnItemId_ShouldReturnTheCorrectItem()
        {
            var story = _storyRepository.GetStoryById(23007676);
            var response = story.Result.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Story>(response);

            Assert.AreEqual(23007676, result.Id);
            Assert.AreEqual("Show HN: Treenga – simple task management for remote teams", result.Title);
            Assert.AreEqual("https://treenga.com/", result.Url);
            Assert.AreEqual("wx196", result.By);
        }

        [Test]
        public void GivenANewStoryType_ShouldReturn100NewStoryIds()
        {
            var stories = _storyRepository.GetStoriesByType("new");
            var response = stories.Result.Content.ReadAsStringAsync().Result;
            var storyIds = JsonConvert.DeserializeObject<int[]>(response);
            int countItems = storyIds.Length;

            Assert.AreEqual(countItems, 100);
        }

    }
}