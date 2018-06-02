using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class ListProjectsPresenter : Presenter, IListProjectsPresenter
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
                    ProjectViewModel = project.ToViewModel(),
                    TeamViewModel = team.ToViewModel(),
                };
            });

            return new ListProjectsViewModel
            {
                Projects = projects,
            };
        }
    }
}
