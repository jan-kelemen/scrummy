using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ViewMeetingsPresenter : BasePresenter
    {
        public ViewMeetingsPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        { 
        }

        public ViewMeetingsViewModel Present(ViewMeetingsResponse response)
        {
            var project = RepositoryProvider.Project.Read(response.ProjectId);
            return new ViewMeetingsViewModel
            {
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                UpcomingMeetings = response.Meetings
                    .Select(x => RepositoryProvider.Meeting.Read(x))
                    .Select(x => new ViewMeetingsViewModel.Meeting
                    {
                        Id = x.Id.ToString(),
                        Text = x.Name,
                        Time = x.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    })
            };
        }
    }
}
