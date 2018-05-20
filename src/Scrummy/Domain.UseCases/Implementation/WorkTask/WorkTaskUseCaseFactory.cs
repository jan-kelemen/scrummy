using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class WorkTaskUseCaseFactory : IWorkTaskUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public WorkTaskUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateWorkTaskUseCase Create => new CreateWorkTaskUseCase(_repositoryProvider.WorkTask, _repositoryProvider.Project);
        public IEditWorkTaskUseCase Edit => new EditWorkTaskUseCase(_repositoryProvider.WorkTask, _repositoryProvider.Project);
        public IViewWorkTaskUseCase View => new ViewWorkTaskUseCase(_repositoryProvider.WorkTask, _repositoryProvider.Project);
        public IDeleteWorkTaskUseCase Delete => new DeleteWorkTaskUseCase(_repositoryProvider.WorkTask,  _repositoryProvider.Project);
    }
}
