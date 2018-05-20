using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class TeamController : BaseController
    {
        private readonly ITeamUseCaseFactory _useCaseFactory;
        private readonly ITeamPresenterFactory _presenterFactory;

        public TeamController(IUseCaseFactoryProvider useCaseFactoryProvider, IPresenterFactoryProvider presenterFactoryProvider)
        {
            _useCaseFactory = useCaseFactoryProvider.Team;
            _presenterFactory = presenterFactoryProvider.Team;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var presenter = _presenterFactory.View(MessageHandler, ErrorHandler);
            try
            {
                var uc = _useCaseFactory.View;
                var response = uc.Execute(new ViewTeamRequest(CurrentUserId)
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
        public IActionResult Create(CreateTeamViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = _presenterFactory.Create(MessageHandler, ErrorHandler);
            var request = ConvertToRequest(vm);
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

        private CreateTeamRequest ConvertToRequest(CreateTeamViewModel vm)
        {
            return new CreateTeamRequest(CurrentUserId)
            {
                Name = vm.Name,
                TimeOfDailyScrum = TimeSpan.ParseExact(vm.TimeOfDailyScrum, @"hh\:mm", CultureInfo.InvariantCulture),
                Members = vm.SelectedMemberIds.Select((t, i) => new Team.Member(Identity.FromString(t), Enum.Parse<PersonRole>(vm.SelectedRoles[i])))
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            return View(presenter.GetInitialViewModel(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditTeamViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = _presenterFactory.Edit(MessageHandler, ErrorHandler);
            var request = ConvertToRequest(vm);
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

        private EditTeamRequest ConvertToRequest(EditTeamViewModel vm)
        {
            return new EditTeamRequest(CurrentUserId)
            {
                Id = Identity.FromString(vm.Id),
                Name = vm.Name,
                TimeOfDailyScrum = TimeSpan.ParseExact(vm.TimeOfDailyScrum, @"hh\:mm", CultureInfo.InvariantCulture),
                Members = vm.SelectedMemberIds.Select((t, i) => new Team.Member(Identity.FromString(t), Enum.Parse<PersonRole>(vm.SelectedRoles[i])))
            };
        }

        [HttpGet]
        public IActionResult List()
        {
            var presenter = _presenterFactory.List(MessageHandler, ErrorHandler);
            return View(presenter.Present());
        }
    }
}