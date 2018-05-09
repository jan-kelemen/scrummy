using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Project;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Factories
{
    internal class ProjectUseCaseFactory : IProjectUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public ProjectUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateProjectUseCase Create => new CreateProjectUseCase(_repositoryProvider.Project, _repositoryProvider.Team);
        public IEditProjectUseCase Edit => new EditProjectUseCase(_repositoryProvider.Project, _repositoryProvider.Team);
        public IViewProjectUseCase View => new ViewProjectUseCase(_repositoryProvider.Project);
        public IViewMeetingsUseCase ViewMeetings => new ViewMeetingsUseCase(_repositoryProvider.Meeting);
        public IViewBacklogUseCase ViewBacklog => new ViewBacklogUseCase(_repositoryProvider.Project, _repositoryProvider.WorkTask);
        public IManageBacklogUseCase ManageBacklog => new ManageBacklogUseCase(_repositoryProvider.Project);
    }
}
