using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
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
                Project = response.Project.ToViewModel(),
                Teams = response.Teams.Select(x =>
                {
                    var team = RepositoryProvider.Team.Read(x.Id);

                    return new ViewTeamHistoryViewModel.Team
                    {
                        Id = team.Id.ToPresentationIdentity(),
                        Text = team.Name,
                        To = x.To.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        From = x.From.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    };
                }),
            };
        }
    }
}
