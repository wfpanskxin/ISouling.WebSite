using ISouling.Component.Web;
using Microsoft.AspNetCore.Mvc;

namespace ISouling.WebSite.Www.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Captcha() => new CaptchaResult();
    }
}