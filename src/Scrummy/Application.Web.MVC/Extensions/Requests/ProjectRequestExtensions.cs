using System;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Extensions.Requests
{
    public static class ProjectRequestExtensions
    {
        public static CreateProjectRequest ToRequest(this CreateProjectViewModel vm, string userId)
        {
            return new CreateProjectRequest(userId)
            {
                Name = vm.Name,
                Description = vm.Description,
                DefinitionOfDone = vm.DefinitionOfDone,
                TeamId = Identity.FromString(vm.SelectedTeamId),
            };
        }
        public static EditProjectRequest ToRequest(this EditProjectViewModel vm, string userId)
        {
            return new EditProjectRequest(userId)
            {
                Id = Identity.FromString(vm.Id),
                Description = vm.Description,
                DefinitionOfDone = vm.DefinitionOfDone,
                Name = vm.Name,
                TeamId = Identity.FromString(vm.SelectedTeamId),
            };
        }
        public static ViewBacklogRequest ToRequest(this string id, ViewBacklogViewModel.BacklogFlavor flavor, string userId)
        {
            var r = new ViewBacklogRequest(userId)
            {
                ProjectId = Identity.FromString(id),
            };
            if (flavor == ViewBacklogViewModel.BacklogFlavor.Done)
                r.Include = status => status == ProductBacklog.WorkTaskStatus.Done;
            return r;
        }
        public static ManageBacklogRequest ToRequest(this ManageBacklogViewModel vm, string userId)
        {
            return new ManageBacklogRequest(userId)
            {
                ProjectId = Identity.FromString(vm.Project.Id),
                BacklogItems = vm.Ids.Select((x, i) => new ManageBacklogRequest.BacklogItem
                {
                    Id = Identity.FromString(x),
                    Status = Enum.Parse<ProductBacklog.WorkTaskStatus>(vm.Status[i])
                }).ToList(),
            };
        }
    }
}
