using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Meeting;
using System;
using Scrummy.Application.Web.MVC.Extensions.Requests;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.Meeting;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        private readonly IMeetingUseCaseFactory _useCaseFactory;
        private readonly IMeetingPresenterFactory _presenterFactory;

        public MeetingController(IUseCaseFactoryProvider useCaseFactoryProvider, IPresenterFactoryProvider presenterFactoryProvider)
        {
            _useCaseFactory = useCaseFactoryProvider.Meeting;
            _presenterFactory = presenterFactoryProvider.Meeting;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = _presenterFactory.View(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.View;
                var response = uc.Execute(new ViewMeetingRequest(CurrentUserId)
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
            return View(presenter.GetInitialViewModel(id, CurrentUserId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMeetingViewModel vm)
        {
            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            if (!ModelState.IsValid)
                return View(presenter.Present(vm));

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
        public IActionResult Edit(EditMeetingViewModel vm)
        {
            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            if (!ModelState.IsValid)
                return View(presenter.Present(vm));

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
        public IActionResult List(string id)
        {
            var presenter = _presenterFactory.List(MessageHandler, ErrorHandler);
            return View(presenter.Present(id));
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var presenter = _presenterFactory.Presenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.Delete;
                var response = uc.Execute(new DeleteMeetingRequest(CurrentUserId)
                {
                    Id = Identity.FromString(id),
                });
                presenter.Present(response);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return RedirectToAction(nameof(Index), new {id});
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return RedirectToAction(nameof(Index), new { id });
            }
        }
    }
}
