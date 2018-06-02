using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ViewProjectResponse : BaseResponse
    {
        public ViewProjectResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Identity TeamId { get; set; }

        public IEnumerable<string> DefinitionOfDone { get; set; }

        public ViewSprintResponse Sprint { get; set; }

        public SprintReportResponse Report { get; set; }

        public bool CanDelete { get; set; } = true;
    }

    public interface IViewProjectUseCase
    {
        ViewProjectResponse Execute(AuthorizedIdRequest request);
    }
}
