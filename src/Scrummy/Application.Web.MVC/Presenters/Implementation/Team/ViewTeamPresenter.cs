using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Team
{
    internal class ViewTeamPresenter : Presenter, IViewTeamPresenter
    {
        public ViewTeamPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewTeamViewModel Present(ViewTeamResponse response)
        {
            return new ViewTeamViewModel
            {
                Id = response.Id.ToString(),
                Name = response.Name,
                TimeOfDailyScrum = response.TimeOfDailyScrum.ToString(@"hh\:mm"),
                Members = response.Members.Select(x =>
                {
                    var p = RepositoryProvider.Person.Read(x.Id);

                    return new ViewTeamViewModel.Member
                    {
                        Id = p.Id.ToString(),
                        Text = p.DisplayName,
                        Role = ConvertEnumToRoleString(x.Role),
                    };
                }),
                CurrentProjects = response.CurrentProjects.Select(x =>
                {
                    var p = RepositoryProvider.Project.Read(x);

                    return new NavigationViewModel
                    {
                        Id = p.Id.ToString(),
                        Text = p.Name,
                    };
                }),
                CanDelete = response.CanDelete,
            };
        }

        private string ConvertEnumToRoleString(PersonRole role)
        {
            switch (role)
            {
                case PersonRole.ProductOwner:
                    return "Product owner";
                case PersonRole.ScrumMaster:
                    return "Scrum master";
                case PersonRole.Developer:
                    return "Developer";
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
