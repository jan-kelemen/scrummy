using System;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Application.Web.MVC.ViewModels.WorkTask.Comment;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;
using Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment;

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
                Steps = vm.Steps
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
                Steps = vm.Steps
            };
        }

        public static AddCommentRequest ToRequest(this AddCommentViewModel vm, string userId)
        {
            return new AddCommentRequest(userId)
            {
                WorkTaskId = Identity.FromString(vm.WorkTask.Id),
                AuthorId = Identity.FromString(userId),
                Content = vm.Content,
            };
        }

        public static EditCommentRequest ToRequest(this EditCommentViewModel vm, string userId)
        {
            return new EditCommentRequest(userId)
            {
                WorkTaskId = Identity.FromString(vm.WorkTask.Id),
                CommentId = Identity.FromString(vm.CommentId),
                Content = vm.Content,
            };
        }
    }
}
