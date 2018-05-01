using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ViewProjectPresenter : BasePresenter
    {
        public ViewProjectPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewProjectViewModel Present(ViewProjectResponse response)
        {
            var team = RepositoryProvider.Team.Read(response.TeamId);
            return new ViewProjectViewModel
            {
                Id = response.Id.ToString(),
                Name = response.Name,
                Team = new NavigationViewModel
                {
                    Id = team.Id.ToString(),
                    Text = team.Name,
                },
                DefinitionOfDone = response.DefinitionOfDone,
            };
        }
    }
}
