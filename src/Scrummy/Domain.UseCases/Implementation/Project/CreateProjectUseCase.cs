using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class CreateProjectUseCase : ICreateProjectUseCase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateProjectUseCase(IProjectRepository projectRepository, ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(CreateProjectRequest request)
        {
            request.ThrowExceptionIfInvalid();

            if (_projectRepository.CheckIfProjectWithNameExists(request.Name))
                throw new UseCaseException("Project of the same name already exists.");

            var team = _teamRepository.Read(request.TeamId);
            var project = ToDomainEntity(request, team);
            var result = _projectRepository.Create(project);

            return new ConfirmationResponse("Project created successfully.")
            {
                Id = result,
            };
        }

        private Core.Entities.Project ToDomainEntity(CreateProjectRequest request, Core.Entities.Team team)
        {
            var dod = new DefinitionOfDone(request.DefinitionOfDone);
            return new Core.Entities.Project(_projectRepository.GenerateNewIdentity(), request.Name, dod, team.Id);
        }
    }
}
