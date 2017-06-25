using Microsoft.AspNetCore.Mvc;

namespace ISouling.WebSite.Www.Areas.User.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Personality()
        {
            return View();
        }

        public IActionResult Career()
        {
            return View();
        }

        public IActionResult Growth()
        {
            return View();
        }
    }
}