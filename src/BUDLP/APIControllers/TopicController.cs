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

        [HttpPost("api/topics/get")]
        public JsonResult GetTopics([FromBody] GetTopicPayload payload)
        {
            var topics = _ctx.Topics.Where(x => x.Language == payload.Language).ToList();
            var result = JsonConvert.SerializeObject(topics);

            return new JsonResult(result);
        }

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

        public class GetTopicPayload
        {
            public int Language { get; set; }
        }

        public class LoadModulePayload
        {
            public int ModuleId { get; set; }
            public int ContentModuleId { get; set; }
        }

        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}