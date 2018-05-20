using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class EditSprintUseCase : IEditSprintUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IProjectRepository _projectRepository;

        public EditSprintUseCase(ISprintRepository sprintRepository, IProjectRepository projectRepository)
        {
            _sprintRepository = sprintRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(EditSprintRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprint = _sprintRepository.Read(request.Id);
            Update(sprint, request);

            var backlog = _sprintRepository.ReadSprintBacklog(sprint.Id);
            var removedStories = backlog.Stories.Except(request.Stories);
            backlog.Stories = request.Stories.ToList();

            var projectBacklog = _projectRepository.ReadProductBacklog(sprint.ProjectId);
            foreach (var story in removedStories)
                projectBacklog.UpdateTask(new ProductBacklog.WorkTaskWithStatus(story, ProductBacklog.WorkTaskStatus.Ready));

            foreach (var story in request.Stories)
                projectBacklog.UpdateTask(new ProductBacklog.WorkTaskWithStatus(story, ProductBacklog.WorkTaskStatus.InSprint));

            _sprintRepository.Update(sprint);
            _sprintRepository.UpdatePlannedTasks(backlog);
            _projectRepository.UpdateProductBacklog(projectBacklog);

            return new ConfirmationResponse("Sprint updated successfully.")
            {
                Id = request.Id,
            };
        }

        private void Update(Core.Entities.Sprint sprint, EditSprintRequest request)
        {
            sprint.Name = request.Name;
            sprint.Goal = request.Goal;
            sprint.TimeSpan = request.TimeSpan;
        }
    }
}
