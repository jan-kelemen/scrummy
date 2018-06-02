using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Person
{
    internal class ViewCurrentWorkPresenter : Presenter, IViewCurrentWorkPresenter
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

        private IEnumerable<NavigationViewModel> GetCurrentProjects(IEnumerable<Identity> projectIds) => 
            projectIds.Select(x => RepositoryProvider.Project.Read(x).ToViewModel());

        private IEnumerable<ViewCurrentWorkViewModel.Meeting> GetUpcomingMeetings(IEnumerable<Identity> meetingIds)
        {
            return meetingIds.Select(x =>
            {
                var meeting = RepositoryProvider.Meeting.Read(x);
                return new ViewCurrentWorkViewModel.Meeting
                {
                    Id = meeting.Id.ToPresentationIdentity(),
                    Text = meeting.Name,
                    Time = meeting.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    Duration = meeting.Duration.ToString(@"hh\:mm", CultureInfo.InvariantCulture),
                };
            });
        }
    }
}
