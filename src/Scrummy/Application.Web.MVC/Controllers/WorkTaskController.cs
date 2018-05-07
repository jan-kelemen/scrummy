using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class WorkTaskController : BaseController
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IWorkTaskUseCaseFactory _workTaskUseCaseFactory;

        public WorkTaskController(IRepositoryProvider repositoryProvider, IUseCaseFactoryProvider useCaseFactoryProvider)
        {
            _repositoryProvider = repositoryProvider;
            _workTaskUseCaseFactory = useCaseFactoryProvider.WorkTask;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = new ViewWorkTaskPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _workTaskUseCaseFactory.View;
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
        public IActionResult Create(string id, string type)
        {
            var presenter = new CreateWorkTaskPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.GetInitialViewModel(id, type));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateWorkTaskViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new CreateWorkTaskPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _workTaskUseCaseFactory.Create;
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

        private CreateWorkTaskRequest ConvertToRequest(CreateWorkTaskViewModel vm)
        {
            return new CreateWorkTaskRequest(CurrentUserId)
            {
                Name = vm.Name,
                ProjectId = Identity.FromString(vm.Project.Id),
                ParentTask = string.IsNullOrWhiteSpace(vm.ParentTaskId) ? Identity.BlankIdentity : Identity.FromString(vm.ParentTaskId),
                Type = Enum.Parse<WorkTaskType>(vm.Type),
                Description = vm.Description,
                ChildTasks = vm.ChildTaskIds.Select(Identity.FromString),
                StoryPoints = vm.StoryPoints,
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var presenter = new EditWorkTaskPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditWorkTaskViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new EditWorkTaskPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _workTaskUseCaseFactory.Edit;
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

        private EditWorkTaskRequest ConvertToRequest(EditWorkTaskViewModel vm)
        {
            return new EditWorkTaskRequest(CurrentUserId)
            {
                Name = vm.Name,
                ParentTask = Identity.FromString(vm.ParentTaskId),
                Description = vm.Description,
                ChildTasks = vm.ChildTaskIds.Select(Identity.FromString),
                StoryPoints = vm.StoryPoints,
                Id = Identity.FromString(vm.Id),
            };
        }
    }
}
