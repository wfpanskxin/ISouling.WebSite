using Microsoft.AspNetCore.Mvc;

namespace ISouling.WebSite.Www.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Personality()
        {
            return View();
        }
    }
}