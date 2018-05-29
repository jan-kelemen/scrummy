using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Person
{
    internal class ViewTeamHistoryPresenter : Presenter, IViewTeamHistoryPresenter
    {
        public ViewTeamHistoryPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewTeamHistoryViewModel Present(ViewTeamHistoryResponse response)
        {
            return new ViewTeamHistoryViewModel
            {
                Person = new NavigationViewModel
                {
                    Id = response.Person.Id.ToString(),
                    Text = response.Person.Name,
                },
                Teams = response.Teams.Select(x =>
                {
                    var project = RepositoryProvider.Team.Read(x.Id);

                    return new ViewTeamHistoryViewModel.Team
                    {
                        Id = project.Id.ToString(),
                        Text = project.Name,
                        Roles = string.Join(", ", x.Roles.Select(ConvertEnumToRoleString)).TrimEnd(' ', ','),
                        To = x.To.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        From = x.From.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    };
                }),
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
