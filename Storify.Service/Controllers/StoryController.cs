using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Storify.Business;
using Storify.Model;
using NLog;
using System.Threading.Tasks;

namespace Storify.Controllers
{
    public class StoryController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly StoryLogic _logic;
        private int userId = 1; //TODO this needs to take from Identity. When Authentication is built.

        public StoryController(StoryLogic logic)
            : base()
        {
            logger.Info("Created: StoryController");
            _logic = logic;
        }

        /// <summary>
        /// Get all stories
        /// </summary>
        /// <returns>Retruns http response message with the status code</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                logger.Info("Called: Action Get Stories");

                var list = _logic.GetStories(userId);

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
            }
        }

        /// <summary>
        /// Get story detail
        /// </summary>
        /// <param name="storyId">story id passed in</param>
        /// <returns>returns httpresponsemessage with status code and data</returns>
        [HttpGet]
        public HttpResponseMessage Get(int Id) {
            try
            {
                logger.Info("Called: Action Get story details");

                var story = _logic.GetStory(Id, userId);

                return Request.CreateResponse(HttpStatusCode.OK, story);
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
            }
        }

        /// <summary>
        /// Create or update story
        /// </summary>
        /// <param name="model">Story User Grou model to passed in</param>
        /// <returns>Retruns http response message with the status code</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]StoryModel model)
        {
            try
            {
                logger.Info("Called: Action Post Story");

                await _logic.AddStory(model, userId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
            }
        }

        /// <summary>
        /// Update story
        /// </summary>
        /// <param name="model">story model</param>
        /// <returns>retruns httpmessageresponse with statuscode and data</returns>
        public async Task<HttpResponseMessage> Put([FromBody]StoryModel model){
         try
            {
                logger.Info("Called: Action Put Story");

                await _logic.UpdateStory(model, userId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
            }
        }

        /// <summary>
        /// Delete story
        /// </summary>
        /// <param name="storyId">StoryId passed in</param>
        /// <returns>Retruns http response message with the status code</returns>
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                logger.Info("Called: Action Delete Story");
                await _logic.DeleteStory(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error Occured");
            }
        }
    }
}
