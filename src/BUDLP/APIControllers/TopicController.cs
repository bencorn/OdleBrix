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

            // Load topics matching user target language, ordered by order column
            if (payload.Language == 1)
            {
                topics = _ctx.Topics.OrderBy(x => x.COrder).ToList();
            }
            else if (payload.Language == 2)
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
        public async Task<JsonResult> LoadTopicModules([FromBody] LoadTopicModuleContentPayload payload)
        {
            // Load topic modules matching user target language
            var user = await GetCurrentUserAsync();

            List<TopicModuleContent> topicModules;

            var topic = _ctx.TopicModules.Where(x => x.TopicModuleId == payload.TopicModuleId).FirstOrDefault();

            var profTopic = _ctx.UserProfileTopics
                    .Where(x => x.TopicId == topic.TopicId && x.UserProfileId == user.Id)
                    .FirstOrDefault();

            switch (profTopic.PastExperience)
            {
                case PriorLearned.None:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && x.PriorLearned == PriorLearned.None && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.C2M:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.C2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.C2CPP:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.C2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.M2C:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.M2C || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.M2CPP:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.M2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.CPP2M:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.CPP2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.CPP2C:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.CPP2C || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.MC2CPP:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.MCPP2C:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                case PriorLearned.CCPP2M:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.CCPP2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
                default:
                    topicModules = _ctx.TopicModuleContent
                        .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && x.PriorLearned == PriorLearned.None && x.TopicModuleId == payload.TopicModuleId)
                        .ToList();
                    break;
            }

            _ctx.UserLearningStates
                .Where(x => x.AuthenticatedUserId == user.Id)
                .Load();

            topicModules = topicModules.OrderBy(x => x.Class).ThenBy(x => x.ContentGroup).ThenBy(x => x.RelativeOrdering).ToList();

            JsonResult result;

            // Serialize object (to JSON)
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

        [HttpPost("api/topics/contentmodule")]
        public async Task<JsonResult> LoadContentModule([FromBody] LoadContentModulePayload payload)
        {
            // Load specific content module
            var user = await GetCurrentUserAsync();
            var contentModule = _ctx.TopicModuleContent
                .Where(x => x.TopicModuleContentId == payload.ContentModuleId)
                .FirstOrDefault();

            // Load linked learning state
            _ctx.UserLearningStates
                .Where(x => x.AuthenticatedUserId == user.Id)
                .Load();

            JsonResult result;

            // Serialize object (to JSON)
            if (contentModule != null)
            {
                result = new JsonResult(JsonConvert.SerializeObject(contentModule));
            }
            else
            {
                result = new JsonResult(HttpStatusCode.BadRequest);
            }

            return result;
        }

        // Load topic module content item
        [HttpPost("api/module/load")]
        public async Task<JsonResult> LoadModule([FromBody] LoadModulePayload payload)
        {
            var user = await GetCurrentUserAsync();

            // Topic modules matching user target language 
            var module = _ctx.TopicModules
                .Where(x => x.TopicModuleId == payload.ContentModuleId)
                .FirstOrDefault();

            // Load ProfileTopicModule
            var profTopic = _ctx.UserProfileTopics
                .Where(x => x.TopicId == module.TopicId && x.UserProfileId == user.Id)
                .FirstOrDefault();

            // If toLearn on profTopic is false, do not include any concept
            if (!profTopic.ToLearn)
            {
                switch (profTopic.PastExperience)
                {
                    case PriorLearned.None:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.None)
                            .Load();
                        break;
                    case PriorLearned.C2M:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.C2M)
                            .Load();
                        break;
                    case PriorLearned.C2CPP:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.C2CPP)
                            .Load();
                        break;
                    case PriorLearned.M2C:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.M2C)
                            .Load();
                        break;
                    case PriorLearned.M2CPP:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.M2CPP)
                            .Load();
                        break;
                    case PriorLearned.CPP2M:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.CPP2M)
                            .Load();
                        break;
                    case PriorLearned.CPP2C:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.CPP2C)
                            .Load();
                        break;
                    case PriorLearned.MC2CPP:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.MC2CPP)
                            .Load();
                        break;
                    case PriorLearned.MCPP2C:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.MC2CPP)
                            .Load();
                        break;
                    case PriorLearned.CCPP2M:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.CCPP2M)
                            .Load();
                        break;
                }
            }
            else
            {
                switch (profTopic.PastExperience)
                {
                    case PriorLearned.None:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.None)
                            .Load();
                        break;
                    case PriorLearned.C2M:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.C2M || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.C2CPP:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.C2CPP || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.M2C:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.M2C || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.M2CPP:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.M2CPP || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.CPP2M:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.CPP2M || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.CPP2C:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.CPP2C || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.MC2CPP:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.MCPP2C:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                    case PriorLearned.CCPP2M:
                        _ctx.TopicModuleContent
                            .Where(x => x.TopicModuleId == module.TopicModuleId && x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.CCPP2M || x.PriorLearned == PriorLearned.None))
                            .Load();
                        break;
                }
            }

            // Learning states matching authenticated user id
            _ctx.UserLearningStates
                .Where(x => x.AuthenticatedUserId == user.Id)
                .Load();

            JsonResult result;

            // Serialize object (to JSON)
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

            int sortIndex = 0;
            switch (user.TargetLanguage)
            {
                case TopicModuleLanguage.C:
                    sortIndex = 0;
                    break;
                case TopicModuleLanguage.CPP:
                    sortIndex = 1;
                    break;
                case TopicModuleLanguage.MATLAB:
                    sortIndex = 2;
                    break;
            }

            var topics = _ctx.UserProfileTopics
                .Where(x => x.UserProfileId == user.Id)
                .Include(course => course.Topic)
                .OrderBy(x => (int)x.ListOrder[sortIndex])
                .ToList();


            ////////////////////////////////////////////////

            // Load language specific topic modules
            _ctx.TopicModules
                .Where(x => x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept)
                .Load();

            //// Load linked content modules to topic modules
            //_ctx.TopicModuleContent.Load();

            foreach (var t in topics)
            {
                //t.ProgressCount = $"{_ctx.UserLearningStates.Where(x => x.TopicModuleId == t.Topic.TopicModules[0].TopicModuleId && x.LearningState == LearningState.Completed && x.AuthenticatedUserId == user.Id).Count()} / {_ctx.UserLearningStates.Where(x => x.TopicModuleId == t.Topic.TopicModules[0].TopicModuleId && x.AuthenticatedUserId == user.Id).Count()}";
                t.ProgressCount = await CalculateUserProgress(t);
            }

            // Load linked learning states to content modules
            _ctx.UserLearningStates
                .Where(x => x.AuthenticatedUserId == user.Id)
                .Load();

            // Serialize object (to JSON)
            var result = JsonConvert.SerializeObject(topics);
            return new JsonResult(result);
        }

        // Load topic module content item
        [HttpPost("api/module/enable")]
        public async Task<JsonResult> EnableTopicModule([FromBody] EnableTopicPayload payload)
        {
            var user = await GetCurrentUserAsync();

            var userProfileTopic = _ctx.UserProfileTopics.Where(x => x.UserProfileId == user.Id && x.TopicId == payload.TopicId).FirstOrDefault();

            if (userProfileTopic != null)
            {
                userProfileTopic.ToLearnOverride = true;
                _ctx.SaveChanges();
            }

            return new JsonResult("Topic enabled.");
        }

        public async Task<string> CalculateUserProgress(UserProfileTopic t)
        {
            var user = await GetCurrentUserAsync();
            var topicModule = _ctx.TopicModules.Where(x => x.TopicId == t.TopicId).First();
            int completedModules = 0;
            int totalModules = 0;

            if (t.ToLearn)
            {
                switch (t.PastExperience)
                {
                    case PriorLearned.None:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && x.PriorLearned == PriorLearned.None && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.C2M:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.C2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.C2CPP:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.C2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.M2C:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.M2C || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.M2CPP:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.M2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.CPP2M:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.CPP2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.CPP2C:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.CPP2C || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.MC2CPP:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.MCPP2C:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.CCPP2M:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && (x.PriorLearned == PriorLearned.CCPP2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    default:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => (x.Language == user.TargetLanguage || x.Language == TopicModuleLanguage.Concept) && x.PriorLearned == PriorLearned.None && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                }
            }
            else
            {
                switch (t.PastExperience)
                {
                    case PriorLearned.None:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.None && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.C2M:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.C2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.C2CPP:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.C2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.M2C:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.M2C || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.M2CPP:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.M2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.CPP2M:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.CPP2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.CPP2C:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.CPP2C || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.MC2CPP:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.MCPP2C:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.MC2CPP || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    case PriorLearned.CCPP2M:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && (x.PriorLearned == PriorLearned.CCPP2M || x.PriorLearned == PriorLearned.None) && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                    default:
                        totalModules += _ctx.TopicModuleContent
                            .Where(x => x.Language == user.TargetLanguage && x.PriorLearned == PriorLearned.None && x.TopicModuleId == topicModule.TopicModuleId)
                            .Count();
                        break;
                }
            }

            completedModules = _ctx.UserLearningStates
                .Where(x => x.AuthenticatedUserId == user.Id && x.TopicModuleId == topicModule.TopicModuleId && x.LearningState == LearningState.Completed)
                .Count();

            return $"{completedModules} / {totalModules}";
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

        public class EnableTopicPayload
        {
            public int TopicId { get; set; }
        }

        public class LoadTopicModuleContentPayload
        {
            public int TopicModuleId { get; set; }
        }

        public class LoadContentModulePayload
        {
            public int ContentModuleId { get; set; }
        }

        // Retrieve user currently logged in
        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}