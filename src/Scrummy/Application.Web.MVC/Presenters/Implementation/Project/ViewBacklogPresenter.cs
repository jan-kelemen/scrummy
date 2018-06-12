using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class ViewBacklogPresenter : Presenter, IViewBacklogPresenter
    {
        public ViewBacklogPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewBacklogViewModel Present(ViewBacklogResponse response, ViewBacklogViewModel.BacklogFlavor flavor)
        {
            var project = RepositoryProvider.Project.Read(response.ProjectId);
            return new ViewBacklogViewModel
            {
                Project = project.ToViewModel(),
                Flavor = flavor,
                Tasks = response.Tasks.Select(x => new ViewBacklogViewModel.Task
                {
                    Id = x.Id.ToPresentationIdentity(),
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    ParentTask = x.ParentTask?.ToViewModel(),
                    StoryPoints = x.StoryPoints?.ToString(),
                    Status = x.Status.ToString(),
                }),
                CanManageBacklog = response.CanManageBacklog,
            };
        }
    }
}
