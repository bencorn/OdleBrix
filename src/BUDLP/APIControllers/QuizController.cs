using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BUDLP.Data;
using BUDLP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using BUDLP.Models.TopicQuizzes;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BUDLP.APIControllers
{
    public class QuizController : Controller
    {
        private UserManager<AuthenticatedUser> _userManager;
        private ApplicationDbContext _appDb;
        private RoleManager<IdentityRole> _roleManager;
        private PlatformDbContext _ctx;

        public QuizController(PlatformDbContext ctx, ApplicationDbContext appDb, UserManager<AuthenticatedUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _appDb = appDb;
            _roleManager = roleManager;
        }

        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost("api/quiz/submit")]
        public async Task<JsonResult> SubmitQuizResponse([FromBody] QuizSubmitPayload q)
        {
            JsonResult result;
            var user = await GetCurrentUserAsync();
            var userProfileId = user?.Id;

            var quizResponse = new UserQuizResponse()
            {
                Response = q.Response,
                QuizId = q.QuizId,
                Correct = q.Correct,
                UserProfileId = userProfileId,
                DateSubmitted = DateTime.Now
            };

            _ctx.UserQuizResponses.Add(quizResponse);
            _ctx.SaveChanges();

            //TODO: Try catch, better result other than status code.
            return result = new JsonResult(new JsonResult(HttpStatusCode.Accepted));  
        }

        [HttpPost("api/quiz/get")]
        public JsonResult GetQuizById([FromBody] GetQuizPayload q)
        {
            // get all quiz objects associated with module
            var quizzes = _ctx.Quizzes.Where(x => x.TopicModuleContentId == q.TopicModuleContentId).Include(x => x.QuizOptions).ToList();

            JsonResult result;

            if (quizzes != null)
            {
                result = new JsonResult(JsonConvert.SerializeObject(quizzes));
            }
            else
            {
                result = new JsonResult("");
            }

            return result;
        }

        public class GetQuizPayload
        {
            public int TopicModuleContentId { get; set; }
        }

        public class QuizSubmitPayload
        {
            public int QuizId { get; set; }
            public string Response { get; set; }
            public bool Correct { get; set; }
        }


    }


}
