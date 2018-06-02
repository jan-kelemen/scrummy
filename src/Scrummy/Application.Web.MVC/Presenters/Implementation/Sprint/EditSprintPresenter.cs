using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint
{
    internal class EditSprintPresenter : Presenter, IEditSprintPresenter
    {
        public EditSprintPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditSprintViewModel GetInitialViewModel(string sprintId)
        {
            var id = Identity.FromString(sprintId);
            var sprint = RepositoryProvider.Sprint.Read(id);
            var backlog = RepositoryProvider.Sprint.ReadSprintBacklog(id);
            var project = RepositoryProvider.Project.Read(sprint.ProjectId);

            return new EditSprintViewModel
            {
                Id = sprintId,
                Name = sprint.Name,
                Project = project.ToViewModel(),
                Goal = sprint.Goal,
                StartDate = sprint.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = sprint.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Stories = Stories(project.Id, backlog.Stories),
                SelectedStories = backlog.Stories.Select(x => x.ToPresentationIdentity()).ToArray(),
                Documents = Documents(project.Id),
                SelectedDocumentIds = sprint.Documents.Select(x => x.ToPresentationIdentity()).ToArray(),
            };
        }

        public EditSprintViewModel Present(EditSprintViewModel vm)
        {
            var id = Identity.FromString(vm.Id);
            var sprint = RepositoryProvider.Sprint.Read(id);
            var backlog = RepositoryProvider.Sprint.ReadSprintBacklog(id);
            vm.Stories = Stories(sprint.ProjectId, backlog.Stories);
            vm.Documents = Documents(sprint.ProjectId);
            return vm;
        }

        private SelectListItem[] Stories(Identity projectId, IEnumerable<Identity> include)
        {
            var backlog = RepositoryProvider.Project.ReadProductBacklog(projectId);
            var readyStories = backlog
                .Where(x => x.Status == ProductBacklog.WorkTaskStatus.Ready || include.Contains(x.WorkTaskId))
                .Where(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId).Type == WorkTaskType.UserStory)
                .Select(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId).ToSelectListItem());
            return readyStories.ToArray();
        }

        private SelectListItem[] Documents(Identity projectId)
        {
            var common = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Common);
            var sprint = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Sprint);

            return sprint.Concat(common).Select(x => x.ToSelectListItem()).ToArray();
        }
    }
}
