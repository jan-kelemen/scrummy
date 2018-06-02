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
    internal class CreateMeetingPresenter : Presenter, ICreateMeetingPresenter
    {
        public CreateMeetingPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateMeetingViewModel GetInitialViewModel(string projectId, string personId)
        {
            var person = RepositoryProvider.Person.Read(Identity.FromString(personId));
            var project = RepositoryProvider.Project.Read(Identity.FromString(projectId));
            return new CreateMeetingViewModel
            {
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Duration = DateTime.Now.ToString("HH:mm", CultureInfo.InvariantCulture),
                Project = project.ToViewModel(),
                OrganizedBy = person.ToViewModel(),
                Persons = Persons(),
                Documents = Documents(project.Id),
            };
        }

        public CreateMeetingViewModel Present(CreateMeetingViewModel vm)
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
