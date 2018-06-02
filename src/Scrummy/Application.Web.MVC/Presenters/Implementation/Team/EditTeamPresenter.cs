using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Team
{
    internal class EditTeamPresenter : Presenter, IEditTeamPresenter
    {
        public EditTeamPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditTeamViewModel GetInitialViewModel(string id)
        {
            var entity = RepositoryProvider.Team.Read(Identity.FromString(id));

            return new EditTeamViewModel
            {
                Id = id,
                Name = entity.Name,
                TimeOfDailyScrum = entity.TimeOfDailyScrum.ToString(@"hh\:mm"),
                Persons = Persons(),
                Roles = Roles(),
                SelectedMemberIds = entity.Members.Select(x => x.Id.ToPresentationIdentity()).ToList(),
                SelectedRoles = entity.Members.Select(x => RoleForSelection(x.Role)).ToList(),
            };
        }

        public EditTeamViewModel Present(EditTeamViewModel vm)
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
                    Value = nameof(PersonRole.ProductOwner),
                    Text = "Product owner",
                },
                new SelectListItem
                {
                    Value = nameof(PersonRole.ScrumMaster),
                    Text = "Scrum master",
                },
                new SelectListItem
                {
                    Value = nameof(PersonRole.Developer),
                    Text = "Developer",
                },
            };
        }

        private string RoleForSelection(PersonRole role)
        {
            switch (role)
            {
                case PersonRole.ProductOwner:
                    return nameof(PersonRole.ProductOwner);
                case PersonRole.ScrumMaster:
                    return nameof(PersonRole.ScrumMaster);
                case PersonRole.Developer:
                    return nameof(PersonRole.Developer);
            }

            throw new ArgumentOutOfRangeException();
        }

        private SelectListItem[] Persons() => RepositoryProvider.Person.ListAll().Select(x => x.ToSelectListItem()).ToArray();
    }
}
