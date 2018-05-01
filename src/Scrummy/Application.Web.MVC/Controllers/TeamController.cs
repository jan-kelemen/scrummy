using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITeamUseCaseFactory _teamUseCaseFactory;
        private readonly IRepositoryProvider _repositoryProvider;

        public TeamController(IUseCaseFactoryProvider useCaseFactoryProvider, IRepositoryProvider repositoryProvider)
        {
            _teamUseCaseFactory = useCaseFactoryProvider.Team;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var presenter = new CreateTeamPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            return View(presenter.GetInitialViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateTeamViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var presenter = new CreateTeamPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _teamUseCaseFactory.Create;
                var response = uc.Execute(request);;
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
        public IActionResult List()
        {
            return NotFound();
            //var presenter = new ListTeamsPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            //return View(presenter.Present());
        }
    }
}