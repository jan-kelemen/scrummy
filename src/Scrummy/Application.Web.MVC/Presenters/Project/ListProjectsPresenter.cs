using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ListProjectsPresenter : BasePresenter
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;

        public ListProjectsPresenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler, IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler)
        {
            _projectRepository = repositoryProvider.Project;
            _teamRepository = repositoryProvider.Team;
        }

        public ListProjectsViewModel Present()
        {
            var projects = _projectRepository.ListAll().Select(x =>
            {
                var project = _projectRepository.Read(x.Id);
                var team = _teamRepository.Read(project.TeamId);

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
