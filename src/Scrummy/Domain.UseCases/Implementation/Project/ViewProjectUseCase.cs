using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class ViewProjectUseCase : IViewProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public ViewProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ViewProjectResponse Execute(ViewProjectRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _projectRepository.Read(request.Id);

            return new ViewProjectResponse
            {
                Id = request.Id,
                Name = entity.Name,
                DefinitionOfDone = entity.DefinitionOfDone.Conditions,
                TeamId = entity.TeamId,
            };
        }
    }
}