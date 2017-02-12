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

namespace BUDLP.APIControllers
{
    public class UserProfileController : Controller
    {
        private UserManager<AuthenticatedUser> _userManager;
        private ApplicationDbContext _appDb;
        private RoleManager<IdentityRole> _roleManager;
        private PlatformDbContext _ctx;
        private SignInManager<AuthenticatedUser> _signInManager;

        public UserProfileController(PlatformDbContext ctx, ApplicationDbContext appDb, UserManager<AuthenticatedUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AuthenticatedUser> signInManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _appDb = appDb;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("/api/user/login")]
        public async Task<JsonResult> SignIn([FromBody] SignInRequest payload)
        {
            var result = await _signInManager.PasswordSignInAsync(payload.Username, payload.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return new JsonResult("/course");
            }
            if (result.IsLockedOut)
            {

            }
            else
            {

            }
            return new JsonResult("OK.");
        }

        [HttpGet("api/guid")]
        public async Task<ActionResult> GetGuid()
        {
            var user = await GetCurrentUserAsync();

            return new JsonResult(JsonConvert.SerializeObject(user.Id));
        }

        [HttpPost("api/profile/create")]
        public async Task<ActionResult> GetTopics([FromBody] Profile payload)
        {
            var user = new AuthenticatedUser { UserName = payload.Email, Email = payload.Email, FullName = payload.FullName,TargetLanguage = (TopicModuleLanguage)payload.TargetLanguage, IsStaff = false, IsActive = true, IsSuperUser = false };
            var result = await _userManager.CreateAsync(user, payload.Password);

            if (result.Succeeded)
            {
                // Create UserProfileTopics
                foreach (var top in payload.ProfileTopics)
                {
                    string order = top.COrder.ToString() + top.CPLUSOrder.ToString() + top.MATLABOrder.ToString();
                    var upt = new UserProfileTopic()
                    {
                        ToLearn = top.ToLearn,
                        TopicId = top.TopicId,
                        UserProfileId = user.Id,
                        ListOrder = order
                    };

                    if (top.PastExperience.Contains(1) && top.PastExperience.Contains(2))
                    {
                        switch (user.TargetLanguage)
                        {
                            case TopicModuleLanguage.MATLAB:
                                upt.PastExperience = PriorLearned.CCPP2M;
                                break;
                            default:
                                upt.PastExperience = PriorLearned.None;
                                break;
                        }
                    }
                    else if (top.PastExperience.Contains(1) && top.PastExperience.Contains(3))
                    {
                        switch (user.TargetLanguage)
                        {
                            case TopicModuleLanguage.CPP:
                                upt.PastExperience = PriorLearned.MC2CPP;
                                break;
                            default:
                                upt.PastExperience = PriorLearned.None;
                                break;
                        }
                    }
                    else if (top.PastExperience.Contains(2) && top.PastExperience.Contains(3))
                    {
                        switch (user.TargetLanguage)
                        {
                            case TopicModuleLanguage.C:
                                upt.PastExperience = PriorLearned.MCPP2C;
                                break;
                            default:
                                upt.PastExperience = PriorLearned.None;
                                break;
                        }
                    }
                    else if (top.PastExperience.Contains(1))
                    {
                        switch (user.TargetLanguage)
                        {
                            case TopicModuleLanguage.CPP:
                                upt.PastExperience = PriorLearned.C2CPP;
                                break;
                            case TopicModuleLanguage.MATLAB:
                                upt.PastExperience = PriorLearned.C2M;
                                break;
                            default:
                                upt.PastExperience = PriorLearned.None;
                                break;
                        }
                    }
                    else if (top.PastExperience.Contains(2))
                    {
                        switch (user.TargetLanguage)
                        {
                            case TopicModuleLanguage.C:
                                upt.PastExperience = PriorLearned.C2CPP;
                                break;
                            case TopicModuleLanguage.MATLAB:
                                upt.PastExperience = PriorLearned.CPP2M;
                                break;
                            default:
                                upt.PastExperience = PriorLearned.None;
                                break;
                        }
                    }
                    else if (top.PastExperience.Contains(3))
                    {
                        switch (user.TargetLanguage)
                        {
                            case TopicModuleLanguage.C:
                                upt.PastExperience = PriorLearned.M2C;
                                break;
                            case TopicModuleLanguage.CPP:
                                upt.PastExperience = PriorLearned.M2CPP;
                                break;
                            default:
                                upt.PastExperience = PriorLearned.None;
                                break;
                        }
                    }
                    else
                    {
                        upt.PastExperience = PriorLearned.None;
                    }

                    _ctx.UserProfileTopics.Add(upt);
                    _ctx.SaveChanges();

                }

                var userProfileTopics = _ctx.UserProfileTopics
                    .Where(x => x.UserProfileId == user.Id)
                    .ToList();

                foreach(UserProfileTopic u in userProfileTopics)
                {
                    var contentModules = _ctx.TopicModuleContent
                        .Where(x => x.TopicModule.TopicId == u.TopicId && x.PriorLearned == u.PastExperience && x.Language == user.TargetLanguage || x.PriorLearned == PriorLearned.None && x.TopicModule.TopicId == u.TopicId && x.Language == user.TargetLanguage || x.TopicModule.TopicId == u.TopicId && x.Class == ContentModuleClass.Concept)
                        .ToList();

                    foreach(TopicModuleContent t in contentModules)
                    {
                        _ctx.UserLearningStates.Add(new UserLearningState()
                        {
                            AuthenticatedUserId = user.Id,
                            LearningState = (int)LearningState.Untouched,
                            TopicId = u.TopicId,
                            TopicModuleContentId = t.TopicModuleContentId,
                            TopicModuleId = t.TopicModuleId
                        });
                    }
                }

                try
                {
                    _ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return new JsonResult("Account created.");

            
        }

        // Retrieve user currently logged in
        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public class ContentModuleState
        {
            public int State { get; set; }
            public int TopicId { get; set; }
            public int ContentModuleId { get; set; }
        }

        public class Profile
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int TargetLanguage { get; set; }
            public List<Topics> ProfileTopics { get; set; }
        }

        public class Topics : Topic
        {
            public bool ToLearn { get; set; }
            public List<int> PastExperience { get; set; }
        }

        public class SignInRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}