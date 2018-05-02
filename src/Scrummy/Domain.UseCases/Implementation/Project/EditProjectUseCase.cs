using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class EditProjectUseCase : IEditProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;

        public EditProjectUseCase(IProjectRepository projectRepository, ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
        }

        public EditProjectResponse Execute(EditProjectRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _projectRepository.Read(request.Id);
            var team = _teamRepository.Read(request.TeamId);

            entity.Name = request.Name;
            entity.DefinitionOfDone = new DefinitionOfDone(request.DefinitionOfDone);
            entity.TeamId = team.Id;

            _projectRepository.Update(entity);

            return new EditProjectResponse("Project updated successfully.")
            {
                Id = entity.Id,
            };
        }
    }
}
