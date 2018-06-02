using System;
using System.Collections.Generic;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class EditTeamRequest : AuthorizedIdRequest
    {
        public EditTeamRequest(string userId) : base(userId)
        {
        }

        public string Name { get; set; }

        public TimeSpan TimeOfDailyScrum { get; set; }

        public IEnumerable<Core.Entities.Team.Member> Members { get; set; }

        protected override void ValidateCore()
        {
            base.ValidateCore();
            if (!Core.Entities.Team.Validation.ValidateName(Name))
                AddError(Core.Entities.Team.Validation.NameErrorKey, Core.Entities.Team.Validation.NameIsInvalidMessage);

            if (!Core.Entities.Team.Validation.ValidateTeamMembers(Members))
                AddError(Core.Entities.Team.Validation.MembersErrorKey, Core.Entities.Team.Validation.TeamMembersAreInvalidMessage);
        }
    }

    public interface IEditTeamUseCase
    {
        ConfirmationResponse Execute(EditTeamRequest response);
    }
}
