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
            var user = new AuthenticatedUser { UserName = payload.Email, Email = payload.Email, FirstName = payload.FirstName, LastName = payload.LastName ,IsStaff = false, IsActive = true, IsSuperUser = false };
            var result = await _userManager.CreateAsync(user, payload.Password);

            if (result.Succeeded)
            {
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
            public string FirstName { get; set; }
            public string LastName { get; set; }
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