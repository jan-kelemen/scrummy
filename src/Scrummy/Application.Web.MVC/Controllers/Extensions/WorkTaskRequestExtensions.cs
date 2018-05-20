using System;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Application.Web.MVC.Controllers.Extensions
{
    public static class WorkTaskRequestExtensions
    {
        public static CreateWorkTaskRequest ToRequest(this CreateWorkTaskViewModel vm, string userId)
        {
            return new CreateWorkTaskRequest(userId)
            {
                Name = vm.Name,
                ProjectId = Identity.FromString(vm.Project.Id),
                ParentTask = string.IsNullOrWhiteSpace(vm.ParentTaskId) ? Identity.BlankIdentity : Identity.FromString(vm.ParentTaskId),
                Type = Enum.Parse<WorkTaskType>(vm.Type),
                Description = vm.Description,
                ChildTasks = vm.ChildTaskIds.Select(Identity.FromString),
                StoryPoints = vm.StoryPoints,
            };
        }

        public static EditWorkTaskRequest ToRequest(this EditWorkTaskViewModel vm, string userId)
        {
            return new EditWorkTaskRequest(userId)
            {
                Name = vm.Name,
                ParentTask = string.IsNullOrWhiteSpace(vm.ParentTaskId) ? Identity.BlankIdentity : Identity.FromString(vm.ParentTaskId),
                Description = vm.Description,
                ChildTasks = vm.ChildTaskIds.Select(Identity.FromString),
                StoryPoints = vm.StoryPoints,
                Id = Identity.FromString(vm.Id),
            };
        }
    }
}
