using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Team
{
    internal class CreateTeamPresenter : Presenter, ICreateTeamPresenter
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

        private SelectListItem[] Persons() => RepositoryProvider.Person.ListAll().Select(x => x.ToSelectListItem()).ToArray();
    }
}
