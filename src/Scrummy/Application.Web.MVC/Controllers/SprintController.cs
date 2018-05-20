using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Controllers.Extensions;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Controllers
{

    [Authorize]
    public class SprintController : BaseController
    {
        private readonly ISprintUseCaseFactory _useCaseFactory;
        private readonly ISprintPresenterFactory _presenterFactory;

        public SprintController(IUseCaseFactoryProvider useCaseFactoryProvider, IPresenterFactoryProvider presenterFactoryProvider)
        {
            _useCaseFactory = useCaseFactoryProvider.Sprint;
            _presenterFactory = presenterFactoryProvider.Sprint;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = _presenterFactory.View(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.View;
                var response = uc.Execute(new ViewSprintRequest(CurrentUserId)
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
        public IActionResult Create(string id)
        {
            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSprintViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            var request = vm.ToRequest(CurrentUserId);
            try
            {
                var uc = _useCaseFactory.Create;
                var response = uc.Execute(request);
                return RedirectToAction(nameof(Index), new { id = presenter.Present(response) });
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return View(presenter.Present(vm));
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return View(presenter.Present(vm));
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSprintViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            var request = vm.ToRequest(CurrentUserId);
            try
            {
                var uc = _useCaseFactory.Edit;
                var response = uc.Execute(request);
                return RedirectToAction(nameof(Index), new { id = presenter.Present(response) });
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return View(presenter.Present(vm));
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return View(presenter.Present(vm));
            }
        }

        [HttpGet]
        public IActionResult Start(string id)
        {
            var presenter = _presenterFactory.Presenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.Start;
                var response = uc.Execute(new StartSprintRequest(CurrentUserId)
                {
                    Id = Identity.FromString(id),
                });
                return RedirectToAction(nameof(Index), "Project", new {id = presenter.Present(response)});
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
        public IActionResult End(string id)
        {
            var presenter = _presenterFactory.Presenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.End;
                var response = uc.Execute(new EndSprintRequest(CurrentUserId)
                {
                    Id = Identity.FromString(id),
                });
                return RedirectToAction(nameof(Index), "Project", new { id = presenter.Present(response) });
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
        public IActionResult ChangeTaskStatus(string id, string taskId, string status)
        {
            var presenter = _presenterFactory.Presenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.ChangeTaskStatus;
                var response = uc.Execute(id.ToRequest(taskId, status, CurrentUserId));
                return RedirectToAction(nameof(Index), "Project", new { id = presenter.Present(response) });
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