using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class CreateSprintUseCase : ICreateSprintUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateSprintUseCase(ISprintRepository sprintRepository, IProjectRepository projectRepository)
        {
            _sprintRepository = sprintRepository;
            _projectRepository = projectRepository;
        }

        public CreateSprintResponse Execute(CreateSprintRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = ToDomainEntity(request);

            var projectBacklog = _projectRepository.ReadProductBacklog(request.ProjectId);
            foreach (var story in request.Stories)
            {
                projectBacklog.UpdateTask(new ProductBacklog.WorkTaskWithStatus(story, ProductBacklog.WorkTaskStatus.InSprint));
            }

            var result = _sprintRepository.Create(entity);
            var backlog = ToDomainEntity(result, request.Stories);
            _sprintRepository.UpdatePlannedTasks(backlog);
            _projectRepository.UpdateProductBacklog(projectBacklog);

            return new CreateSprintResponse("Sprint created successfully.")
            {
                Id = result,
            };
        }

        private Core.Entities.Sprint ToDomainEntity(CreateSprintRequest request) => 
            new Core.Entities.Sprint(_sprintRepository.GenerateNewIdentity(), request.ProjectId, request.Name, request.TimeSpan, request.Goal, SprintStatus.Planned);

        private SprintBacklog ToDomainEntity(Identity sprintId, IEnumerable<Identity> stories) => 
            new SprintBacklog(sprintId, stories, new SprintBacklog.WorkTaskWithStatus[0]);
    }
}
