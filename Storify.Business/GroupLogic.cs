using NLog;
using Storify.Data;
using Storify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storify.Business
{
    public class GroupLogic
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IStorifyRepository _repo;
        /// <summary>
        /// Logic constructor
        /// </summary>
        /// <param name="repo">Repository interface</param>
        public GroupLogic(IStorifyRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get list of groups
        /// </summary>
        /// <returns>Returns list of groups</returns>
        public List<GroupModel> GetGroups()
        {
            return _repo.GetGroups();
        }
    }
}
