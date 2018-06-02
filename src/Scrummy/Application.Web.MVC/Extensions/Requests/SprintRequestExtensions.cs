using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Extensions.Requests
{
    public static class SprintRequestExtensions
    {
        public static CreateSprintRequest ToRequest(this CreateSprintViewModel vm, string userId)
        {
            return new CreateSprintRequest(userId)
            {
                Goal = vm.Goal,
                Name = vm.Name,
                ProjectId = Identity.FromString(vm.Project.Id),
                TimeSpan = new Tuple<DateTime, DateTime>(
                    DateTime.ParseExact(vm.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(vm.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)),
                Stories = vm.SelectedStories.Select(Identity.FromString),
                Documents = vm.SelectedDocumentIds.Select(Identity.FromString),
            };
        }

        public static EditSprintRequest ToRequest(this EditSprintViewModel vm, string userId)
        {
            return new EditSprintRequest(userId)
            {
                Id = Identity.FromString(vm.Id),
                Goal = vm.Goal,
                Name = vm.Name,
                TimeSpan = new Tuple<DateTime, DateTime>(
                    DateTime.ParseExact(vm.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(vm.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)),
                Stories = vm.SelectedStories.Select(Identity.FromString),
                Documents = vm.SelectedDocumentIds.Select(Identity.FromString),
            };
        }
        public static ChangeTaskStatusRequest ToRequest(this string sprintId, string taskId, string status, string userId)
        {
            return new ChangeTaskStatusRequest(userId)
            {
                SprintId = Identity.FromString(sprintId),
                TaskId = Identity.FromString(taskId),
                Status = Enum.Parse<SprintBacklog.WorkTaskStatus>(status)
            };
        }

        public static EndSprintRequest ToRequest(this EndSprintViewModel vm, string userId)
        {
            return new EndSprintRequest(userId)
            {
                Id = Identity.FromString(vm.Sprint.Id),
                Stories = vm.Ids.Select((t, i) => new EndSprintRequest.Story
                {
                    Id = Identity.FromString(t),
                    Decision = Enum.Parse<EndSprintRequest.StoryDecision>(vm.Decisions[i]),
                }),
            };
        }
    }
}
