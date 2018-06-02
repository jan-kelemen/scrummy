using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Meeting
{
    internal class ListMeetingsPresenter : Presenter, IListMeetingsPresenter
    {
        public ListMeetingsPresenter(
            Action<MessageType, string> messageHandler, 
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
                Project = project.ToViewModel(),
                Meetings = RepositoryProvider.Meeting
                    .GetMeetingsOfProjectInTimeRange(project.Id, DateTime.MinValue, DateTime.MaxValue)
                    .Select(x =>
                    {
                        var meeting = RepositoryProvider.Meeting.Read(x);
                        return new ListMeetingsViewModel.Meeting
                        {
                            Id = meeting.Id.ToPresentationIdentity(),
                            Text = meeting.Name,
                            Time = meeting.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Duration = meeting.Duration.ToString(@"hh\:mm", CultureInfo.InvariantCulture),
                        };
                    })
            };
        }
    }
}
