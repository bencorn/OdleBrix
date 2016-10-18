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

        public class GetTopicPayload
        {
            public int Language { get; set; }
        }
    }
}