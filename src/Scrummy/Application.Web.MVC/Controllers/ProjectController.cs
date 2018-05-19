using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectUseCaseFactory _projectUseCaseFactory;
        private readonly IRepositoryProvider _repositoryProvider;

        public ProjectController(IUseCaseFactoryProvider useCaseFactoryProvider, IRepositoryProvider repositoryProvider)
        {
            _projectUseCaseFactory = useCaseFactoryProvider.Project;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = new ViewProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _projectUseCaseFactory.View;
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
            var presenter = new CreateProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.GetInitialViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new CreateProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _projectUseCaseFactory.Create;
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
            var presenter = new EditProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProjectViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = new EditProjectPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _projectUseCaseFactory.Edit;
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
            var presenter = new ListProjectsPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.Present());
        }

        [HttpGet]
        public IActionResult Meetings(string id)
        {
            var presenter = new ViewMeetingsPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _projectUseCaseFactory.ViewMeetings;
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
        public IActionResult Backlog(string id)
        {
            var uc = _projectUseCaseFactory.ViewBacklog;
            var response = uc.Execute(new ViewBacklogRequest(CurrentUserId)
            {
                ProjectId = Identity.FromString(id),
            });

            var presenter = new ViewBacklogPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.Present(response));
        }

        [HttpGet]
        public IActionResult ManageBacklog(string id)
        {
            var presenter = new ManageBacklogPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageBacklog(ManageBacklogViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var request = ConvertToRequest(vm);
            var presenter = new ManageBacklogPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _projectUseCaseFactory.ManageBacklog;
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
            var presenter = new ViewSprintsPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.GetInitialViewModel(id, status));
        }
    }
}
