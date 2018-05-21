using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Meeting
{
    internal class ViewMeetingPresenter : Presenter, IViewMeetingPresenter
    {
        public ViewMeetingPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewMeetingViewModel Present(ViewMeetingResponse response)
        {
            var organizer = RepositoryProvider.Person.Read(response.OrganizedBy);
            var project = RepositoryProvider.Project.Read(response.ProjectId);

            return new ViewMeetingViewModel
            {
                Id = response.Id.ToString(),
                Description = response.Description,
                Name = response.Name,
                Time = response.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                OrganizedBy = new NavigationViewModel
                {
                    Id = organizer.Id.ToString(),
                    Text = organizer.DisplayName,
                },
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                InvolvedPersons = response.InvolvedPersons.Select(x =>
                {
                    var person = RepositoryProvider.Person.Read(x);

                    return new NavigationViewModel
                    {
                        Id = person.Id.ToString(),
                        Text = person.DisplayName,
                    };
                }),
                CanDelete = response.CanDelete,
            };
        }
    }
}
