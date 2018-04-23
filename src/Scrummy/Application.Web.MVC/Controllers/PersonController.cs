using System;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonUseCaseFactory _personUseCaseFactory;

        public PersonController(IUseCaseFactoryProvider useCaseFactoryProvider) : this(useCaseFactoryProvider.Person)
        {
        }

        private PersonController(IPersonUseCaseFactory personUseCaseFactory)
        {
            _personUseCaseFactory = personUseCaseFactory;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            return NotFound();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterPersonViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterPersonViewModel vm)
        {
            if(vm.Password != vm.ConfirmedPassword)
                ErrorHandler(nameof(RegisterPersonViewModel.ConfirmedPassword), "Passwords don't match.");

            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = new RegisterPersonPresenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _personUseCaseFactory.Create;
                var response = uc.Execute(request);
                presenter.Present(response);
                return RedirectToAction(nameof(Index), new {response.Id});
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return View(vm);
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return View(vm);
            }
        }

        private CreatePersonRequest ConvertToRequest(RegisterPersonViewModel vm)
        {
            return new CreatePersonRequest
            {
                DisplayName = vm.DisplayName,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Password = vm.Password,
            };
        }
    }
}