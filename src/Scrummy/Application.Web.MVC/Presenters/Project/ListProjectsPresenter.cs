using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ListProjectsPresenter : BasePresenter
    {
        public ListProjectsPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ListProjectsViewModel Present()
        {
            var projects = RepositoryProvider.Project.ListAll().Select(x =>
            {
                var project = RepositoryProvider.Project.Read(x.Id);
                var team = RepositoryProvider.Team.Read(project.TeamId);

                return new ListProjectsViewModel.Project
                {
                    ProjectViewModel = new NavigationViewModel
                    {
                        Id = project.Id.ToString(),
                        Text = project.Name,
                    },
                    TeamViewModel = new NavigationViewModel
                    {
                        Id = team.Id.ToString(),
                        Text = team.Name,
                    }
                };
            });

            return new ListProjectsViewModel
            {
                Projects = projects,
            };
        }
    }
}
