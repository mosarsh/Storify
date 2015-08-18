using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storify.Model
{
    public class StoryModel
    {
        public int StoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTimeOffset PostedOn { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
