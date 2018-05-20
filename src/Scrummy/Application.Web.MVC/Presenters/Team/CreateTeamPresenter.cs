using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public class CreateTeamPresenter : BasePresenter
    {
        public CreateTeamPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateTeamViewModel GetInitialViewModel()
        {
            return new CreateTeamViewModel
            {
                TimeOfDailyScrum = DateTime.Now.TimeOfDay.ToString(@"hh\:mm"),
                Persons = Persons(),
                Roles = Roles(),
            };
        }

        public CreateTeamViewModel Present(CreateTeamViewModel vm)
        {
            vm.Persons = Persons();
            vm.Roles = Roles();

            return vm;
        }

        private SelectListItem[] Roles()
        {
            return new[]
            {
                new SelectListItem
                {
                    Value = nameof(Domain.Core.Entities.Enumerations.PersonRole.ProductOwner),
                    Text = "Product owner",
                },
                new SelectListItem
                {
                    Value = nameof(Domain.Core.Entities.Enumerations.PersonRole.ScrumMaster),
                    Text = "Scrum master",
                },
                new SelectListItem
                {
                    Value = nameof(Domain.Core.Entities.Enumerations.PersonRole.Developer),
                    Text = "Developer",
                },
            };
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
