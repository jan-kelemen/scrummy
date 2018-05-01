using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        private readonly IPersonUseCaseFactory _personUseCaseFactory;
        private readonly IRepositoryProvider _repositoryProvider;

        public PersonController(IUseCaseFactoryProvider useCaseFactoryProvider, IRepositoryProvider repositoryProvider) : this(useCaseFactoryProvider.Person)
        {
            _repositoryProvider = repositoryProvider;
        }

        private PersonController(IPersonUseCaseFactory personUseCaseFactory)
        {
            _personUseCaseFactory = personUseCaseFactory;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = new ViewPersonPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _personUseCaseFactory.View;
                var response = uc.Execute(new ViewPersonRequest(CurrentUserId)
                {
                    Id = Identity.FromString(id),
                });
                return View(presenter.Present(response));
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterPersonViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterPersonViewModel vm)
        {
            if(vm.Password != vm.ConfirmedPassword)
                ErrorHandler(nameof(RegisterPersonViewModel.ConfirmedPassword), "Passwords don't match.");

            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = new RegisterPersonPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _personUseCaseFactory.Create;
                var response = uc.Execute(request);
                presenter.Present(response);
                return RedirectToAction(nameof(Index), new {id = response.Id.ToString()});
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

        [HttpGet]
        public IActionResult List()
        {
            var presenter = new ListPersonsPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.Present());
        }

        [HttpGet]
        public IActionResult CurrentWork(string id)
        {
            var presenter = new ViewCurrentWorkPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            try
            {
                var uc = _personUseCaseFactory.ViewCurrentWork;
                var response = uc.Execute(new ViewCurrentWorkRequest(CurrentUserId)
                {
                    CurrentTime = DateTime.UtcNow,
                    ForUserId = CurrentUserId,
                });
                return View(presenter.Present(response));
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}