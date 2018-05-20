using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class SprintUseCaseFactory : ISprintUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public SprintUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateSprintUseCase Create =>
            new CreateSprintUseCase(_repositoryProvider.Sprint, _repositoryProvider.Project);

        public IEditSprintUseCase Edit =>
            new EditSprintUseCase(_repositoryProvider.Sprint, _repositoryProvider.Project);

        public IViewSprintUseCase View =>
            new ViewSprintUseCase(_repositoryProvider.Sprint, _repositoryProvider.WorkTask);

        public IStartSprintUseCase Start =>
            new StartSprintUseCase(_repositoryProvider.Sprint, _repositoryProvider.WorkTask);

        public IEndSprintUseCase End =>
            new EndSprintUseCase(_repositoryProvider.Sprint, _repositoryProvider.Project);

        public IChangeTaskStatusUseCase ChangeTaskStatus =>
            new ChangeTaskStatusUseCase(_repositoryProvider.Sprint);
    }
}
