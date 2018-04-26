using System;
using System.Collections.Generic;
using System.Text;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using ProjectValidatior = Scrummy.Domain.Core.Entities.Project.Validation;

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
            if (!ProjectValidatior.ValidateName(Name))
                AddError(ProjectValidatior.NameErrorKey, ProjectValidatior.NameIsInvalidMessage);

            if (!ProjectValidatior.ValidateDefinitionOfDoneConditions(DefinitionOfDone))
                AddError(ProjectValidatior.DefinitionOfDoneErrorKey, ProjectValidatior.DefinitionOfDoneIsInvalid);

            if (TeamId.IsBlankIdentity())
                AddError(ProjectValidatior.TeamErrorKey, ProjectValidatior.TeamIsInvalidMessage);
        }
    }

    public class CreateProjectResponse : BaseResponse
    {
        public CreateProjectResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface ICreateProjectUseCase
    {
        CreateProjectResponse Execute(CreateProjectRequest request);
    }
}
