using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.MVC.Controllers
{
    public class SprintController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}