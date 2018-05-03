﻿using System;
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
    public class CreateMeetingPresenter : BasePresenter
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

        public CreateMeetingViewModel Present(CreateMeetingViewModel vm)
        {
            vm.Persons = Persons();
            return vm;
        }

        public string Present(CreateMeetingResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.Id.ToString();
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
