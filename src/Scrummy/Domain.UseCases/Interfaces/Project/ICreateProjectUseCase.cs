using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using ProjectValidation = Scrummy.Domain.Core.Entities.Project.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class CreateProjectRequest : AuthorizedRequest
    {
        public CreateProjectRequest(string userId) : base(userId)
        {
        }

        public string Name { get; set; }

        public IEnumerable<string> DefinitionOfDone { get; set; }

        public Identity TeamId { get; set; }

        protected override void ValidateCore()
        {
            if (!ProjectValidation.ValidateName(Name))
                AddError(ProjectValidation.NameErrorKey, ProjectValidation.NameIsInvalidMessage);

            if (!ProjectValidation.ValidateDefinitionOfDoneConditions(DefinitionOfDone))
                AddError(ProjectValidation.DefinitionOfDoneErrorKey, ProjectValidation.DefinitionOfDoneIsInvalid);

            if (TeamId.IsBlankIdentity())
                AddError(ProjectValidation.TeamErrorKey, ProjectValidation.TeamIsInvalidMessage);
        }
    }

    public interface ICreateProjectUseCase
    {
        ConfirmationResponse Execute(CreateProjectRequest request);
    }
}
