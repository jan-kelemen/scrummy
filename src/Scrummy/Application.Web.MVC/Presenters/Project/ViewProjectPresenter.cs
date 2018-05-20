using System;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ViewProjectPresenter : Presenter
    {
        private readonly ViewSprintPresenter _viewSprintPresenter;

        public ViewProjectPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
            _viewSprintPresenter = new ViewSprintPresenter(MessageHandler, ErrorHandler, RepositoryProvider);
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
                Sprint = response.Sprint != null ? _viewSprintPresenter.Present(response.Sprint) : null,
            };
        }
    }
}
