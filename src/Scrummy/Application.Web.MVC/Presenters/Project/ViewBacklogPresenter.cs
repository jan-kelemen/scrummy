using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ViewBacklogPresenter : BasePresenter
    {
        public ViewBacklogPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewBacklogViewModel Present(ViewBacklogResponse response)
        {
            var project = RepositoryProvider.Project.Read(response.ProjectId);
            return new ViewBacklogViewModel
            {
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                Tasks = response.Tasks.Select(x => new ViewBacklogViewModel.Task
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    ParentTask = x.ParentTask == null ? null : new NavigationViewModel
                    {
                        Id = x.ParentTask.Id.ToString(),
                        Text = x.ParentTask.Name,
                    },
                    Status = x.Status.ToString(),
                }),
            };
        }
    }
}
