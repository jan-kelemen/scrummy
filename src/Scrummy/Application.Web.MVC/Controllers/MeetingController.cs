using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Meeting;
using System;
using System.Globalization;
using System.Linq;

namespace Scrummy.Application.Web.MVC.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        private IMeetingUseCaseFactory _meetingUseCaseFactory;
        private IRepositoryProvider _repositoryProvider;

        public MeetingController(IUseCaseFactoryProvider useCaseFactoryProvider, IRepositoryProvider repositoryProvider)
        {
            _meetingUseCaseFactory = useCaseFactoryProvider.Meeting;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var presenter = new CreateMeetingPresenter(MessageHandler, ErrorHandler, _repositoryProvider);

            return View(presenter.GetInitialViewModel(id, CurrentUserId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMeetingViewModel vm)
        {
            var presenter = new CreateMeetingPresenter(MessageHandler, ErrorHandler, _repositoryProvider);
            if (!ModelState.IsValid)
                return View(presenter.Present(vm));
            
            var request = ConvertToRequest(vm);
            try
            {
                var uc = _meetingUseCaseFactory.Create;
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

        private CreateMeetingRequest ConvertToRequest(CreateMeetingViewModel vm)
        {
            return new CreateMeetingRequest(CurrentUserId)
            {
                Name = vm.Name,
                Description = vm.Description,
                ProjectId = Identity.FromString(vm.Project.Id),
                OrganizedBy = Identity.FromString(vm.OrganizedBy.Id),
                InvolvedPersons = vm.SelectedPersonIds.Select(x => Identity.FromString(x)),
                Time = DateTime.ParseExact(vm.Time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List(string projectId)
        {
            return View();
        }
    }
}
