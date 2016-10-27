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

        [HttpPost("api/profile/create")]
        public async Task<ActionResult> GetTopics([FromBody] Profile payload)
        {
            var user = new AuthenticatedUser { UserName = payload.Email, Email = payload.Email, FullName = payload.FullName,TargetLanguage = payload.TargetLanguage, IsStaff = false, IsActive = true, IsSuperUser = false };
            var result = await _userManager.CreateAsync(user, payload.Password);

            if (result.Succeeded)
            {

                foreach (var top in payload.ProfileTopics)
                {
                    _ctx.UserProfileTopics.Add(new UserProfileTopic()
                    {
                        ToLearn = top.ToLearn,
                        TopicId = top.TopicId,
                        UserProfileId = user.Id,
                        PastExperience = JsonConvert.SerializeObject(top.PastExperience)
                    });
                }

                _ctx.SaveChanges();

                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return new JsonResult("Account created.");

            
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
    }
}