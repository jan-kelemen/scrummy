using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        private readonly IPersonUseCaseFactory _useCaseFactory;
        private readonly IPersonPresenterFactory _presenterFactory;

        public PersonController(IUseCaseFactoryProvider useCaseFactoryProvider, IPresenterFactoryProvider presenterFactoryProvider)
        {
            _useCaseFactory = useCaseFactoryProvider.Person;
            _presenterFactory = presenterFactoryProvider.Person;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = _presenterFactory.View(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.View;
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
            if (vm.Password != vm.ConfirmedPassword)
                ErrorHandler(nameof(RegisterPersonViewModel.ConfirmedPassword), "Passwords don't match.");

            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = _presenterFactory.Presenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.Create;
                var response = uc.Execute(request);
                presenter.Present(response);
                return RedirectToAction(nameof(Index), new { id = response.Id.ToString() });
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
        public IActionResult Edit(string id)
        {
            if (id != CurrentUserId)
                return Unauthorized();

            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditPersonViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.Edit;
                var response = uc.Execute(request);
                return RedirectToAction(nameof(Index), new { id = presenter.Present(response) });
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

        private EditPersonRequest ConvertToRequest(EditPersonViewModel vm)
        {
            return new EditPersonRequest(CurrentUserId)
            {
                ForUserId = Identity.FromString(vm.Id),
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DisplayName = vm.DisplayName,
                Email = vm.Email,
            };
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            if (id != CurrentUserId)
                return Unauthorized();

            var presenter = _presenterFactory.ChangePassword(MessageHandler, ErrorHandler);

            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            if (vm.NewPassword != vm.ConfirmedPassword)
                ErrorHandler(nameof(RegisterPersonViewModel.ConfirmedPassword), "Passwords don't match.");

            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = _presenterFactory.ChangePassword(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.ChangePassword;
                var response = uc.Execute(request);
                return RedirectToAction(nameof(Index), new { id = presenter.Present(response) });
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

        private ChangePasswordRequest ConvertToRequest(ChangePasswordViewModel vm)
        {
            return new ChangePasswordRequest(CurrentUserId)
            {
                ForUserId = Identity.FromString(vm.Id),
                OldPassword = vm.OldPassword,
                NewPassword = vm.NewPassword,
            };
        }

        [HttpGet]
        public IActionResult List()
        {
            var presenter = _presenterFactory.List(MessageHandler, ErrorHandler);
            return View(presenter.Present());
        }

        [HttpGet]
        public IActionResult CurrentWork(string id)
        {
            var presenter = _presenterFactory.ViewCurrentWork(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.ViewCurrentWork;
                var response = uc.Execute(new ViewCurrentWorkRequest(CurrentUserId)
                {
                    CurrentTime = DateTime.Now,
                    ForUserId = Identity.FromString(CurrentUserId),
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