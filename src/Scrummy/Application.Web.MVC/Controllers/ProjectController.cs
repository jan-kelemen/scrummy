using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Extensions.Requests;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectUseCaseFactory _useCaseFactory;
        private readonly IProjectPresenterFactory _presenterFactory;

        public ProjectController(IUseCaseFactoryProvider useCaseFactoryProvider, IPresenterFactoryProvider presenterFactoryProvider)
        {
            _useCaseFactory = useCaseFactoryProvider.Project;
            _presenterFactory = presenterFactoryProvider.Project;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = _presenterFactory.View(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.View;
                var response = uc.Execute(new AuthorizedIdRequest(CurrentUserId)
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
        public IActionResult Create()
        {
            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            var request = vm.ToRequest(CurrentUserId);
            try
            {
                var uc = _useCaseFactory.Create;
                var response = uc.Execute(request);
                presenter.Present(response);
                return RedirectToAction(nameof(Index), new { id = response.Id.ToPresentationIdentity() });
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
        public IActionResult Edit(EditProjectViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var request = vm.ToRequest(CurrentUserId);
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
                return View(presenter.Present(vm));
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return View(presenter.Present(vm));
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            var presenter = _presenterFactory.List(MessageHandler, ErrorHandler);
            return View(presenter.Present());
        }

        [HttpGet]
        public IActionResult Meetings(string id)
        {
            var presenter = _presenterFactory.ViewMeetings(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.ViewMeetings;
                var response = uc.Execute(new ViewMeetingsRequest(CurrentUserId)
                {
                    ProjectId = Identity.FromString(id),
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
        public IActionResult Backlog(string id, string flavor)
        {
            var uc = _useCaseFactory.ViewBacklog;
            var f = Enum.Parse<ViewBacklogViewModel.BacklogFlavor>(flavor);
            var response = uc.Execute(id.ToRequest(f, CurrentUserId));

            var presenter = _presenterFactory.ViewBacklog(MessageHandler, ErrorHandler);

            return View(presenter.Present(response, f));
        }

        [HttpGet]
        public IActionResult ManageBacklog(string id)
        {
            var presenter = _presenterFactory.ManageBacklog(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageBacklog(ManageBacklogViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var request = vm.ToRequest(CurrentUserId);
            var presenter = _presenterFactory.ManageBacklog(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.ManageBacklog;
                var response = uc.Execute(request);
                return RedirectToAction(nameof(Backlog), new { id = presenter.Present(response), flavor = "Backlog"});
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

        [HttpGet]
        public IActionResult Sprints(string id, string status)
        {
            var presenter = _presenterFactory.ViewSprints(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id, status));
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var presenter = _presenterFactory.Presenter(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.Delete;
                var response = uc.Execute(new AuthorizedIdRequest(CurrentUserId)
                {
                    Id = Identity.FromString(id),
                });
                presenter.Present(response);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (InvalidRequestException ire)
            {
                presenter.PresentErrors(ire.Message, ire.Errors);
                return RedirectToAction(nameof(Index), new { id });
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return RedirectToAction(nameof(Index), new { id });
            }
        }

        [HttpGet]
        public IActionResult Teams(string id)
        {
            var presenter = _presenterFactory.TeamHistory(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.TeamHistory;
                var response = uc.Execute(new AuthorizedIdRequest(CurrentUserId)
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
        public IActionResult Documents(string id, string flavor)
        {
            var presenter = _presenterFactory.ViewDocuments(MessageHandler, ErrorHandler);
            return View(presenter.Present(id, flavor));
        }

        [HttpGet]
        public IActionResult Report(string id)
        {
            var presenter = _presenterFactory.Report(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.Report;
                var response = uc.Execute(new AuthorizedIdRequest(CurrentUserId)
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
    }
}
