using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public class EditMeetingPresenter : Presenter
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
                Id = meeting.Id.ToString(),
                Name = meeting.Name,
                Description = meeting.Description,
                Time = meeting.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                SelectedPersonIds = meeting.InvolvedPersons.Select(x => x.Id.ToString()).ToList(),
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                OrganizedBy = new NavigationViewModel
                {
                    Id = person.Id.ToString(),
                    Text = person.DisplayName,
                },
                Persons = Persons(),
            };
        }

        public EditMeetingViewModel Present(EditMeetingViewModel vm)
        {
            vm.Persons = Persons();
            return vm;
        }

        private SelectListItem[] Persons()
        {
            return RepositoryProvider.Person.ListAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToArray();
        }
    }
}
