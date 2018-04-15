using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Models;
using Scrummy.Domain.UseCases.Interfaces;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUseCaseFactoryProvider _useCaseFactoryProvider;

        public HomeController(IUseCaseFactoryProvider useCaseFactoryProvider)
        {
            _useCaseFactoryProvider = useCaseFactoryProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About() => View();

        public IActionResult Error() => 
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
