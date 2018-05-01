using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ViewCurrentWorkPresenter : BasePresenter
    {
        public ViewCurrentWorkPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
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
                var project = RepositoryProvider.Project.Read(x);
                return new NavigationViewModel {Id = project.Id.ToString(), Text = project.Name};
            });
        }

        private IEnumerable<ViewCurrentWorkViewModel.Meeting> GetUpcomingMeetings(IEnumerable<Identity> meetingIds)
        {
            return meetingIds.Select(x =>
            {
                var meeting = RepositoryProvider.Meeting.Read(x);
                return new ViewCurrentWorkViewModel.Meeting
                {
                    Id = meeting.Id.ToString(),
                    Text = meeting.Name,
                    Time = meeting.Time.ToLongDateString(),
                };
            });
        }
    }
}
