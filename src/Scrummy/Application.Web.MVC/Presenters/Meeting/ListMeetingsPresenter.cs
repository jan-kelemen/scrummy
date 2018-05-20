using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public class ListMeetingsPresenter : Presenter
    {
        public ListMeetingsPresenter(
            Action<MessageType, 
            string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ListMeetingsViewModel Present(string id)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(id));
            return new ListMeetingsViewModel
            {
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                Meetings = RepositoryProvider.Meeting
                    .GetMeetingsOfProjectInTimeRange(project.Id, DateTime.MinValue, DateTime.MaxValue)
                    .Select(x =>
                    {
                        var meeting = RepositoryProvider.Meeting.Read(x);
                        return new ListMeetingsViewModel.Meeting
                        {
                            Id = meeting.Id.ToString(),
                            Text = meeting.Name,
                            Time = meeting.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        };
                    })
            };
        }
    }
}
