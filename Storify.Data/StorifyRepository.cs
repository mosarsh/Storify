using Storify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Transactions;

namespace Storify.Data
{
    public class StorifyRepository : IStorifyRepository
    {
        private StorifyEntities _context;

        public StorifyRepository(StorifyEntities context)
        {
            _context = context;
            _context.Configuration.LazyLoadingEnabled = false;
        }

        #region User

        /// <summary>
        /// Get user by userId
        /// </summary>
        /// <param name="userId">type of int</param>
        /// <returns>returns User entity or null</returns>
        public User GetUser(int userId)
        {

            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Geting the list of groups for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns groups' list</returns>
        public List<GroupModel> GetGroups(int userId)
        {

            return _context.User_Story_Group
                .Where(g => g.UserId == userId)
                .Select(g => new GroupModel
                {
                    GroupId = g.Group.GroupId,
                    Name = g.Group.Name,
                    Description = g.Group.Description,
                })
                .ToList();
        }

        #endregion
        #region Story
        /// <summary>
        /// Add new story
        /// </summary>
        /// <param name="stroy">Story model passed in</param>
        public async Task AddStory(StoryModel model, int userId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var story = new Story()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    PostedOn = DateTime.UtcNow
                };

                _context.Stories.AddOrUpdate(story);
                await _context.SaveChangesAsync();

                var newStory = _context.Stories.FirstOrDefault(s => s.PostedOn == story.PostedOn && s.Title == model.Title);
                await AddUserStoryGroup(userId, newStory.StoryId, model.GroupId);

                scope.Complete();
            }
        }

        /// <summary>
        /// Update story details
        /// </summary>
        /// <param name="model">story model</param>
        /// <param name="userId">user id</param>
        /// <param name="groupId">group id</param>
        /// <returns>returns void task</returns>
        public async Task UpdateStory(StoryModel model, int userId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var story = _context.Stories.FirstOrDefault(s => s.StoryId == model.StoryId);

                if (story != null)
                {
                    story.Title = model.Title;
                    story.Description = model.Description;
                    story.Content = model.Content;
                    story.PostedOn = DateTime.UtcNow;
                    _context.Stories.AddOrUpdate(story);
                    await _context.SaveChangesAsync();
                    await UpdateUserStoryGroup(userId, model.StoryId, model.GroupId);
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Method to add data in user_story_group table
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="storyId">story id</param>
        /// <param name="groupId">group id</param>
        /// <returns>returns void Task</returns>
        private async Task AddUserStoryGroup(int userId, int storyId, int groupId)
        {
            if (groupId != null)
            {
                _context.User_Story_Group.Add(
                    new User_Story_Group
                    {
                        UserId = userId,
                        GroupId = groupId,
                        StoryId = storyId
                    });
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method to update user_story_group
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="storyId">story id</param>
        /// <param name="groupId">group id</param>
        /// <returns>returns void task</returns>
        private async Task UpdateUserStoryGroup(int userId, int storyId, int groupId)
        {
            if (groupId != null)
            {
                var combination = _context.User_Story_Group.FirstOrDefault(c => c.StoryId == storyId && c.UserId == userId);
                if (combination != null)
                {
                    combination.UserId = userId;
                    combination.StoryId = storyId;
                    combination.GroupId = groupId;
                }

                await _context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Delete story
        /// </summary>
        /// <param name="storyModel">Story model passed in</param>
        public async Task Delete(int storyId)
        {
            var story = _context.User_Story_Group.FirstOrDefault(s => s.StoryId == storyId);

            if (story != null)
            {
                _context.User_Story_Group.Remove(story);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Getting the list of stories fr the user
        /// </summary>
        /// <returns>Returns Stories' list</returns>
        public List<StoryModel> GetStories(int userId)
        {
            return _context.User_Story_Group
               .Where(u => u.UserId == userId)
               .Select(s => new StoryModel
                {
                    StoryId = s.Story.StoryId,
                    Title = s.Story.Title
                })
                 .ToList();
        }

        /// <summary>
        /// Get story
        /// </summary>
        /// <param name="storyId">story id passed in</param>
        /// <returns>returns story model</returns>
        public StoryModel GetStory(int storyId, int userId)
        {
            var usrstrgrp = _context.User_Story_Group.FirstOrDefault(g => g.UserId == userId && g.StoryId == storyId);
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == usrstrgrp.GroupId);
            return _context.Stories.Where(s => s.StoryId == storyId).Select(s => new StoryModel
                {
                    StoryId = s.StoryId,
                    Title = s.Title,
                    Description = s.Description,
                    Content = s.Content,
                    PostedOn = s.PostedOn,
                    GroupId = group.GroupId,
                    GroupName = group.Name
                }).FirstOrDefault();
        }

        #endregion
        #region Group
        /// <summary>
        /// Add or update 
        /// </summary>
        /// <param name="groupModel"></param>
        public async Task AddOrUpdate(GroupModel groupModel)
        {
            var group = _context.Groups.FirstOrDefault(s => s.GroupId == groupModel.GroupId);
            if (group == null)
            {
                group = new Group();
            }

            group.Name = groupModel.Name;
            group.Description = groupModel.Description;

            _context.Groups.AddOrUpdate(group);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Geting the list of groups
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns groups' list</returns>
        public List<GroupModel> GetGroups()
        {
            var query =
               from g in _context.Groups
               join usg in _context.User_Story_Group on g.GroupId equals usg.GroupId into ps
               from usg in ps.DefaultIfEmpty()
               select new { UserStoryGroup = usg, Group = g };

            return query
                .GroupBy(g => g.Group.GroupId)
                .Select(s => new GroupModel
                {
                    GroupId = s.Key,
                    Name = s.Where(u => u.Group != null).Select(u => u.Group.Name).FirstOrDefault(),
                    Description = s.Where(u => u.Group != null).Select(u => u.Group.Description).FirstOrDefault(),
                    StoryList = s.Where(u=>u.UserStoryGroup != null).Select(u => u.UserStoryGroup.StoryId).Distinct().ToList(),
                    UserList = s.Where(u => u.UserStoryGroup != null).Select(u => u.UserStoryGroup.UserId).Distinct().ToList()
                }
                ).ToList();
        }

        #endregion
    }
}
