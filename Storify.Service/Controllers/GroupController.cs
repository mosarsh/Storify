using NLog;
using Storify.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storify.Service.Controllers
{
    public class GroupController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly GroupLogic _logic;

        /// <summary>
        /// contructor to initialize logic
        /// </summary>
        /// <param name="logic"></param>
        public GroupController(GroupLogic logic)
            : base()
        {
            logger.Info("Created: GroupController");
            _logic = logic;
        }

        /// <summary>
        /// Get response message with the group list
        /// </summary>
        /// <returns>Status code OK and list og groups</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var list = _logic.GetGroups();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occured.");
            }
        }


    }
}
