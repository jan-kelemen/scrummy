using System;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Controllers.Extensions;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class WorkTaskController : BaseController
    {
        private readonly IWorkTaskUseCaseFactory _useCaseFactory;
        private readonly IWorkTaskPresenterFactory _presenterFactory;

        public WorkTaskController(IUseCaseFactoryProvider useCaseFactoryProvider, IPresenterFactoryProvider presenterFactoryProvider)
        {
            _useCaseFactory = useCaseFactoryProvider.WorkTask;
            _presenterFactory = presenterFactoryProvider.WorkTask;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = _presenterFactory.View(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.View;
                var response = uc.Execute(new ViewWorkTaskRequest(CurrentUserId)
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
        public IActionResult Create(string id, string type, string parent, string child)
        {
            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id, type, parent, child));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateWorkTaskViewModel vm)
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
        public IActionResult Edit(EditWorkTaskViewModel vm)
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
    }
}
