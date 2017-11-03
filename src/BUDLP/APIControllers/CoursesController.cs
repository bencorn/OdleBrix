using BUDLP.Data;
using BUDLP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Azure.Documents;
using Microsoft.AspNetCore.Mvc;

namespace BUDLP.APIControllers
{
    public class CoursesController : Controller
    {
        private UserManager<AuthenticatedUser> _userManager;
        private ApplicationDbContext _appDb;
        private RoleManager<IdentityRole> _roleManager;
        private PlatformDbContext _ctx;

        public CoursesController(PlatformDbContext ctx, ApplicationDbContext appDb, UserManager<AuthenticatedUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _appDb = appDb;
            _roleManager = roleManager;
        }

        // Front page topics ordered by target language
        [HttpGet("api/v2/courses")]
        public JsonResult GetCourses()
        {
            return new JsonResult("Hello");
        }
    }
}
