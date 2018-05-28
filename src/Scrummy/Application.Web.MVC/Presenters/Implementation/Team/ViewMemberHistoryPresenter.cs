using System;
using System.Globalization;
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
    internal class ViewMemberHistoryPresenter : Presenter, IViewMemberHistoryPresenter
    {
        public ViewMemberHistoryPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewMemberHistoryViewModel Present(ViewMemberHistoryResponse response)
        {
            return new ViewMemberHistoryViewModel
            {
                Team = new NavigationViewModel
                {
                    Id = response.Team.Id.ToString(),
                    Text = response.Team.Name,
                },
                Members = response.Members.Select(x => new ViewMemberHistoryViewModel.TeamMembers
                {
                    To = x.To.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    From = x.From.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Members = x.Select(y =>
                    {
                        var person = RepositoryProvider.Person.Read(y.Id);

                        return new ViewMemberHistoryViewModel.Member
                        {
                            Id = person.Id.ToString(),
                            Text = person.DisplayName,
                            Role = ConvertEnumToRoleString(y.Role),
                        };
                    })
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
