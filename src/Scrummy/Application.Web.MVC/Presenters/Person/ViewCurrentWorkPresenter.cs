using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ViewCurrentWorkPresenter : BasePresenter
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IProjectRepository _projectRepository;

        public ViewCurrentWorkPresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler, IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler)
        {
            _projectRepository = repositoryProvider.Project;
            _meetingRepository = repositoryProvider.Meeting;
        }

        public ViewCurrentWorkViewModel Present(ViewCurrentWorkResponse response)
        {
            return new ViewCurrentWorkViewModel
            {
                CurrentProjects = GetCurrentProjects(response.CurrentProjects),
                UpcomingMeetings = GetUpcomingMeetings(response.UpcomingMeetings),
            };
        }

        private IEnumerable<NavigationViewModel> GetCurrentProjects(IEnumerable<Identity> projectIds)
        {
            return projectIds.Select(x =>
            {
                var project = _projectRepository.Read(x);
                return new NavigationViewModel {Id = project.Id.ToString(), Text = project.Name};
            });
        }

        private IEnumerable<NavigationViewModel> GetUpcomingMeetings(IEnumerable<Identity> meetingIds)
        {
            return meetingIds.Select(x =>
            {
                var meeting = _meetingRepository.Read(x);
                return new NavigationViewModel {Id = meeting.Id.ToString(), Text = meeting.Name};
            });
        }
    }
}
