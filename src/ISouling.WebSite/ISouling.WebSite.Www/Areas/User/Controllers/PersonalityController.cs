using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISouling.WebSite.Www.Areas.User.Controllers
{
    public class PersonalityController : Controller
    {
        public IActionResult Recognise(int? id)
        {
            ViewBag.Step = id;
            return View("Step" + id);
        }
    }
}