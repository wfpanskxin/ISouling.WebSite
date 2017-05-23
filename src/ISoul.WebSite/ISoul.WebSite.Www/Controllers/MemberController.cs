using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISoul.WebSite.Www.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Personality()
        {
            return View();
        }
    }
}