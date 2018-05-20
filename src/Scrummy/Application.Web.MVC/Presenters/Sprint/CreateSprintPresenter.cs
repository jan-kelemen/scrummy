using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public class CreateSprintPresenter : Presenter
    {
        public CreateSprintPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateSprintViewModel GetInitialViewModel(string projectId)
        {
            var id = Identity.FromString(projectId);
            var project = RepositoryProvider.Project.Read(id);

            return new CreateSprintViewModel
            {
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                StartDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Stories = Stories(id),
            };
        }

        public CreateSprintViewModel Present(CreateSprintViewModel vm)
        {
            vm.Stories = Stories(Identity.FromString(vm.Project.Id));

            return vm;
        }

        private SelectListItem[] Stories(Identity projectId)
        {
            var backlog = RepositoryProvider.Project.ReadProductBacklog(projectId);
            var readyStories = backlog
                .Where(x => x.Status == ProductBacklog.WorkTaskStatus.Ready)
                .Where(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId).Type == WorkTaskType.UserStory)
                .Select(x =>
                {
                    var story = RepositoryProvider.WorkTask.Read(x.WorkTaskId);

                    return new SelectListItem
                    {
                        Value = story.Id.ToString(),
                        Text = story.Name,
                    };
                });
            return readyStories.ToArray();
        }
    }
}
