using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using System.Collections.Generic;
using Scrummy.Domain.UseCases.Boundary.Responses;
using ProjectValidation = Scrummy.Domain.Core.Entities.Project.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class EditProjectRequest : AuthorizedRequest
    {
        public EditProjectRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

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

    public class EditProjectResponse : BaseResponse
    {
        public EditProjectResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface IEditProjectUseCase
    {
        EditProjectResponse Execute(EditProjectRequest request);
    }
}
