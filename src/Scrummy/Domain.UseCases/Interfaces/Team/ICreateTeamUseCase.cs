using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using TeamValidation = Scrummy.Domain.Core.Entities.Team.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class CreateTeamRequest : AuthorizedRequest
    {
        public CreateTeamRequest(string userId) : base(userId)
        {
        }

        public string Name { get; set; }

        public TimeSpan TimeOfDailyScrum { get; set; }

        public IEnumerable<Core.Entities.Team.Member> Members { get; set; }

        protected override void ValidateCore()
        {
            if (!TeamValidation.ValidateName(Name))
                AddError(TeamValidation.NameErrorKey, TeamValidation.NameIsInvalidMessage);

            if (!TeamValidation.ValidateTeamMembers(Members))
                AddError(TeamValidation.MembersErrorKey, TeamValidation.TeamMembersAreInvalidMessage);
        }
    }

    public interface ICreateTeamUseCase
    {
        ConfirmationResponse Execute(CreateTeamRequest request);
    }
}
