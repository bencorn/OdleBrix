using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BUDLP.Controllers
{
    public class CourseController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return PartialView("CourseWelcome");
        }
    }
}