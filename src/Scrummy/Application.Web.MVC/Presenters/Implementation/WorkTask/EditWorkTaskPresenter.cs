using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.WorkTask
{
    internal class EditWorkTaskPresenter : Presenter, IEditWorkTaskPresenter
    {
        public EditWorkTaskPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditWorkTaskViewModel GetInitialViewModel(string id)
        {
            var task = RepositoryProvider.WorkTask.Read(Identity.FromString(id));
            var project = RepositoryProvider.Project.Read(task.ProjectId);
            var backlog = RepositoryProvider.Project.ReadProductBacklog(project.Id);

            var possibleParents = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done,
                CreateParentTypeFilter(task.Type));

            var possibleChildren = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done,
                CreateChildTypeFilter(task.Type));
            AddTasksThatDontExist(possibleChildren, task.ChildTasks);

            return new EditWorkTaskViewModel
            {
                Id = task.Id.ToString(),
                Name = task.Name,
                Description = task.Description,
                StoryPoints = task.StoryPoints,
                Type = task.Type.ToString(),
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                ParentTaskId = task.ParentTask.ToString(),
                ChildTaskIds = task.ChildTasks.Select(x => x.ToString()).ToList(),
                ParentTasks = ToSelectListWithBlankEntry(possibleParents),
                ChildTasks = ToSelectListWithBlankEntry(possibleChildren),
                OriginalChildTaskIds = task.ChildTasks.Select(x => x.ToString()).ToList(),
                Steps = task.Steps.ToList(),
                Documents = Documents(task.ProjectId),
                SelectedDocumentIds = task.Documents.Select(x => x.ToString()).ToList(),
            };
        }

        public EditWorkTaskViewModel Present(EditWorkTaskViewModel vm)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(vm.Project.Id));
            var backlog = RepositoryProvider.Project.ReadProductBacklog(project.Id);

            var possibleParents = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done,
                CreateParentTypeFilter(Parse(vm.Type)));

            var possibleChildren = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done,
                CreateChildTypeFilter(Parse(vm.Type)));
            AddTasksThatDontExist(possibleChildren, vm.OriginalChildTaskIds);

            vm.ParentTasks = ToSelectListWithBlankEntry(possibleParents);
            vm.ChildTasks = ToSelectListWithBlankEntry(possibleChildren);
            vm.Documents = Documents(project.Id);

            return vm;
        }

        private WorkTaskType Parse(string type) => Enum.Parse<WorkTaskType>(type);

        private Func<WorkTaskType, bool> CreateParentTypeFilter(WorkTaskType type)
        {
            switch (type)
            {
                case WorkTaskType.Epic:
                    return taskType => false;
                case WorkTaskType.UserStory:
                    return taskType => taskType == WorkTaskType.Epic;
                case WorkTaskType.Task:
                    return taskType => taskType == WorkTaskType.UserStory;
                case WorkTaskType.Defect:
                    return taskType => taskType == WorkTaskType.UserStory;
            }
            throw new ArgumentOutOfRangeException();
        }

        private Func<WorkTaskType, bool> CreateChildTypeFilter(WorkTaskType type)
        {
            switch (type)
            {
                case WorkTaskType.Epic:
                    return taskType => taskType == WorkTaskType.UserStory;
                case WorkTaskType.UserStory:
                    return taskType => taskType == WorkTaskType.Task || taskType == WorkTaskType.Defect;
                case WorkTaskType.Task:
                case WorkTaskType.Defect:
                    return taskType => false;
            }
            throw new ArgumentOutOfRangeException();
        }

        private List<SelectListItem> GetTasksFromBacklog(ProductBacklog backlog, Func<ProductBacklog.WorkTaskStatus, bool> statusFilter, Func<WorkTaskType, bool> typeFilter)
        {
            return backlog
                .Where(x => statusFilter(x.Status))
                .Select(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId))
                .Where(x => typeFilter(x.Type))
                .Select(AsSelectListItem)
                .ToList();
        }

        private void AddTasksThatDontExist(List<SelectListItem> items, IEnumerable<Identity> tasks)
        {
            foreach (var identity in tasks)
            {
                if (items.All(x => x.Value != identity.ToString()))
                {
                    var task = RepositoryProvider.WorkTask.Read(identity);
                    items.Add(AsSelectListItem(task));
                }
            }
        }

        private void AddTasksThatDontExist(List<SelectListItem> items, IEnumerable<string> tasks)
            => AddTasksThatDontExist(items, tasks.Select(Identity.FromString));

        private SelectListItem[] ToSelectListWithBlankEntry(IEnumerable<SelectListItem> tasks)
            => new[] { new SelectListItem { Value = "", Text = "" } }.Concat(tasks).ToArray();

        private SelectListItem AsSelectListItem(Domain.Core.Entities.WorkTask task)
        {
            return new SelectListItem
            {
                Value = task.Id.ToString(),
                Text = task.Name,
            };
        }

        private SelectListItem[] Documents(Identity projectId)
        {
            var common = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Common);
            var sprint = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.WorkTask);

            return sprint.Concat(common).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToArray();
        }
    }
}
