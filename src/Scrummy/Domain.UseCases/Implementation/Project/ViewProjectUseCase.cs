using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Implementation.Sprint;
using Scrummy.Domain.UseCases.Interfaces.Project;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class ViewProjectUseCase : IViewProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ISprintRepository _sprintRepository;
        private readonly IViewSprintUseCase _viewSprintUseCase;
        private readonly ISprintReportUseCase _sprintReportUseCase;

        public ViewProjectUseCase(IProjectRepository projectRepository, ISprintRepository sprintRepository, IWorkTaskRepository workTaskRepository)
        {
            _projectRepository = projectRepository;
            _sprintRepository = sprintRepository;
            _viewSprintUseCase = new ViewSprintUseCase(sprintRepository, workTaskRepository);
            _sprintReportUseCase = new SprintReportUseCase(sprintRepository, workTaskRepository);
        }

        public ViewProjectResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _projectRepository.Read(request.Id);
            var response = new ViewProjectResponse
            {
                Id = request.Id,
                Name = entity.Name,
                DefinitionOfDone = entity.DefinitionOfDone.Conditions,
                Description = entity.Description,
                TeamId = entity.TeamId,
            };
            var sprint = _sprintRepository.ReadCurrentSprint(request.Id);
            if (sprint != null)
            {
                response.Sprint = _viewSprintUseCase.Execute(new AuthorizedIdRequest(request.UserId)
                {
                    Id = sprint.Id,
                });
                response.Report = _sprintReportUseCase.Execute(new AuthorizedIdRequest(request.UserId)
                {
                    Id = sprint.Id,
                });
            }

            return response;
        }
    }
}