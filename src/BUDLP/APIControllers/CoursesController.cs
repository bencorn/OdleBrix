using BUDLP.Data;
using BUDLP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Azure.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BUDLP.APIControllers
{
    public class CoursesController : Controller
    {
        private UserManager<AuthenticatedUser> _userManager;
        private ApplicationDbContext _appDb;
        private RoleManager<IdentityRole> _roleManager;
        private PlatformDbContext _ctx;
        private DocumentClient _client;
        private static readonly string endpointUrl = "https://odlebrix.documents.azure.com:443/";
        private static readonly string authorizationKey = "zlUgYbN7ICMazassuP6NBvnxtHQHFc50UjvR0480TUizOW9mCJ1m4rMU99UUz0n38oCf4qI51OegKdB15t1jJQ==";
        private static readonly string databaseId = "odle-courses";
        private static readonly string courseCollection = "Courses";
        private static readonly string progressCollection = "Progress";

        public CoursesController(PlatformDbContext ctx, ApplicationDbContext appDb, UserManager<AuthenticatedUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _appDb = appDb;
            _roleManager = roleManager;
            _client = new DocumentClient(new Uri(endpointUrl), authorizationKey);
        }

        [HttpGet("api/v2/courses")]
        public JsonResult GetCourses()
        {
            var courseLink = UriFactory.CreateDocumentCollectionUri(databaseId, courseCollection);        
            var response = _client.CreateDocumentQuery(courseLink).ToList();
            return new JsonResult(response);
        }

        [HttpGet("api/v2/courses/{courseId}")]
        public JsonResult GetCoursesById(int courseId)
        {
            var courseLink = UriFactory.CreateDocumentCollectionUri(databaseId, courseCollection);
            var response = _client.CreateDocumentQuery(courseLink, "SELECT * FROM c where c.courseId = " + courseId).ToList();
            return new JsonResult(response);
        }

        [HttpGet("api/v2/progress")]
        public async Task<JsonResult> GetProgress()
        {
            var user = await GetCurrentUserAsync();
            var courseLink = UriFactory.CreateDocumentCollectionUri(databaseId, progressCollection);
            var response = _client.CreateDocumentQuery(courseLink, "SELECT * FROM p where c.learnerId = " + user.Email).ToList();
            return new JsonResult(response);
        }

        [HttpGet("api/v2/progress/{learnerId}")]
        public JsonResult GetProgressById(string learnerId)
        {
            var courseLink = UriFactory.CreateDocumentCollectionUri(databaseId, progressCollection);
            var response = _client.CreateDocumentQuery(courseLink, "SELECT * FROM p where p.learnerId = " + learnerId).ToList();
            return new JsonResult(response);
        }

        private Task<AuthenticatedUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
