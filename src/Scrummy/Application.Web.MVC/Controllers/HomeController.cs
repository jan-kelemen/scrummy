using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Models;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About() => View();

        public IActionResult Error() => 
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
