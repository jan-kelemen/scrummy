using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Models;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Home;
using Scrummy.Domain.Core.Utilities;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public HomeController(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public IActionResult Index()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id != null)
                return RedirectToAction("CurrentWork", "Person", new { id });

            return RedirectToAction(nameof(About));
        }

        public IActionResult About() => View();

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var repository = _repositoryProvider.Person;
            var person = repository.FindByEmailAndPasswordHash(vm.Email, SecurityUtility.HashPassword(vm.Password));

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, person.DisplayName),
                    new Claim(ClaimTypes.NameIdentifier, person.Id.ToPresentationIdentity())
                };

                var userIdentity = new ClaimsIdentity(claims, "SecureLogin");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                        AllowRefresh = false
                    });

                return RedirectToAction(nameof(Index));
            }
            MessageHandler(MessageType.Error, "Username or password is invalid.");
            return View();
        }

        public IActionResult Denied() => Unauthorized();

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
