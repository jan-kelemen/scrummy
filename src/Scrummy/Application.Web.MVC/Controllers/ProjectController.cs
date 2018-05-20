using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
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
                var response = uc.Execute(new ViewProjectRequest(CurrentUserId)
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
            var request = ConvertToRequest(vm);
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
                return View(presenter.Present(vm));
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return View(presenter.Present(vm));
            }
        }

        private CreateProjectRequest ConvertToRequest(CreateProjectViewModel vm)
        {
            return new CreateProjectRequest(CurrentUserId)
            {
                Name = vm.Name,
                DefinitionOfDone = vm.DefinitionOfDone,
                TeamId = Identity.FromString(vm.SelectedTeamId),
            };
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
                return View(presenter.Present(vm));
            }
            catch (Exception e)
            {
                presenter.PresentMessage(MessageType.Error, e.Message);
                return View(presenter.Present(vm));
            }
        }

        private EditProjectRequest ConvertToRequest(EditProjectViewModel vm)
        {
            return new EditProjectRequest(CurrentUserId)
            {
                Id = Identity.FromString(vm.Id),
                DefinitionOfDone = vm.DefinitionOfDone,
                Name = vm.Name,
                TeamId = Identity.FromString(vm.SelectedTeamId),
            };
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
            var response = uc.Execute(ConvertToRequest(id, f));

            var presenter = _presenterFactory.ViewBacklog(MessageHandler, ErrorHandler);

            return View(presenter.Present(response, f));
        }

        private ViewBacklogRequest ConvertToRequest(string id, ViewBacklogViewModel.BacklogFlavor flavor)
        {
            var r = new ViewBacklogRequest(CurrentUserId)
            {
                ProjectId = Identity.FromString(id),
            };
            if (flavor == ViewBacklogViewModel.BacklogFlavor.Done)
                r.Include = status => status == ProductBacklog.WorkTaskStatus.Done;
            return r;
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

            var request = ConvertToRequest(vm);
            var presenter = _presenterFactory.ManageBacklog(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.ManageBacklog;
                var response = uc.Execute(request);
                return RedirectToAction(nameof(Backlog), new { id = presenter.Present(response) });
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

        private ManageBacklogRequest ConvertToRequest(ManageBacklogViewModel vm)
        {
            return new ManageBacklogRequest(CurrentUserId)
            {
                ProjectId = Identity.FromString(vm.Project.Id),
                BacklogItems = vm.Ids.Select((x, i) => new ManageBacklogRequest.BacklogItem
                {
                    Id = Identity.FromString(x),
                    Status = Enum.Parse<ProductBacklog.WorkTaskStatus>(vm.Status[i])
                }).ToList(),
            };
        }

        [HttpGet]
        public IActionResult Sprints(string id, string status)
        {
            var presenter = _presenterFactory.ViewSprints(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id, status));
        }
    }
}
