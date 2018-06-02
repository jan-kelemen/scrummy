using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
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
                Id = response.Id.ToPresentationIdentity(),
                Description = response.Description,
                Name = response.Name,
                Time = response.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Duration = response.Duration.ToString(@"hh\:mm", CultureInfo.InvariantCulture),
                Log = response.Log,
                OrganizedBy = organizer.ToViewModel(),
                Project = project.ToViewModel(),
                InvolvedPersons = response.InvolvedPersons.Select(x => RepositoryProvider.Person.Read(x).ToViewModel()),
                CanDelete = response.CanDelete,
                Documents = response.Documents.Select(x => RepositoryProvider.Document.Read(x).ToViewModel()),
            };
        }
    }
}

