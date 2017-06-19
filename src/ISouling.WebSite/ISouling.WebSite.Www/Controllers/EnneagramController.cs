using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ISouling.WebSite.Www.Controllers
{
    public class EnneagramController : Controller
    {
        public IActionResult Index(int? id)
        {
            if (id == null)
                return View();

            return View(id.ToString());
        }

        public IActionResult Career()
        {
            return View();
        }
    }
}
