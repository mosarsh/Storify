using Storify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storify.Data
{
    public interface IStorifyRepository
    {
        User GetUser(int userId);
        List<GroupModel> GetGroups(int userId);
        Task AddStory(StoryModel model, int userId);
        Task Delete(int storyId);
        List<StoryModel> GetStories(int userId);
        Task AddOrUpdate(GroupModel groupModel);
        List<GroupModel> GetGroups();
        StoryModel GetStory(int storyId, int userId);
        Task UpdateStory(StoryModel model, int userId);
    }
}
