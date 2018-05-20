using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ManageBacklogPresenter : BasePresenter
    {
        public ManageBacklogPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ManageBacklogViewModel GetInitialViewModel(string id)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(id));
            var backlog = RepositoryProvider.Project.ReadProductBacklog(project.Id);
            var items = GetBacklogItems(backlog);

            return new ManageBacklogViewModel
            {
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                Ids = items.Item1,
                Name = items.Item2,
                Type = items.Item3,
                StoryPoints = items.Item4,
                Status = items.Item5
            };
        }

        public string Present(ManageBacklogResponse response)
        {
            PresentMessage(MessageType.Success, response.Message);
            return response.ProjectId.ToString();
        }

        private Tuple<string[], string[], string[], string[], string[]> GetBacklogItems(ProductBacklog backlog)
        {
            var id = new List<string>();
            var name = new List<string>();
            var type = new List<string>();
            var point = new List<string>();
            var status = new List<string>();

            var hack = backlog
                .Where(x => x.Status == ProductBacklog.WorkTaskStatus.ToDo || x.Status == ProductBacklog.WorkTaskStatus.Ready)
                .All(x =>
                {
                    var task = RepositoryProvider.WorkTask.Read(x.WorkTaskId);

                    id.Add(task.Id.ToString());
                    name.Add(task.Name);
                    type.Add(task.Type.ToString());
                    point.Add(task.StoryPoints?.ToString());
                    status.Add(x.Status.ToString());

                    return true;
                });

            return new Tuple<string[], string[], string[], string[], string[]>(
                id.ToArray(), name.ToArray(), type.ToArray(), point.ToArray(), status.ToArray());
        }
    }
}
