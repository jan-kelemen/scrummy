using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ViewProjectRequest : AuthorizedRequest
    {
        public ViewProjectRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
            {
                AddError("", "Idenitity is invalid.");
            }
        }
    }

    public class ViewProjectResponse : BaseResponse
    {
        public ViewProjectResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public Identity TeamId { get; set; }

        public IEnumerable<string> DefinitionOfDone { get; set; }
    }

    public interface IViewProjectUseCase
    {
        ViewProjectResponse Execute(ViewProjectRequest request);
    }
}
