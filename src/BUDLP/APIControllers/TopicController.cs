using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BUDLP.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BUDLP.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using BUDLP.Models.TopicModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BUDLP.APIControllers
{
    public class TopicController : Controller
    {
        private UserManager<AuthenticatedUser> _userManager;
        private ApplicationDbContext _appDb;
        private RoleManager<IdentityRole> _roleManager;
        private PlatformDbContext _ctx;

        public TopicController(PlatformDbContext ctx, ApplicationDbContext appDb, UserManager<AuthenticatedUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _appDb = appDb;
            _roleManager = roleManager;
        }

        // Front page topics ordered by target language
        [HttpPost("api/topics/get")]
        public JsonResult GetTopics([FromBody] GetTopicPayload payload)
        {
            List<Topic> topics;

            if (payload.Language == 1)
            {
                topics = _ctx.Topics.OrderBy(x => x.COrder).ToList();
            }
            else if(payload.Language == 2)
            {
                topics = _ctx.Topics.OrderBy(x => x.CPLUSOrder).ToList();
            }
            else
            {
                topics = _ctx.Topics.OrderBy(x => x.MATLABOrder).ToList();
            }

            var result = JsonConvert.SerializeObject(topics);

            return new JsonResult(result);
        }

        [HttpPost("api/topics/topicmodules")]
        public JsonResult LoadTopicModules([FromBody] LoadTopicModuleContentPayload payload)
        {
            var topicModules = _ctx.TopicModuleContent.Where(x => x.TopicModuleId == payload.TopicModuleId).ToList();

            JsonResult result;

            if (topicModules != null)
            {
                result = new JsonResult(JsonConvert.SerializeObject(topicModules));
            }
            else
            {
                result = new JsonResult(HttpStatusCode.BadRequest);
            }

            return result;
        }

        // Load topic module content item
        [HttpPost("api/module/load")]
        public JsonResult LoadModule([FromBody] LoadModulePayload payload)
        {
            var module = _ctx.TopicModules.Where(x => x.TopicModuleId == payload.ContentModuleId).Include(z => z.TopicModuleContent).FirstOrDefault();

            JsonResult result;

            if (module != null)
            {
                result = new JsonResult(JsonConvert.SerializeObject(module.TopicModuleContent.FirstOrDefault()));
            }
            else
            {
                result = new JsonResult("No module found.");
            }

            return result;
        }

        // Load all topics and topic modules
        [HttpGet("api/modules/get")]
        public async Task<JsonResult> GetProfileTopics()
        {
            var user = await GetCurrentUserAsync();
            var topics = _ctx.UserProfileTopics
                .Where(x => x.UserProfileId == user.Id)
                .Include(course => course.Topic)
                .ThenInclude(topic => topic.TopicModules)
                .ToList();

            _ctx.TopicModuleContent.Load();

            var result = JsonConvert.SerializeObject(topics);

            var result_ = new JsonResult("423");
            result_.StatusCode = (int)HttpStatusCode.OK;


            return new JsonResult(result);
        }

        // Payload section
        public class GetTopicPayload
        {
            public int Language { get; set; }
        }

        public class LoadModulePayload
        {
            public int ModuleId { get; set; }
            public int ContentModuleId { get; set; }
        }

        public class LoadTopicModuleContentPayload
        {
            public int TopicModuleId { get; set; }
        }

        // Retrieve user currently logged in
        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}