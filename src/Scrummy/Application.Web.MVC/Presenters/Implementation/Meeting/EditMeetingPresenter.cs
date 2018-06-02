using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Meeting
{
    internal class EditMeetingPresenter : Presenter, IEditMeetingPresenter
    {
        public EditMeetingPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditMeetingViewModel GetInitialViewModel(string id)
        {
            var meeting = RepositoryProvider.Meeting.Read(Identity.FromString(id));
            var person = RepositoryProvider.Person.Read(meeting.OrganizedBy);
            var project = RepositoryProvider.Project.Read(meeting.ProjectId);

            return new EditMeetingViewModel
            {
                Id = meeting.Id.ToPresentationIdentity(),
                Name = meeting.Name,
                Description = meeting.Description,
                Time = meeting.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Duration = meeting.Duration.ToString(@"hh\:mm", CultureInfo.InvariantCulture),
                SelectedPersonIds = meeting.InvolvedPersons.Select(x => x.ToPresentationIdentity()).ToList(),
                Log = meeting.Log,
                Project = project.ToViewModel(),
                OrganizedBy = person.ToViewModel(),
                Persons = Persons(),
                SelectedDocumentIds = meeting.Documents.Select(x => x.ToPresentationIdentity()).ToList(),
                Documents = Documents(project.Id),
            };
        }

        public EditMeetingViewModel Present(EditMeetingViewModel vm)
        {
            vm.Persons = Persons();
            vm.Documents = Documents(Identity.FromString(vm.Project.Id));
            return vm;
        }

        private SelectListItem[] Persons() => RepositoryProvider.Person.ListAll().Select(x => x.ToSelectListItem()).ToArray();

        private SelectListItem[] Documents(Identity projectId)
        {
            var common = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Common);
            var meeting = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Meeting);

            return meeting.Concat(common).Select(x => x.ToSelectListItem()).ToArray();
        }
    }
}
