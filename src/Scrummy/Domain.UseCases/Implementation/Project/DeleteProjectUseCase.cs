using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class DeleteProjectUseCase : IDeleteProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(DeleteProjectRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var meeting = _projectRepository.Read(request.Id);
            _projectRepository.Delete(meeting.Id);

            return new ConfirmationResponse($"Project {meeting.Name} successfully deleted.")
            {
                Id = meeting.Id,
            };
        }
    }
}
