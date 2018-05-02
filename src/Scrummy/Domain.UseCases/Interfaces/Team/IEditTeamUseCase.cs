using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class EditTeamRequest : AuthorizedRequest
    {
        public EditTeamRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public TimeSpan TimeOfDailyScrum { get; set; }

        public IEnumerable<Core.Entities.Team.Member> Members { get; set; }

        protected override void ValidateCore()
        {
            if (!Core.Entities.Team.Validation.ValidateName(Name))
                AddError(Core.Entities.Team.Validation.NameErrorKey, Core.Entities.Team.Validation.NameIsInvalidMessage);

            if (!Core.Entities.Team.Validation.ValidateTeamMembers(Members))
                AddError(Core.Entities.Team.Validation.MembersErrorKey, Core.Entities.Team.Validation.TeamMembersAreInvalidMessage);
        }
    }

    public class EditTeamResponse : BaseResponse
    {
        public EditTeamResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface IEditTeamUseCase
    {
        EditTeamResponse Execute(EditTeamRequest response);
    }
}
