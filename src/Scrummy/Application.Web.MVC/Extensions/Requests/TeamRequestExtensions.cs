using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Extensions.Requests
{
    public static class TeamRequestExtensions
    {
        public static CreateTeamRequest ToRequest(this CreateTeamViewModel vm, string userId)
        {
            return new CreateTeamRequest(userId)
            {
                Name = vm.Name,
                TimeOfDailyScrum = TimeSpan.ParseExact(vm.TimeOfDailyScrum, @"hh\:mm", CultureInfo.InvariantCulture),
                Members = vm.SelectedMemberIds.Select((t, i) => new Team.Member(Identity.FromString(t), Enum.Parse<PersonRole>(vm.SelectedRoles[i])))
            };
        }

        public static EditTeamRequest ToRequest(this EditTeamViewModel vm, string userId)
        {
            return new EditTeamRequest(userId)
            {
                Id = Identity.FromString(vm.Id),
                Name = vm.Name,
                TimeOfDailyScrum = TimeSpan.ParseExact(vm.TimeOfDailyScrum, @"hh\:mm", CultureInfo.InvariantCulture),
                Members = vm.SelectedMemberIds.Select((t, i) => new Team.Member(Identity.FromString(t), Enum.Parse<PersonRole>(vm.SelectedRoles[i])))
            };
        }
    }
}
