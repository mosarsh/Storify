using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storify.Model
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> UserList { get; set; }
        public List<int?> StoryList { get; set; }

        public int TotalUsers
        {
            get
            {
                return UserList.Count();
            }
        }
        public int TotalStories
        {
            get
            {
                return StoryList.Count();
            }
        }
    }
}
