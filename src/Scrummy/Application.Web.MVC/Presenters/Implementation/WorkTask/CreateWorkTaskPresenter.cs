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
    internal class CreateWorkTaskPresenter : Presenter, ICreateWorkTaskPresenter
    {
        public CreateWorkTaskPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateWorkTaskViewModel GetInitialViewModel(string projectId, string type, string parent = null, string child = null)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(projectId));
            var backlog = RepositoryProvider.Project.ReadProductBacklog(project.Id);

            var possibleParents = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done,
                CreateParentTypeFilter(Parse(type)));

            var possibleChildren = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done,
                CreateChildTypeFilter(Parse(type)));

            var vm = new CreateWorkTaskViewModel
            {
                Type = type,
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                ParentTasks = ToSelectListWithBlankEntry(possibleParents),
                ChildTasks = ToSelectListWithBlankEntry(possibleChildren),
            };

            if (!string.IsNullOrWhiteSpace(parent))
                vm.ParentTaskId = parent;
            if (!string.IsNullOrWhiteSpace(child))
                vm.ChildTaskIds.Add(child);
            return vm;
        }

        public CreateWorkTaskViewModel Present(CreateWorkTaskViewModel vm)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(vm.Project.Id));
            var backlog = RepositoryProvider.Project.ReadProductBacklog(project.Id);

            var possibleParents = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done && status != ProductBacklog.WorkTaskStatus.InSprint,
                CreateParentTypeFilter(Parse(vm.Type)));

            var possibleChildren = GetTasksFromBacklog(
                backlog,
                status => status != ProductBacklog.WorkTaskStatus.Done && status != ProductBacklog.WorkTaskStatus.InSprint,
                CreateChildTypeFilter(Parse(vm.Type)));

            vm.ParentTasks = ToSelectListWithBlankEntry(possibleParents);
            vm.ChildTasks = ToSelectListWithBlankEntry(possibleChildren);

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

        private IEnumerable<Domain.Core.Entities.WorkTask> GetTasksFromBacklog(ProductBacklog backlog, Func<ProductBacklog.WorkTaskStatus, bool> statusFilter, Func<WorkTaskType, bool> typeFilter)
        {
            return backlog
                .Where(x => statusFilter(x.Status))
                .Select(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId))
                .Where(x => typeFilter(x.Type));
        }

        private SelectListItem[] ToSelectListWithBlankEntry(IEnumerable<Domain.Core.Entities.WorkTask> tasks)
        {
            var tasksEnumerable = tasks.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });

            return new[] {new SelectListItem {Value = "", Text = ""}}.Concat(tasksEnumerable).ToArray();
        }
    }
}
