using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Application.Web.MVC.Extensions.Requests
{
    public static class MeetingRequestExtensions
    {
        public static CreateMeetingRequest ToRequest(this CreateMeetingViewModel vm, string userId)
        {
            return new CreateMeetingRequest(userId)
            {
                Name = vm.Name,
                Description = vm.Description,
                ProjectId = Identity.FromString(vm.Project.Id),
                OrganizedBy = Identity.FromString(vm.OrganizedBy.Id),
                InvolvedPersons = vm.SelectedPersonIds.Select(Identity.FromString),
                Time = DateTime.ParseExact(vm.Time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Duration = DateTime.ParseExact(vm.Duration, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay,
                Log = vm.Log,
                Documents = vm.SelectedDocumentIds.Select(Identity.FromString),
            };
        }

        public static EditMeetingRequest ToRequest(this EditMeetingViewModel vm, string userId)
        {
            return new EditMeetingRequest(userId)
            {
                Id = Identity.FromString(vm.Id),
                Name = vm.Name,
                Description = vm.Description,
                InvolvedPersons = vm.SelectedPersonIds.Select(Identity.FromString),
                Time = DateTime.ParseExact(vm.Time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Duration = DateTime.ParseExact(vm.Duration, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay,
                Log = vm.Log,
                Documents = vm.SelectedDocumentIds.Select(Identity.FromString),
            };
        }
    }
}
