using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storify.Data;
using NLog;
using Storify.Model;

namespace Storify.Business
{
    public class StoryLogic
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IStorifyRepository _repo;
        /// <summary>
        /// Logic constructor
        /// </summary>
        /// <param name="repo">Repository interface</param>
        public StoryLogic(IStorifyRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get the list of stories for the user
        /// </summary>
        /// <param name="userId">UserId passed in</param>
        /// <returns>List of stories</returns>
        public List<StoryModel> GetStories(int userId)
        {
            return _repo.GetStories(userId);
        }

        /// <summary>
        /// Get the story details
        /// </summary>
        /// <param name="storyId">story id to passed in</param>
        /// <returns>returns story model</returns>
        public StoryModel GetStory(int storyId, int userId)
        {
            return _repo.GetStory(storyId, userId);
        }

        /// <summary>
        /// Add or update story
        /// </summary>
        /// <param name="model">Story model</param>
        /// <returns>Return void task</returns>
        public async Task AddStory(StoryModel model, int userId)
        {
            await _repo.AddStory(model, userId);
        }

        public async Task UpdateStory(StoryModel model, int userId)
        {
            await _repo.UpdateStory(model, userId);
        }

        public async Task DeleteStory(int storyId)
        {
            await _repo.Delete(storyId);
        }
    }
}
