using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Controllers
{

    [Authorize]
    public class SprintController : BaseController
    {
        private readonly ISprintUseCaseFactory _sprintUseCaseFactory;
        private readonly IRepositoryProvider _repositoryProvider;

        public SprintController(IUseCaseFactoryProvider useCaseFactoryProvider, IRepositoryProvider repositoryProvider)
        {
            _sprintUseCaseFactory = useCaseFactoryProvider.Sprint;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = new ViewSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _sprintUseCaseFactory.View;
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
            var presenter = new CreateSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSprintViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new CreateSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _sprintUseCaseFactory.Create;
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

        private CreateSprintRequest ConvertToRequest(CreateSprintViewModel vm)
        {
            return new CreateSprintRequest(CurrentUserId)
            {
                Goal = vm.Goal,
                Name = vm.Name,
                ProjectId = Identity.FromString(vm.Project.Id),
                TimeSpan = new Tuple<DateTime, DateTime>(
                    DateTime.ParseExact(vm.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(vm.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)),
                Stories = vm.SelectedStories.Select(Identity.FromString),
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var presenter = new EditSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSprintViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new EditSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _sprintUseCaseFactory.Edit;
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

        private EditSprintRequest ConvertToRequest(EditSprintViewModel vm)
        {
            return new EditSprintRequest(CurrentUserId)
            {
                Id = Identity.FromString(vm.Id),
                Goal = vm.Goal,
                Name = vm.Name,
                TimeSpan = new Tuple<DateTime, DateTime>(
                    DateTime.ParseExact(vm.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(vm.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)),
                Stories = vm.SelectedStories.Select(Identity.FromString),
            };
        }

        [HttpGet]
        public IActionResult Start(string id)
        {
            var presenter = new StartSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _sprintUseCaseFactory.Start;
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
            var presenter = new EndSprintPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            try
            {
                var uc = _sprintUseCaseFactory.End;
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

    }
}