using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected virtual void ErrorHandler(string key, string message)
        {
            ModelState.AddModelError(key, message);
        }

        protected virtual void MessageHandler(MessageType type, string message)
        {
            TempData["Status"] = type;
            TempData["Message"] = message;
        }
    }
}
