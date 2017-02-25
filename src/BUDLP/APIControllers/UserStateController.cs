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
using BUDLP.Controllers;
using System.Net;

namespace BUDLP.APIControllers
{
    public class UserStateController : Controller
    {
        private UserManager<AuthenticatedUser> _userManager;
        private ApplicationDbContext _appDb;
        private RoleManager<IdentityRole> _roleManager;
        private PlatformDbContext _ctx;
        private SignInManager<AuthenticatedUser> _signInManager;

        public UserStateController(PlatformDbContext ctx, ApplicationDbContext appDb, UserManager<AuthenticatedUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AuthenticatedUser> signInManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _appDb = appDb;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("api/state/create")]
        public async Task<JsonResult> CreateContentModuleState([FromBody] ContentModuleState cms)
        {
            var user = await GetCurrentUserAsync();
            UserLearningState uls = new UserLearningState()
            {
                FirstVisited = DateTime.Now,
                LastVisited = DateTime.Now,
                LearningState = LearningState.Started,
                AuthenticatedUserId = user.Id,
                TopicId = cms.TopicId,
                TopicModuleContentId = cms.ContentModuleId,
                TopicModuleId = cms.TopicModuleId
            };

            _ctx.UserLearningStates.Add(uls);
            _ctx.SaveChanges();

            return new JsonResult(JsonConvert.SerializeObject(uls));
        }

        [HttpPost("api/state/set")]
        public async Task<JsonResult> SetContentModuleState([FromBody] ContentModuleState cms)
        {
            var user = await GetCurrentUserAsync();
            var uls = _ctx.UserLearningStates.Where(x => x.AuthenticatedUserId == user.Id && x.TopicModuleContentId == cms.ContentModuleId).FirstOrDefault();
            var topicModuleContent = _ctx.TopicModuleContent.Where(x => x.TopicModuleContentId == cms.ContentModuleId).FirstOrDefault();

            if (uls != null)
            {
                uls.LastVisited = DateTime.Now;
                uls.LearningState = (LearningState)cms.State;
            }
            else
            {
                _ctx.UserLearningStates.Add(new UserLearningState()
                {
                    AuthenticatedUserId = user.Id,
                    FirstVisited = DateTime.Now,
                    LastVisited = DateTime.Now,
                    LearningState = (LearningState)cms.State,
                    TopicModuleContentId = cms.ContentModuleId,
                    TopicId = topicModuleContent.TopicModule.TopicId,
                    TopicModuleId = topicModuleContent.TopicModuleContentId
                });
            }

            // All content module items were completed, set topic module state to completed
            var res = _ctx.UserLearningStates.Where(x => x.TopicModuleId == topicModuleContent.TopicModuleId && x.AuthenticatedUserId == user.Id).All(x => x.LearningState == LearningState.Completed);
            if (res)
            {
                var topicModuleState = _ctx.TopicModuleStates.Where(x => x.TopicModuleId == topicModuleContent.TopicModuleId && x.AuthenticatedUserId == user.Id).FirstOrDefault();

                if (topicModuleState != null)
                {
                    topicModuleState.LearningState = LearningState.Completed;
                }
            }

            // Persist updates to db
            try
            {
                _ctx.SaveChanges();
            }
            catch { }

            JsonResult result = new JsonResult("State change successful.");
            result.StatusCode = (int)HttpStatusCode.Accepted;

            return result;
        }

        // Retrieve user currently logged in
        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public class ContentModuleState
        {
            public int State { get; set; }
            public int TopicId { get; set; }
            public int ContentModuleId { get; set; }
            public int TopicModuleId { get; set; }
        }
    }
}